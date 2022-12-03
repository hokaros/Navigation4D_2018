using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orthoplex : Polytope4 {

	[SerializeField] float edgeSize = 2f;
	public override List<Vector4> StartVertices
    {
        get
        {
			List<Vector4> vertices = new List<Vector4>();
			vertices.Add(new Vector4(1, 0, 0, 0) * edgeSize);
			vertices.Add(new Vector4(-1, 0, 0, 0) * edgeSize);
			vertices.Add(new Vector4(0, 1, 0, 0) * edgeSize);
			vertices.Add(new Vector4(0, -1, 0, 0) * edgeSize);
			vertices.Add(new Vector4(0, 0, 1, 0) * edgeSize);
			vertices.Add(new Vector4(0, 0, -1, 0) * edgeSize);
			vertices.Add(new Vector4(0, 0, 0, 1) * edgeSize);
			vertices.Add(new Vector4(0, 0, 0, -1) * edgeSize);
			return vertices;
		}
    }


	public override List<Edge> Edges
	{
		get
		{
			List<Edge> edges = new List<Edge>();
			edges.Add(new Edge(0, 1));
			edges.Add(new Edge(0, 2));
			edges.Add(new Edge(0, 3));
			edges.Add(new Edge(0, 4));
			edges.Add(new Edge(0, 5));
			edges.Add(new Edge(0, 6));
			edges.Add(new Edge(1, 2));
			edges.Add(new Edge(1, 3));
			edges.Add(new Edge(1, 4));
			edges.Add(new Edge(1, 5));
			edges.Add(new Edge(1, 7));
			edges.Add(new Edge(2, 3));
			edges.Add(new Edge(2, 4));
			edges.Add(new Edge(2, 6));
			edges.Add(new Edge(2, 7));
			edges.Add(new Edge(3, 5));
			edges.Add(new Edge(3, 6));
			edges.Add(new Edge(3, 7));
			edges.Add(new Edge(4, 5));
			edges.Add(new Edge(4, 6));
			edges.Add(new Edge(4, 7));
			edges.Add(new Edge(5, 6));
			edges.Add(new Edge(5, 7));
			edges.Add(new Edge(6, 7));

			return edges;
		}
	}

	public override List<List<int>> Faces
	{
		get
		{
			List<List<int>> faces = new List<List<int>>();
			faces.Add(new List<int>() { 0, 1, 2 });
			faces.Add(new List<int>() { 0, 1, 3 });
			faces.Add(new List<int>() { 0, 1, 4 });
			faces.Add(new List<int>() { 0, 1, 5 });
			faces.Add(new List<int>() { 0, 2, 3 });
			faces.Add(new List<int>() { 0, 2, 4 });
			faces.Add(new List<int>() { 0, 2, 6 });
			faces.Add(new List<int>() { 0, 3, 5 });
			faces.Add(new List<int>() { 0, 3, 6 });
			faces.Add(new List<int>() { 0, 4, 5 });
			faces.Add(new List<int>() { 0, 4, 6 });
			faces.Add(new List<int>() { 0, 5, 6 });
			faces.Add(new List<int>() { 1, 2, 3 });
			faces.Add(new List<int>() { 1, 2, 4 });
			faces.Add(new List<int>() { 1, 2, 7 });
			faces.Add(new List<int>() { 1, 3, 5 });
			faces.Add(new List<int>() { 1, 3, 7 });
			faces.Add(new List<int>() { 1, 4, 5 });
			faces.Add(new List<int>() { 1, 4, 7 });
			faces.Add(new List<int>() { 1, 5, 7 });
			faces.Add(new List<int>() { 2, 3, 6 });
			faces.Add(new List<int>() { 2, 3, 7 });
			faces.Add(new List<int>() { 2, 4, 6 });
			faces.Add(new List<int>() { 2, 4, 7 });
			faces.Add(new List<int>() { 2, 6, 7 });
			faces.Add(new List<int>() { 3, 5, 6 });
			faces.Add(new List<int>() { 3, 5, 7 });
			faces.Add(new List<int>() { 3, 6, 7 });
			faces.Add(new List<int>() { 4, 5, 6 });
			faces.Add(new List<int>() { 4, 5, 7 });
			faces.Add(new List<int>() { 4, 6, 7 });
			faces.Add(new List<int>() { 5, 6, 7 });

			return faces;
		}
	}

	public override List<Triangle4> Triangles
	{
		get
		{
			List<Triangle4> triangles = new List<Triangle4>();
			foreach (List<int> face in Faces)
			{
				triangles.Add(new Triangle4(face[0], face[1], face[2]));

				//for double-sided faces
				triangles.Add(new Triangle4(face[2], face[1], face[0]));
			}
			return triangles;
		}
	}
}
