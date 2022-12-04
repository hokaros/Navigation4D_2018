using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ShaderVisualizerIntersectioned : MonoBehaviour, IShaderVisualizer
{
    public Material faceMaterial;
    public Material wideEdgesMaterial;
    public Material lineEdgesMaterial;
    [SerializeField] float faceCoplanarityTolerance = 0.001f;

    private Mesh mesh;

    public bool IsMeshDynamic => true; // The intersection can change every frame

    public Bounds MeshBounds => mesh.bounds;

    public Material[] Materials => new Material[] { faceMaterial, lineEdgesMaterial, wideEdgesMaterial };

    public Mesh InitializeMesh(float edgeWidth, out Mesh boundingSubmesh)
    {
        Polytope4 polytope4 = GetComponent<Polytope4>();

        Polytope3 intersection = GeometryIntersection.GetNativeIntersection(polytope4, faceCoplanarityTolerance);

        Mesh mesh = new Mesh();
        mesh.subMeshCount = 3;

        List<Vector4> vertices = intersection.vertices.Select(v => new Vector4(v.x, v.y, v.z, 0)).ToList();

        // Mid-Face vertices for rendering wide edges
        List<Triangle4> midFaceTriangles;
        List<List<int>> faces = intersection.GetVertexFaces();
        List<Vector4> midFaceVertices4 = WideEdgesCreator.MidFaceVertices(vertices, faces, edgeWidth, out midFaceTriangles);
        List<Vector3> midFaceVertices = midFaceVertices4.Select(v => new Vector3(v.x, v.y, v.z)).ToList();

        List<Vector3> allVertices = new List<Vector3>(intersection.vertices);
        allVertices.AddRange(midFaceVertices);

        mesh.vertices = allVertices.ToArray();
        mesh.SetTriangles(intersection.GetTriangles(), 0);
        MeshCalculations.InitializeWireframeSubmesh(mesh, intersection.edges, 1);
        MeshCalculations.InitializeSolidSubmesh(mesh, midFaceTriangles, 2);

        this.mesh = mesh;

        // Separate the bounding mesh
        boundingSubmesh = new Mesh();
        boundingSubmesh.vertices = vertices.Select(v => new Vector3(v.x, v.y, v.z)).ToArray();
        boundingSubmesh.SetTriangles(intersection.GetTriangles(), 0);

        return mesh;
    }

    public void UpdateMaterialProperties()
    {
        return;
    }

    public void UpdateTransform(Transform selfTransform)
    {
        // Disable standard transform - Transform4 is used to calculate intersection
        // (?) We should update it differently if we define the 3D intersection in the center of space
        selfTransform.position = Vector3.zero;
        selfTransform.rotation = Quaternion.identity;
        selfTransform.localScale = new Vector3(1, 1, 1);
    }

    public void UpdateEdgeWidth(float edgeWidth)
    {
        return;
    }

    public void UpdateFaceOpacity(float opacity)
    {
        Color prev = faceMaterial.color;
        faceMaterial.color = new Color(prev.r, prev.g, prev.b, opacity);
    }
}
