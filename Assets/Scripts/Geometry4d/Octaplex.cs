using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Octaplex : Polytope4 {

	[SerializeField] float edgeSize = 2f;
	public override List<Vector4> StartVertices
	{
		get
		{
			List<Vector4> vertices = new List<Vector4>();
			vertices.Add(new Vector4(1, 1, 0, 0) * edgeSize);
			vertices.Add(new Vector4(1, -1, 0, 0) * edgeSize);
			vertices.Add(new Vector4(1, 0, 1, 0) * edgeSize);
			vertices.Add(new Vector4(1, 0, -1, 0) * edgeSize);
			vertices.Add(new Vector4(1, 0, 0, 1) * edgeSize);
			vertices.Add(new Vector4(1, 0, 0, -1) * edgeSize);
			vertices.Add(new Vector4(-1, 1, 0, 0) * edgeSize);
			vertices.Add(new Vector4(-1, -1, 0, 0) * edgeSize);
			vertices.Add(new Vector4(-1, 0, 1, 0) * edgeSize);
			vertices.Add(new Vector4(-1, 0, -1, 0) * edgeSize);
			vertices.Add(new Vector4(-1, 0, 0, 1) * edgeSize);
			vertices.Add(new Vector4(-1, 0, 0, -1) * edgeSize);
			vertices.Add(new Vector4(0, 1, 1, 0) * edgeSize);
			vertices.Add(new Vector4(0, 1, -1, 0) * edgeSize);
			vertices.Add(new Vector4(0, 1, 0, 1) * edgeSize);
			vertices.Add(new Vector4(0, 1, 0, -1) * edgeSize);
			vertices.Add(new Vector4(0, -1, 1, 0) * edgeSize);
			vertices.Add(new Vector4(0, -1, -1, 0) * edgeSize);
			vertices.Add(new Vector4(0, -1, 0, 1) * edgeSize);
			vertices.Add(new Vector4(0, -1, 0, -1) * edgeSize);
			vertices.Add(new Vector4(0, 0, 1, 1) * edgeSize);
			vertices.Add(new Vector4(0, 0, 1, -1) * edgeSize);
			vertices.Add(new Vector4(0, 0, -1, 1) * edgeSize);
			vertices.Add(new Vector4(0, 0, -1, -1) * edgeSize);
			return vertices;
		}
	}

	public override List<Edge> Edges
	{
		get
		{
			List<Edge> edges = new List<Edge>();
			edges.Add(new Edge(0, 2));
			edges.Add(new Edge(0, 3));
			edges.Add(new Edge(0, 4));
			edges.Add(new Edge(0, 5));
			edges.Add(new Edge(0, 12));
			edges.Add(new Edge(0, 13));
			edges.Add(new Edge(0, 14));
			edges.Add(new Edge(0, 15));
			edges.Add(new Edge(1, 2));
			edges.Add(new Edge(1, 3));
			edges.Add(new Edge(1, 4));
			edges.Add(new Edge(1, 5));
			edges.Add(new Edge(1, 16));
			edges.Add(new Edge(1, 17));
			edges.Add(new Edge(1, 18));
			edges.Add(new Edge(1, 19));
			edges.Add(new Edge(2, 4));
			edges.Add(new Edge(2, 5));
			edges.Add(new Edge(2, 12));
			edges.Add(new Edge(2, 16));
			edges.Add(new Edge(2, 20));
			edges.Add(new Edge(2, 21));
			edges.Add(new Edge(3, 4));
			edges.Add(new Edge(3, 5));
			edges.Add(new Edge(3, 13));
			edges.Add(new Edge(3, 17));
			edges.Add(new Edge(3, 22));
			edges.Add(new Edge(3, 23));
			edges.Add(new Edge(4, 14));
			edges.Add(new Edge(4, 18));
			edges.Add(new Edge(4, 20));
			edges.Add(new Edge(4, 22));
			edges.Add(new Edge(5, 15));
			edges.Add(new Edge(5, 19));
			edges.Add(new Edge(5, 21));
			edges.Add(new Edge(5, 23));
			edges.Add(new Edge(6, 8));
			edges.Add(new Edge(6, 9));
			edges.Add(new Edge(6, 10));
			edges.Add(new Edge(6, 11));
			edges.Add(new Edge(6, 12));
			edges.Add(new Edge(6, 13));
			edges.Add(new Edge(6, 14));
			edges.Add(new Edge(6, 15));
			edges.Add(new Edge(7, 8));
			edges.Add(new Edge(7, 9));
			edges.Add(new Edge(7, 10));
			edges.Add(new Edge(7, 11));
			edges.Add(new Edge(7, 16));
			edges.Add(new Edge(7, 17));
			edges.Add(new Edge(7, 18));
			edges.Add(new Edge(7, 19));
			edges.Add(new Edge(8, 10));
			edges.Add(new Edge(8, 11));
			edges.Add(new Edge(8, 12));
			edges.Add(new Edge(8, 16));
			edges.Add(new Edge(8, 20));
			edges.Add(new Edge(8, 21));
			edges.Add(new Edge(9, 10));
			edges.Add(new Edge(9, 11));
			edges.Add(new Edge(9, 13));
			edges.Add(new Edge(9, 17));
			edges.Add(new Edge(9, 22));
			edges.Add(new Edge(9, 23));
			edges.Add(new Edge(10, 14));
			edges.Add(new Edge(10, 18));
			edges.Add(new Edge(10, 20));
			edges.Add(new Edge(10, 22));
			edges.Add(new Edge(11, 15));
			edges.Add(new Edge(11, 19));
			edges.Add(new Edge(11, 21));
			edges.Add(new Edge(11, 23));
			edges.Add(new Edge(12, 14));
			edges.Add(new Edge(12, 15));
			edges.Add(new Edge(12, 20));
			edges.Add(new Edge(12, 21));
			edges.Add(new Edge(13, 14));
			edges.Add(new Edge(13, 15));
			edges.Add(new Edge(13, 22));
			edges.Add(new Edge(13, 23));
			edges.Add(new Edge(14, 20));
			edges.Add(new Edge(14, 22));
			edges.Add(new Edge(15, 21));
			edges.Add(new Edge(15, 23));
			edges.Add(new Edge(16, 18));
			edges.Add(new Edge(16, 19));
			edges.Add(new Edge(16, 20));
			edges.Add(new Edge(16, 21));
			edges.Add(new Edge(17, 18));
			edges.Add(new Edge(17, 19));
			edges.Add(new Edge(17, 22));
			edges.Add(new Edge(17, 23));
			edges.Add(new Edge(18, 20));
			edges.Add(new Edge(18, 22));
			edges.Add(new Edge(19, 21));
			edges.Add(new Edge(19, 23));

			return edges;
		}
	}

	public override List<List<int>> Faces
	{
		get
		{
			List<List<int>> faces = new List<List<int>>();
			faces.Add(new List<int>() { 0, 2, 4 });
			faces.Add(new List<int>() { 0, 2, 5 });
			faces.Add(new List<int>() { 0, 2, 12 });
			faces.Add(new List<int>() { 0, 3, 4 });
			faces.Add(new List<int>() { 0, 3, 5 });
			faces.Add(new List<int>() { 0, 3, 13 });
			faces.Add(new List<int>() { 0, 4, 14 });
			faces.Add(new List<int>() { 0, 5, 15 });
			faces.Add(new List<int>() { 0, 12, 14 });
			faces.Add(new List<int>() { 0, 12, 15 });
			faces.Add(new List<int>() { 0, 13, 14 });
			faces.Add(new List<int>() { 0, 13, 15 });
			faces.Add(new List<int>() { 1, 2, 4 });
			faces.Add(new List<int>() { 1, 2, 5 });
			faces.Add(new List<int>() { 1, 2, 16 });
			faces.Add(new List<int>() { 1, 3, 4 });
			faces.Add(new List<int>() { 1, 3, 5 });
			faces.Add(new List<int>() { 1, 3, 17 });
			faces.Add(new List<int>() { 1, 4, 18 });
			faces.Add(new List<int>() { 1, 5, 19 });
			faces.Add(new List<int>() { 1, 16, 18 });
			faces.Add(new List<int>() { 1, 16, 19 });
			faces.Add(new List<int>() { 1, 17, 18 });
			faces.Add(new List<int>() { 1, 17, 19 });
			faces.Add(new List<int>() { 2, 4, 20 });
			faces.Add(new List<int>() { 2, 5, 21 });
			faces.Add(new List<int>() { 2, 12, 20 });
			faces.Add(new List<int>() { 2, 12, 21 });
			faces.Add(new List<int>() { 2, 16, 20 });
			faces.Add(new List<int>() { 2, 16, 21 });
			faces.Add(new List<int>() { 3, 4, 22 });
			faces.Add(new List<int>() { 3, 5, 23 });
			faces.Add(new List<int>() { 3, 13, 22 });
			faces.Add(new List<int>() { 3, 13, 23 });
			faces.Add(new List<int>() { 3, 17, 22 });
			faces.Add(new List<int>() { 3, 17, 23 });
			faces.Add(new List<int>() { 4, 14, 20 });
			faces.Add(new List<int>() { 4, 14, 22 });
			faces.Add(new List<int>() { 4, 18, 20 });
			faces.Add(new List<int>() { 4, 18, 22 });
			faces.Add(new List<int>() { 5, 15, 21 });
			faces.Add(new List<int>() { 5, 15, 23 });
			faces.Add(new List<int>() { 5, 19, 21 });
			faces.Add(new List<int>() { 5, 19, 23 });
			faces.Add(new List<int>() { 6, 8, 10 });
			faces.Add(new List<int>() { 6, 8, 11 });
			faces.Add(new List<int>() { 6, 8, 12 });
			faces.Add(new List<int>() { 6, 9, 10 });
			faces.Add(new List<int>() { 6, 9, 11 });
			faces.Add(new List<int>() { 6, 9, 13 });
			faces.Add(new List<int>() { 6, 10, 14 });
			faces.Add(new List<int>() { 6, 11, 15 });
			faces.Add(new List<int>() { 6, 12, 14 });
			faces.Add(new List<int>() { 6, 12, 15 });
			faces.Add(new List<int>() { 6, 13, 14 });
			faces.Add(new List<int>() { 6, 13, 15 });
			faces.Add(new List<int>() { 7, 8, 10 });
			faces.Add(new List<int>() { 7, 8, 11 });
			faces.Add(new List<int>() { 7, 8, 16 });
			faces.Add(new List<int>() { 7, 9, 10 });
			faces.Add(new List<int>() { 7, 9, 11 });
			faces.Add(new List<int>() { 7, 9, 17 });
			faces.Add(new List<int>() { 7, 10, 18 });
			faces.Add(new List<int>() { 7, 11, 19 });
			faces.Add(new List<int>() { 7, 16, 18 });
			faces.Add(new List<int>() { 7, 16, 19 });
			faces.Add(new List<int>() { 7, 17, 18 });
			faces.Add(new List<int>() { 7, 17, 19 });
			faces.Add(new List<int>() { 8, 10, 20 });
			faces.Add(new List<int>() { 8, 11, 21 });
			faces.Add(new List<int>() { 8, 12, 20 });
			faces.Add(new List<int>() { 8, 12, 21 });
			faces.Add(new List<int>() { 8, 16, 20 });
			faces.Add(new List<int>() { 8, 16, 21 });
			faces.Add(new List<int>() { 9, 10, 22 });
			faces.Add(new List<int>() { 9, 11, 23 });
			faces.Add(new List<int>() { 9, 13, 22 });
			faces.Add(new List<int>() { 9, 13, 23 });
			faces.Add(new List<int>() { 9, 17, 22 });
			faces.Add(new List<int>() { 9, 17, 23 });
			faces.Add(new List<int>() { 10, 14, 20 });
			faces.Add(new List<int>() { 10, 14, 22 });
			faces.Add(new List<int>() { 10, 18, 20 });
			faces.Add(new List<int>() { 10, 18, 22 });
			faces.Add(new List<int>() { 11, 15, 21 });
			faces.Add(new List<int>() { 11, 15, 23 });
			faces.Add(new List<int>() { 11, 19, 21 });
			faces.Add(new List<int>() { 11, 19, 23 });
			faces.Add(new List<int>() { 12, 14, 20 });
			faces.Add(new List<int>() { 12, 15, 21 });
			faces.Add(new List<int>() { 13, 14, 22 });
			faces.Add(new List<int>() { 13, 15, 23 });
			faces.Add(new List<int>() { 16, 18, 20 });
			faces.Add(new List<int>() { 16, 19, 21 });
			faces.Add(new List<int>() { 17, 18, 22 });
			faces.Add(new List<int>() { 17, 19, 23 });
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
