using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EdgeMesh4
{
    const int maxFaceVertices = 20;
    public List<Vector4> Vertices => vertices;
    public List<Edge> Edges => edges;

    private List<Vector4> vertices;
    private List<Edge> edges;
    private Dictionary<int, List<int>> edgeNeighbouring;

    public EdgeMesh4(List<Vector4> vertices, List<Edge> edges)
    {
        // warning: copying only the reference
        this.vertices = vertices;
        this.edges = edges;

        edgeNeighbouring = GetEdgeNeighbouring();
    }

    /// <summary>
    /// Creates an EdgeMesh4 based on edges represented as positions of their ends
    /// </summary>
    /// <param name="edges"></param>
    /// <returns></returns>
    public static EdgeMesh4 FromVertexEdges(List<VertexEdge4> edges)
    {
        List<Vector4> vertices;
        List<Edge> indexEdges;
        VertexEdgesToIndexEdges(edges, out vertices,  out indexEdges);

        return new EdgeMesh4(vertices, indexEdges);
    }

    /// <summary>
    /// Returns edges which form faces
    /// </summary>
    /// <returns></returns>
    public List<List<int>> GetFaces(float coplanarityTolerance = 10e-6f)
    {
        List<List<int>> faces = new List<List<int>>();

        // Krawędzie, aby utworzyć ścianę, muszą być:
        // - współpłaszczyźniane
        // - tworzyć zamknięty cykl
        for (int edgeIndex = 0; edgeIndex < edges.Count; edgeIndex++)
        {
            foreach (int neighbourIndex in edgeNeighbouring[edgeIndex])
            {
                List<int> cycle = ExtendToPlanarCycle(edgeIndex, neighbourIndex, coplanarityTolerance);
                if (cycle == null)
                    continue;

                if (cycle.Min() != edgeIndex)
                    continue; // Makes sure that only one edge adds a face

                if(StructureAlgorithms.ContainsDuplicates(cycle) || cycle.Count < 3)
                {
                    int x = 1;
                }
                faces.Add(cycle);
            }
        }

        return faces;
    }

    private List<List<int>> CopyListOfLists(List<List<int>> l)
    {
        List<List<int>> result = new List<List<int>>();
        for(int i = 0; i < l.Count; i++)
        {
            List<int> sublist = l[i];
            result.Add(new List<int>(sublist));
        }

        return result;
    }

    private static void VertexEdgesToIndexEdges(List<VertexEdge4> vertexEdges, out List<Vector4> uniqueVertices, out List<Edge> indexEdges)
    {
        float mergeTolerance = 10e-6f;

        uniqueVertices = new List<Vector4>();
        indexEdges = new List<Edge>();

        // Prepare a data structure to merge edges
        Dictionary<int, List<int>> vertexNeighbours = new Dictionary<int, List<int>>(); // index of vertex -> list of indices of its neighbours

        // Merge vertex positions
        foreach (VertexEdge4 vertexEdge in vertexEdges)
        {
            Vector4 edgeStart = vertexEdge.start;
            Vector4 edgeEnd = vertexEdge.end;
            int startIndex, endIndex;

            int closestToStartVertexIndex = FindClosestVertex(uniqueVertices, edgeStart, mergeTolerance);
            if (closestToStartVertexIndex != -1)
            { // Reuse a saved vertex
                startIndex = closestToStartVertexIndex;
            }
            else
            { // Save a new vertex
                uniqueVertices.Add(edgeStart);
                startIndex = uniqueVertices.Count - 1;
            }

            int closestToEndVertexIndex = FindClosestVertex(uniqueVertices, edgeEnd, mergeTolerance);
            if (closestToEndVertexIndex != -1)
            { // Reuse a saved vertex
                endIndex = closestToEndVertexIndex;
            }
            else
            { // Save a new vertex
                uniqueVertices.Add(edgeEnd);
                endIndex = uniqueVertices.Count - 1;
            }

            // Check if the edge already exists and add to the dictionary otherwise
            bool edgePresent = true;
            if (!vertexNeighbours.ContainsKey(startIndex))
            {
                edgePresent = false;
                vertexNeighbours.Add(startIndex, new List<int>());
            }
            if (!vertexNeighbours[startIndex].Contains(endIndex))
            {
                edgePresent = false;
                vertexNeighbours[startIndex].Add(endIndex);
            }
            
            if (!vertexNeighbours.ContainsKey(endIndex))
            {
                edgePresent = false;
                vertexNeighbours.Add(endIndex, new List<int>());
            }
            if (!vertexNeighbours[endIndex].Contains(startIndex))
            {
                edgePresent = false;
                vertexNeighbours[endIndex].Add(startIndex);
            }

            if (!edgePresent)
            {
                indexEdges.Add(new Edge(startIndex, endIndex));
            }
        }
    }

    /// <summary>
    /// Converts face representation from a list of edges to a list of vertices
    /// </summary>
    private List<int> EdgeFaceToVertexFace(List<int> edgeFace)
    {
        List<int> vertexFace = new List<int>();

        foreach(int edgeIndex in edgeFace)
        {
            int edgeStart = edges[edgeIndex].startId;
            int edgeEnd = edges[edgeIndex].endId;

            if (!vertexFace.Contains(edgeStart))
                vertexFace.Add(edgeStart);

            if (!vertexFace.Contains(edgeEnd))
                vertexFace.Add(edgeEnd);
        }

        return vertexFace;
    }

    /// <summary>
    /// Finds a cycle of neighbouring edges which starts with edge1 and edge2.
    /// The whole cycle lies on a plane (2D) which is determined by edge1 and edge2.
    /// </summary>
    /// <param name="edge1">Neighbour of edge2</param>
    /// <param name="edge2">Neighbour of edge1</param>
    /// <returns></returns>
    private List<int> ExtendToPlanarCycle(int edge1, int edge2, float coplanarityTolerance)
    {
        List<int> result = new List<int>();
        result.Add(edge1);
        result.Add(edge2);

        // Change indexes of edges to indexes of vertices
        int edge1Start = edges[edge1].startId;
        int edge1End = edges[edge1].endId;

        int edge2Start = edges[edge2].startId;
        int edge2End = edges[edge2].endId;

        // Get 3 points that determine the plane
        Vector3 a = vertices[edge1Start];
        Vector3 b = vertices[edge1End];
        Vector3 c = (edge2Start == edge1Start) || (edge2Start == edge1End) ? 
            vertices[edge2End] : vertices[edge2Start]; // choose the vertex that isn't in edge1

        int previousEdge = edge1;
        int currentEdge = edge2;
        while (true)
        {
            int cycleStart = edge1;
            if (previousEdge == edge1)
                cycleStart = -1; // Disallow 2-edge cycles

            int? nextEdge = GetCoplanarNeighbour(currentEdge, result, cycleStart, a, b, c, coplanarityTolerance); // ignore the previous edge so we don't come back
            if (!nextEdge.HasValue)
                return null; // Failed to create a full planar cycle

            if (nextEdge.Value == edge1)
                return result; // Full cycle

            result.Add(nextEdge.Value);
            if (result.Count > maxFaceVertices)
                return null; // Too long path

            previousEdge = currentEdge;
            currentEdge = nextEdge.Value;
        }
    }

    /// <summary>
    /// Returns a neighbour of edge which is not ignoreEdge and is coplanar with points a,b,c
    /// </summary>
    private int? GetCoplanarNeighbour(int edge, List<int> ignoredEdges, int unignoredEdge, Vector3 a, Vector3 b, Vector3 c, float coplanarityTolerance)
    {
        if (!edgeNeighbouring.ContainsKey(edge))
            return null; // No neighbours

        List<int> neighbours = edgeNeighbouring[edge];
        if (neighbours.Count == 0)
            return null; // No neighbours

        foreach(int neighbourIndex in neighbours)
        {
            if (neighbourIndex != unignoredEdge && ignoredEdges.Contains(neighbourIndex))
                continue;

            int neighbourStartIndex = edges[neighbourIndex].startId;
            int neighbourEndIndex = edges[neighbourIndex].endId;
            Vector3 neighbourStart = vertices[neighbourStartIndex];
            Vector3 neighbourEnd = vertices[neighbourEndIndex];

            if (!Plane4.AreCoplanar(a, b, c, neighbourStart, coplanarityTolerance))
                continue;
            if (!Plane4.AreCoplanar(a, b, c, neighbourEnd, coplanarityTolerance))
                continue;

            // Coplanar neighbour
            return neighbourIndex;
        }

        return null;
    }

    /// <summary>
    /// Finds neighbours for each edge
    /// </summary>
    /// <returns>Key: index of an edge. Value: list of indexes of the edge's neighbours</returns>
    private Dictionary<int, List<int>> GetEdgeNeighbouring()
    {
        Dictionary<int, List<int>> result = new Dictionary<int, List<int>>();

        int e1Index = -1;
        foreach (Edge e1 in edges)
        {
            int e1Start = e1.startId;
            int e1End = e1.endId;
            e1Index++;

            int e2Index = -1;
            foreach (Edge e2 in edges)
            {
                int e2Start = e2.startId;
                int e2End = e2.endId;
                e2Index++;

                if (e1Index == e2Index)
                    continue; // the same edge

                if (e1Start == e2Start ||
                    e1Start == e2End ||
                    e1End == e2Start ||
                    e1End == e2End)
                {
                    // Hello, neighbour!
                    if (result.ContainsKey(e1Index))
                    {
                        result[e1Index].Add(e2Index);
                    }
                    else
                    {
                        result.Add(
                            e1Index,
                            new List<int> { e2Index }
                            );
                    }
                }
            }
        }

        return result;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="vertices"></param>
    /// <param name="target"></param>
    /// <param name="toleranceSquared"></param>
    /// <returns>Index of the closest vertex. -1 if not found</returns>
    private static int FindClosestVertex(List<Vector4> vertices, Vector4 target, float toleranceSquared)
    {
        int closestIndex = -1;
        float closestDistanceSquared = Mathf.Infinity;

        for(int i = 0; i < vertices.Count; i++)
        {
            Vector4 v = vertices[i];
            float sqrDistance = (v - target).sqrMagnitude;
            if (toleranceSquared < sqrDistance)
                continue; // outside of tolerance

            if(closestDistanceSquared > sqrDistance)
            { // new king of the hill
                closestIndex = i;
                closestDistanceSquared = sqrDistance;
            }
        }

        return closestIndex;
    }
}
