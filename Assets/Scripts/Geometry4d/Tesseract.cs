using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class Tesseract : Polytope4
{
	[SerializeField] float edgeSize = 2f;
	public override List<Vector4> StartVertices
		=> new List<Vector4>
		{
			new Vector4(edgeSize/2,edgeSize/2,edgeSize/2,edgeSize/2),
			new Vector4(edgeSize/2,edgeSize/2,edgeSize/2,-edgeSize/2),
			new Vector4(edgeSize/2,edgeSize/2,-edgeSize/2,edgeSize/2),
			new Vector4(edgeSize/2,edgeSize/2,-edgeSize/2,-edgeSize/2),
			new Vector4(edgeSize/2,-edgeSize/2,edgeSize/2,edgeSize/2),
			new Vector4(edgeSize/2,-edgeSize/2,edgeSize/2,-edgeSize/2),
			new Vector4(edgeSize/2,-edgeSize/2,-edgeSize/2,edgeSize/2),
			new Vector4(edgeSize/2,-edgeSize/2,-edgeSize/2,-edgeSize/2),
			new Vector4(-edgeSize/2,edgeSize/2,edgeSize/2,edgeSize/2),
			new Vector4(-edgeSize/2,edgeSize/2,edgeSize/2,-edgeSize/2),
			new Vector4(-edgeSize/2,edgeSize/2,-edgeSize/2,edgeSize/2),
			new Vector4(-edgeSize/2,edgeSize/2,-edgeSize/2,-edgeSize/2),
			new Vector4(-edgeSize/2,-edgeSize/2,edgeSize/2,edgeSize/2),
			new Vector4(-edgeSize/2,-edgeSize/2,edgeSize/2,-edgeSize/2),
			new Vector4(-edgeSize/2,-edgeSize/2,-edgeSize/2,edgeSize/2),
			new Vector4(-edgeSize/2,-edgeSize/2,-edgeSize/2,-edgeSize/2)
		};

	public override List<Edge> Edges
	{
        get
        {
			List<Edge> edges = new List<Edge>();

			List<Vector4> vertices = StartVertices;
			// Organize into pairs where 2 vertices have 3 equal coordinates
			for(int i = 0; i < vertices.Count-1; i++)
            {
				for(int j = i + 1; j < vertices.Count; j++)
                {
					Vector4 v1 = vertices[i];
					Vector4 v2 = vertices[j];

					bool equalX = v1.x == v2.x;
					bool equalY = v1.y == v2.y;
					bool equalZ = v1.z == v2.z;
					bool equalW = v1.w == v2.w;
					if(equalX && equalY && equalZ
						|| equalX && equalY && equalW
						|| equalX && equalZ && equalW
						|| equalY && equalZ && equalW)
                    {
						edges.Add(new Edge(i, j));
                    }
                }
            }
			//StreamWriter writer = new StreamWriter("assets/polytopes/tesseractEdges.txt");
			//for (int i = 0; i < edges.Count; i++)
			//{
			//	writer.Write(edges[i].Item1 + ","+ edges[i].Item2+";");
			//}
			//writer.Close();
			return edges;
        }
    }

	public override List<List<int>> Faces
	{
		get
		{
			List<List<int>> faces = new List<List<int>>();
			List<Vector4> vertices = StartVertices;
			// Organize into fours, where they all have two equal coordinates
			for (int i = 0; i < vertices.Count; i++)
			{
				for (int j = i + 1; j < vertices.Count; j++)
				{
					for (int k = j + 1; k < vertices.Count; k++)
					{
						for (int l = k + 1; l < vertices.Count; l++)
						{
							Vector4 v1 = vertices[i];
							Vector4 v2 = vertices[j];
							Vector4 v3 = vertices[k];
							Vector4 v4 = vertices[l];

							bool equalX = v1.x == v2.x && v2.x == v3.x && v3.x == v4.x;
							bool equalY = v1.y == v2.y && v2.y == v3.y && v3.y == v4.y;
							bool equalZ = v1.z == v2.z && v2.z == v3.z && v3.z == v4.z;
							bool equalW = v1.w == v2.w && v2.w == v3.w && v3.w == v4.w;

							int numberOfEqualCoordinates = Convert.ToInt32(equalX) + Convert.ToInt32(equalY) + Convert.ToInt32(equalZ) + Convert.ToInt32(equalW);

							if (numberOfEqualCoordinates == 2)
							{
								faces.Add(new List<int>() { i, j, l, k });
							}
						}
					}
				}
			}
			//StreamWriter writer = new StreamWriter("assets/polytopes/tesseractFaces.txt");
			//for(int i = 0; i < faces.Count; i++)
   //         {
			//	for(int j = 0; j < faces[i].Count - 1; j++)
   //             {
			//		writer.Write(faces[i][j] + ",");
			//	}
			//	writer.Write(faces[i][faces[i].Count - 1] + ";");
			//}
			//writer.Close();
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
				triangles.Add(new Triangle4(face[2], face[3], face[0]));

				//for double-sided faces
				triangles.Add(new Triangle4(face[2], face[1], face[0]));
				triangles.Add(new Triangle4(face[0], face[3], face[2]));
			}
			return triangles;
		}
	}
}
