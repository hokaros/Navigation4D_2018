using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

static class WideEdgesCreator
{
    private struct Quadrangle4
    {
        public int v1;
        public int v2;
        public int v3;
        public int v4;

        public Quadrangle4(int v1, int v2, int v3, int v4)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
        }
    }

    public static List<Vector4> MidFaceVertices(List<Vector4> polytopeVertices, List<List<int>> faces, float scale, out List<Triangle4> triangles)
    {
        List<Vector4> allMidFaceVertices = new List<Vector4>();
        triangles = new List<Triangle4>();
        int createdMidVertices = 0;
        foreach (List<int> face in faces)
        {
            List<Vector4> currentFaceVertices = new List<Vector4>();
            for (int i = 0; i < face.Count; i++)
            {
                currentFaceVertices.Add(polytopeVertices[face[i]]);
            }
            allMidFaceVertices.AddRange(Polytope4.ScaledFace(currentFaceVertices, scale));
            for (int i = 0; i < face.Count - 1; i++)
            {
                triangles.AddRange(DoubleSidedTrianglesOfAQuadrangle(new Quadrangle4(face[i], face[i + 1], polytopeVertices.Count + createdMidVertices + i + 1, polytopeVertices.Count + createdMidVertices + i)));
            }
            triangles.AddRange(DoubleSidedTrianglesOfAQuadrangle(new Quadrangle4(face[face.Count - 1], face[0], polytopeVertices.Count + createdMidVertices, polytopeVertices.Count + createdMidVertices + face.Count - 1)));
            createdMidVertices += face.Count;
        }
        return allMidFaceVertices;
    }

    private static List<Triangle4> DoubleSidedTrianglesOfAQuadrangle(Quadrangle4 quadrangle)
    {
        List<Triangle4> triangles = new List<Triangle4>();
        triangles.Add(new Triangle4(quadrangle.v1, quadrangle.v2, quadrangle.v3));
        triangles.Add(new Triangle4(quadrangle.v3, quadrangle.v4, quadrangle.v1));

        //double-sided face
        triangles.Add(new Triangle4(quadrangle.v3, quadrangle.v2, quadrangle.v1));
        triangles.Add(new Triangle4(quadrangle.v1, quadrangle.v4, quadrangle.v3));

        return triangles;
    }
}
