using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Polytope3
{
    public List<Vector3> vertices;
    public List<Edge> edges;
    public List<List<int>> faces;

    public int[] GetTriangles()
    {
        List<Vector3Int> triangles = new List<Vector3Int>();
        foreach (var face in faces)
        {
            foreach (var triangle in FaceToTriangles(face))
            {
                triangles.Add(triangle);
            }
        }

        int[] trianglesArray = new int[2*triangles.Count * 3];
        for (int i = 0; i < triangles.Count; i++)
        {
            Vector3Int triangle = triangles[i];
            trianglesArray[6 * i] = triangle.x;
            trianglesArray[6 * i + 1] = triangle.y;
            trianglesArray[6 * i + 2] = triangle.z;
            trianglesArray[6 * i + 3] = triangle.z;
            trianglesArray[6 * i + 4] = triangle.y;
            trianglesArray[6 * i + 5] = triangle.x;
        }

        return trianglesArray;
    }

    public List<List<int>> GetVertexFaces()
    {
        return faces.Select(
            face => EdgeFaceToVertexFace(face)
            ).ToList();
    }

    private List<int> EdgeFaceToVertexFace(List<int> edgeFace)
    {
        List<int> vertexFace = new List<int>();

        for(int i = 0; i < edgeFace.Count - 1; i++)
        {
            int edgeIndex = edgeFace[i];
            int edgeStart = edges[edgeIndex].startId;
            int edgeEnd = edges[edgeIndex].endId;
            if(i == 0)
            {
                // Add both vertices, but add first the one that isn't present in the next edge
                int nextEdgeStart = edges[edgeFace[i + 1]].startId;
                int nextEdgeEnd = edges[edgeFace[i + 1]].endId;
                if (edgeStart != nextEdgeStart && edgeStart != nextEdgeEnd)
                {
                    vertexFace.Add(edgeStart);
                    vertexFace.Add(edgeEnd);
                }
                else
                {
                    vertexFace.Add(edgeEnd);
                    vertexFace.Add(edgeStart);
                }
            }
            else
            {
                int prevEdgeIndex = edgeFace[i - 1];
                int prevEdgeStart = edges[prevEdgeIndex].startId;
                int prevEdgeEnd = edges[prevEdgeIndex].endId;
                // Add the vertex that isn't present in the previous edge
                if(edgeStart != prevEdgeStart && edgeStart != prevEdgeEnd)
                {
                    vertexFace.Add(edgeStart);
                }
                else
                {
                    vertexFace.Add(edgeEnd);
                }
            }
        }

        return vertexFace;
    }

    private IEnumerable<Vector3Int> FaceToTriangles(List<int> edgeFace)
    {
        int edge1Id = edgeFace[0];
        int edge1Start = edges[edge1Id].startId;
        int edge1End = edges[edge1Id].endId;

        int edge2Id = edgeFace[1];
        int edge2Start = edges[edge2Id].startId;
        int edge2End = edges[edge2Id].endId;

        int vertexToRuleThemAll = (edge1Start == edge2Start) ? edge1Start : edge1End;

        for(int i = 2; i < edgeFace.Count; i++)
        {
            int edgeId = edgeFace[i];
            int edgeStart = edges[edgeId].startId;
            int edgeEnd = edges[edgeId].endId;
            yield return new Vector3Int(
                vertexToRuleThemAll,
                edgeStart,
                edgeEnd
                );
        }
    }
}
