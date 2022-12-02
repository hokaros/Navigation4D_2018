using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simplex : Polytope4
{

	[SerializeField] float edgeSize = 2f;
	public override List<Vector4> StartVertices
		=> new List<Vector4>
		{
			new Vector4(1,1,1,-0.44721359f) * edgeSize,
			new Vector4(1,-1,-1,-0.44721359f) * edgeSize,
			new Vector4(-1,1,-1,-0.44721359f) * edgeSize,
			new Vector4(-1,-1,1,-0.44721359f) * edgeSize,
			new Vector4(0,0,0,1.78885438f) * edgeSize,
		};

	public override List<Edge> Edges
	{
		get
		{
			List<Edge> edges = new List<Edge>();
			edges.Add(new Edge(0, 1));
			edges.Add(new Edge(0, 2));
			edges.Add(new Edge(0, 3));
			edges.Add(new Edge(0, 4));
			edges.Add(new Edge(1, 2));
			edges.Add(new Edge(1, 3));
			edges.Add(new Edge(1, 4));
			edges.Add(new Edge(2, 3));
			edges.Add(new Edge(2, 4));
			edges.Add(new Edge(3, 4));
				
			return edges;
		}
	}

	public override List<List<int>> Faces
	{
		get
		{
			List<List<int>> faces = new List<List<int>>();
			faces.Add(new List<int>() { 0, 1, 4 });
			faces.Add(new List<int>() { 0, 1, 2 });
			faces.Add(new List<int>() { 0, 1, 3 });
			faces.Add(new List<int>() { 0, 2, 4 });
			faces.Add(new List<int>() { 0, 2, 3 });
			faces.Add(new List<int>() { 0, 4, 3 });
			faces.Add(new List<int>() { 1, 2, 3 });
			faces.Add(new List<int>() { 1, 2, 4 });
			faces.Add(new List<int>() { 1, 3, 4 });
			faces.Add(new List<int>() { 2, 3, 4 });

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
