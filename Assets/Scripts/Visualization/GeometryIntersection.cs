using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class GeometryIntersection
{
    /// <summary>
    /// Calculates the intersection of a given polytope with the camera's hyperplane of view
    /// </summary>
    /// <param name="polytope"></param>
    /// <param name="faceCoplanarityTolerance">Tolerance of connecting edges based on their coplanarity when detecting faces</param>
    /// <returns></returns>
    public static Polytope3 GetNativeIntersection(Polytope4 polytope, float faceCoplanarityTolerance)
    {
        Transform4 cameraTransform4 = Camera.main.GetComponent<Transform4>();

        // Get native hyperplane
        Hyperplane4 nativeHyperplane = cameraTransform4.GetNativeHyperplane();

        // Intersect polytope and the hyperplane
        EdgeMesh4 intersection = Polytope4Intersection(polytope, nativeHyperplane);

        List<Vector4> verticesGlobal = intersection.Vertices;

        List<Vector3> verticesCameraSpace = verticesGlobal.Select(v =>
        {
            // The intersection is global in 4D, transforming to the camera's local space makes all points have the 'w' coordinate equal to 0
            Vector4 vertexCam = cameraTransform4.PointToLocal(v);

            // The result is in 3D local space of the camera. Perform traditional transforming to world space
            return Camera.main.transform.TransformPoint(new Vector3(vertexCam.x, vertexCam.y, vertexCam.z));
        }).ToList();

        List<VertexEdge3> edgesCameraSpace = new List<VertexEdge3>();
        foreach (Edge edge in intersection.Edges)
        {
            edgesCameraSpace.Add(new VertexEdge3(
                verticesCameraSpace[edge.startId],
                verticesCameraSpace[edge.endId]
                ));
        }

        Polytope3 intersectionPolytope = new Polytope3
        {
            vertices = verticesCameraSpace,
            edges = intersection.Edges,
            faces = intersection.GetFaces(faceCoplanarityTolerance)
        };
        return intersectionPolytope;
    }

    public static EdgeMesh4 Polytope4Intersection(Polytope4 polytope, Hyperplane4 hyperplane)
    {
        List<VertexEdge4> intersectionEdges = new List<VertexEdge4>();

        Dictionary<int, List<Vector4>> faceIntersections = FacesIntersections(polytope, hyperplane);

        foreach(var entry in faceIntersections)
        {
            List<Vector4> faceIntersectionPoints = entry.Value;
            faceIntersectionPoints = EliminateDuplicates(faceIntersectionPoints);
            if (faceIntersectionPoints.Count != 2)
            {
                StringBuilder allPoints = new StringBuilder();
                foreach(Vector4 p in faceIntersectionPoints)
                {
                    allPoints.Append($"{p}, ");
                }
                Debug.LogError($"Intersections with a face: {faceIntersectionPoints.Count}. {allPoints}");
                if (faceIntersectionPoints.Count < 2)
                    continue; // can't even form a single edge
            }

            intersectionEdges.Add(new VertexEdge4(faceIntersectionPoints[0], faceIntersectionPoints[1]));
        }

        return EdgeMesh4.FromVertexEdges(intersectionEdges);
    }

    /// <summary>
    /// Calculates the intersection between an edge and a hyperplane
    /// </summary>
    public static Vector4? EdgeIntersection(Vector4 edgeStart, Vector4 edgeEnd, Hyperplane4 hyperplane)
    {
        Vector4 edgeDirection = edgeEnd - edgeStart;
        Line4 edgeLine = new Line4(edgeStart, edgeDirection);

        Vector4 crossingPoint;
        try
        {
            crossingPoint = hyperplane.CrossingPoint(edgeLine);
        }
        catch(DivideByZeroException)
        {
            return null; // No intersection (the edge is parallel to the hyperplane)
        }

        // Check if the crossingPoint is between the bounds of the edge
        Vector4 toCross = crossingPoint - edgeStart;
        Vector4 toEdgeEnd = edgeEnd - edgeStart;

        float fromStartToEndFactor = Mathf.Infinity;
        if(toEdgeEnd.x != 0)
        {
            fromStartToEndFactor = toCross.x / toEdgeEnd.x;
        }
        else if(toEdgeEnd.y != 0)
        {
            fromStartToEndFactor = toCross.y / toEdgeEnd.y;
        }
        else if (toEdgeEnd.z != 0)
        {
            fromStartToEndFactor = toCross.z / toEdgeEnd.z;
        }
        else if (toEdgeEnd.w != 0)
        {
            fromStartToEndFactor = toCross.w / toEdgeEnd.w;
        }

        if (fromStartToEndFactor >= 0f && fromStartToEndFactor <= 1f)
            return crossingPoint; // Between the edge bounds
        return null; // Crossing with the edge line, but outside of the edge
    }

    /// <summary>
    /// Intersects hyperplane with faces of a polytope
    /// </summary>
    /// <param name="polytope"></param>
    /// <param name="hyperplane"></param>
    /// <returns>A dictionary where: Key = index of the face; Value = intersection points with the edges of the face. </returns>
    private static Dictionary<int, List<Vector4>> FacesIntersections(Polytope4 polytope, Hyperplane4 hyperplane)
    {
        List<Vector4> vertices = polytope.VerticesWorld;

        Dictionary<int, List<Vector4>> faceIntersections = new Dictionary<int, List<Vector4>>(); // faceIndex -> intersection points

        int edgeIndex = -1;
        foreach (Edge edge in polytope.Edges)
        {
            int edgeStartIndex = edge.startId;
            int edgeEndIndex = edge.endId;
            edgeIndex++;

            Vector4 edgeStart = vertices[edgeStartIndex];
            Vector4 edgeEnd = vertices[edgeEndIndex];

            Vector4? edgeIntersection = EdgeIntersection(edgeStart, edgeEnd, hyperplane);
            if (!edgeIntersection.HasValue)
                continue; // no intersection

            // Save the face which the edge belongs to, clone record for every face
            foreach (int faceIndex in polytope.GetEdgeFaces(edgeIndex))
            {
                // Add the intersection to the face's record
                if (faceIntersections.ContainsKey(faceIndex))
                {
                    faceIntersections[faceIndex].Add(edgeIntersection.Value);
                }
                else
                {
                    faceIntersections.Add(faceIndex, new List<Vector4> { edgeIntersection.Value });
                }
            }
        }

        return faceIntersections;
    }

    private static List<Vector4> EliminateDuplicates(List<Vector4> points)
    {
        List<Vector4> uniquePoints = new List<Vector4>();

        foreach(Vector4 p in points)
        {
            bool isNew = true;
            foreach(Vector4 saved in uniquePoints)
            {
                if(p == saved)
                {
                    isNew = false;
                    break;
                }
            }

            if (isNew)
            {
                uniquePoints.Add(p);
            }
        }
        return uniquePoints;
    }
}
