using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class MeshCalculations
{
    public static void InitializeSolidSubmesh(Mesh mesh, List<Triangle4> triangles, int submeshNo)
    {
        // triangles
        int[] indices = new int[triangles.Count * 3];
        for (int i = 0; i < triangles.Count; i++)
        {
            indices[3 * i] = triangles[i].v1Id;
            indices[3 * i + 1] = triangles[i].v2Id;
            indices[3 * i + 2] = triangles[i].v3Id;
        }

        mesh.SetTriangles(indices, submeshNo);
    }

    public static void InitializeWireframeSubmesh(Mesh mesh, List<Edge> edges, int submeshNo)
    {
        // Edges
        int[] indices = new int[edges.Count * 2];
        for (int i = 0; i < edges.Count; i++)
        {
            indices[2 * i] = edges[i].startId;
            indices[2 * i + 1] = edges[i].endId;
        }

        mesh.SetIndices(indices, MeshTopology.Lines, submeshNo);
    }
}
