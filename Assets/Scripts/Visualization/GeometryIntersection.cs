using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class GeometryIntersection
{
    /// <summary>
    /// Calculates the intersection of a given polytope with the camera's hyperplane of view
    /// </summary>
    /// <param name="polytope"></param>
    /// <param name="faceCoplanarityTolerance">Tolerance of connecting edges based on their coplanarity when detecting faces</param>
    /// <returns></returns>
    public static Polytope3 GetNativeIntersection(Polytope4 polytope, float faceCoplanarityTolerance)
    {
        Transform4 cameraTransform4 = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Transform4>();// Camera.main.GetComponent<Transform4>();

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
            return /*Camera.main*/GameObject.FindGameObjectsWithTag("MainCamera")[0].transform.TransformPoint(new Vector3(vertexCam.x, vertexCam.y, vertexCam.z));
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

        Dictionary<int, List<Vector4>> faceIntersections = polytope.FacesIntersections(hyperplane);

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
               // Debug.LogWarning($"Intersections with a face: {faceIntersectionPoints.Count}. {allPoints}");
                if (faceIntersectionPoints.Count < 2)
                    continue; // can't even form a single edge
            }

            intersectionEdges.Add(new VertexEdge4(faceIntersectionPoints[0], faceIntersectionPoints[1]));
        }

        return EdgeMesh4.FromVertexEdges(intersectionEdges);
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
