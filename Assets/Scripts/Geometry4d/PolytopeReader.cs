using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Globalization;

public class PolytopeReader : MonoBehaviour
{
    [SerializeField] Material faces4D, wideEdges4D, edges4D;
    [SerializeField] Material faces3D, wideEdges3D, edges3D;
    public GameObject GenerateFromFile(string path)
    {
        string fileContent = ReadFile(path);
        string[] lines = fileContent.Split('\n');
        string name = lines[0];
        List<Vector4> vertices = new List<Vector4>();
        List<Edge> edges = new List<Edge>();
        List<List<int>> faces = new List<List<int>>();

        foreach(string v4 in lines[1].Split(';'))
        {
            string[] coordinates = v4.Split(',');
            float x = float.Parse(coordinates[0], CultureInfo.InvariantCulture);
            float y = float.Parse(coordinates[1], CultureInfo.InvariantCulture);
            float z = float.Parse(coordinates[2], CultureInfo.InvariantCulture);
            float w = float.Parse(coordinates[3], CultureInfo.InvariantCulture);
            vertices.Add(new Vector4(x, y, z, w));
        }

        foreach(string edge in lines[2].Split(';'))
        {
            string[] indices = edge.Split(',');
            edges.Add(new Edge(int.Parse(indices[0]), int.Parse(indices[1])));
        }

        foreach (string face in lines[3].Split(';'))
        {
            string[] indices = face.Split(',');
            List<int> faceIndices = new List<int>();
            foreach(string index in indices)
            {
                faceIndices.Add(int.Parse(index));
            }
            faces.Add(faceIndices);
        }
        return GeneratePolytope(vertices, edges, faces, name);
    }

    public GameObject GeneratePolytope(List<Vector4> vertices, List<Edge>edges, List<List<int>>faces, string name="new polytope")
    {
        GameObject polytope = new GameObject(name);
        polytope.AddComponent<Transform4>();
        polytope.AddComponent<GizmosMeshVisualizer>();
        polytope.AddComponent<MeshRenderer>();
        polytope.AddComponent<MeshFilter>();

        polytope.AddComponent<Polytope4>();


        Polytope4 p4 = polytope.GetComponent<Polytope4>();
        p4.Initialize(vertices, edges, faces);
        polytope.AddComponent<ShaderVisualizer>();

        ShaderVisualizerInterprojected interprojected = polytope.GetComponent<ShaderVisualizerInterprojected>();
        ShaderVisualizerIntersectioned intersectioned = polytope.GetComponent<ShaderVisualizerIntersectioned>();

        interprojected.faceMaterial = faces4D;
        interprojected.wideEdgesMaterial = wideEdges4D;
        interprojected.lineEdgesMaterial = edges4D;

        intersectioned.faceMaterial = faces3D;
        intersectioned.wideEdgesMaterial = wideEdges3D;
        intersectioned.lineEdgesMaterial = edges3D;
        return polytope;
    }

    public string ReadFile(string path)
    {
        StreamReader reader = new StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        return content;
    }

    public GameObject GenerateSimplex()
    {
        return GenerateFromFile("assets/polytopes/simplex.txt");
    }

    public GameObject GenerateTesseract()
    {
        return GenerateFromFile("assets/polytopes/tesseract.txt");
    }

    public GameObject GenerateDodecaplex()
    {
        return GenerateFromFile("assets/polytopes/dodecaplex.txt");
    }

    void Start()
    {
        
    }

    void Update()
    {

    }
}
