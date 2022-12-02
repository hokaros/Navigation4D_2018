using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[RequireComponent(typeof(Transform4))]
[ExecuteInEditMode]
public class Polytope4 : MonoBehaviour
{
    public Transform4 transform4 { get; private set; }

    public virtual List<Vector4> StartVertices { 
        get
        {
            return vertices;
        }
    }
    public virtual List<Edge> Edges {
        get
        {
            return edges;
        }
    }
    public virtual List<Triangle4> Triangles {
        get
        {
            return triangles;
        }
    }
    public virtual List<List<int>> Faces {
        get
        {
            return faces;
        }
    }

    private List<Vector4> verticesWorld;
    /// <summary>
    /// Vertices of the polytope in the world space
    /// </summary>
    public List<Vector4> VerticesWorld {
        get
        {
            if(vertices == null)
            {
                Start();
            }
            return verticesWorld;
        }
    }

    /// <summary>
    /// Vertices in local space
    /// </summary>
    protected List<Vector4> vertices;
    /// <summary>
    /// Pairs of indices in the list of vertices
    /// </summary>
    protected List<Edge> edges;

    /// <summary>
    /// Triplets of indices in the list of vertices
    /// </summary>
    protected List<Triangle4> triangles;

    /// <summary>
    /// n's of indices forming a face
    /// </summary>
    protected List<List<int>> faces;

    /// <summary>
    /// Cached map. Key: edge index. Value: indexes of faces the edge belongs to.
    /// </summary>
    protected Dictionary<int, List<int>> facesForEdges = new Dictionary<int, List<int>>();


    /// <summary>
    /// Returns faces which the edge belongs to.
    /// </summary>
    /// <param name="edgeIndex">Index to the "Edges" property</param>
    /// <returns>Indexes to the "Faces" property</returns>
    public List<int> GetEdgeFaces(int edgeIndex)
    {
        return facesForEdges[edgeIndex];
    }

    public static List<Vector4> VerticesOnPlane(List<Vector4> vertices, Plane4 plane)
    {
        List<Vector4> result = new List<Vector4>();

        foreach (Vector4 vertex in vertices)
        {
            if (plane.Contains(vertex))
            {
                result.Add(vertex);
            }
        }

        return result;
    }

    /// <summary>
    /// Intersects hyperplane with faces of the polytope
    /// </summary>
    /// <param name="hyperplane"></param>
    /// <returns>A dictionary where: Key = index of the face; Value = intersection points with the edges of the face. </returns>
    public virtual Dictionary<int, List<Vector4>> FacesIntersections(Hyperplane4 hyperplane)
    {
        List<Vector4> vertices = VerticesWorld;

        Dictionary<int, List<Vector4>> faceIntersections = new Dictionary<int, List<Vector4>>(); // faceIndex -> intersection points

        int edgeIndex = -1;
        foreach (Edge edge in Edges)
        {
            int edgeStartIndex = edge.startId;
            int edgeEndIndex = edge.endId;
            edgeIndex++;

            Vector4 edgeStart = vertices[edgeStartIndex];
            Vector4 edgeEnd = vertices[edgeEndIndex];

            Vector4? edgeIntersection = hyperplane.EdgeIntersection(edgeStart, edgeEnd);
            if (!edgeIntersection.HasValue)
                continue; // no intersection

            // Save the face which the edge belongs to, clone record for every face
            foreach (int faceIndex in GetEdgeFaces(edgeIndex))
            {
                // Add the intersection to the face's record
                if (faceIntersections.ContainsKey(faceIndex))
                {
                    faceIntersections[faceIndex].Add(edgeIntersection.Value);
                }
                else
                {
                    faceIntersections.Add(faceIndex, new List<Vector4> { edgeIntersection.Value });
                }
            }
        }

        return faceIntersections;
    }

    /// <summary>
    /// Returns faces which the edge belongs to.
    /// </summary>
    /// <param name="edgeIndex">Index to the "Edges" property</param>
    /// <returns>Indexes to the "Faces" property</returns>
    private List<int> GetEdgeFacesUncached(int edgeIndex)
    {
        int edgeStart = Edges[edgeIndex].startId;
        int edgeEnd = Edges[edgeIndex].endId;

        List<int> edgeFaces = new List<int>();

        int faceIndex = 0;
        foreach (var face in Faces)
        {
            bool edgeStartContained = false;
            bool edgeEndContained = false;

            foreach (int faceVertex in face)
            {
                if (edgeStart == faceVertex)
                {
                    edgeStartContained = true;
                }
                if (edgeEnd == faceVertex)
                {
                    edgeEndContained = true;
                }
            }

            if (edgeStartContained && edgeEndContained)
            {
                edgeFaces.Add(faceIndex);
            }
            faceIndex++;
        }

        return edgeFaces;
    }

    /// <summary>
    /// Recalculates properties of a mesh which don't change often. Currently only recalculates mapping edges to faces
    /// </summary>
    private void UpdateMeshRelationsCache()
    {
        facesForEdges = GetFacesForEdgesUncached();
    }

    protected virtual Dictionary<int, List<int>> GetFacesForEdgesUncached()
    {
        Dictionary<int, List<int>> facesForEdges = new Dictionary<int, List<int>>();

        for (int edgeIndex = 0; edgeIndex < Edges.Count; edgeIndex++)
        {
            facesForEdges.Add(
                edgeIndex,
                GetEdgeFacesUncached(edgeIndex)
            );
        }

        return facesForEdges;
    }


    public static List<Vector4> ScaledFace(List<Vector4> face, float scale)
    {
        List<Vector4> scaledPoints = new List<Vector4>();
        Vector4 middlePoint = Vectors4.Sum(face) / face.Count;
        foreach (Vector4 point in face)
        {
            scaledPoints.Add(Vector4.LerpUnclamped(middlePoint, point, scale));
        }
        return scaledPoints;
    }

    public List<Vector4> GetVertices()
    {
        return vertices;
    }

    private void Awake()
    {
        transform4 = GetComponent<Transform4>();
    }

    // Assuming that the face is convex
    private List<Triangle4> TriangulateFaces(List<List<int>> faces)
    {
        List<Triangle4> triangles = new List<Triangle4>();
        foreach(List<int> face in faces)
        {
            if (face.Count < 3) continue;
            for(int i = 2; i < face.Count; i++)
            {
                triangles.Add(new Triangle4(face[0], face[i - 1], face[i]));
                triangles.Add(new Triangle4(face[i], face[i - 1], face[0]));
            }
        }
        return triangles;
    }

    public void Initialize(List<Vector4> vertices, List<Edge> edges, List<List<int>> faces)
    {
        this.vertices = new List<Vector4>(vertices);
        this.edges = new List<Edge>(edges);
        this.faces = new List<List<int>>(faces);
        triangles = TriangulateFaces(faces);
    }

    // Start is called before the first frame update
    void Start()
    {
        vertices = StartVertices;
        edges = Edges;
        triangles = Triangles;
        faces = Faces;
        Debug.Log($"Vertices: {vertices.Count}, edges: {edges.Count}, faces: {faces.Count}");

        transform4 = GetComponent<Transform4>();
        verticesWorld = vertices.Select(v => transform4.PointToWorld(v)).ToList();

        UpdateMeshRelationsCache();
    }

    // Update is called once per frame
    void Update()
    {
        verticesWorld = vertices.Select(v => transform4.PointToWorld(v)).ToList();
    }
}
