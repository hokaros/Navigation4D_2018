using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

public class ShaderVisualizerInterprojected : MonoBehaviour, IShaderVisualizer
{
    public Material faceMaterial;
    public Material wideEdgesMaterial;
    public Material lineEdgesMaterial;
    [SerializeField] float viewportDistance = 10;

    private Mesh mesh;


    public bool IsMeshDynamic => false;

    // Since the vertex calculations are made in a shader, we cannot put the exact bounds here
    public Bounds MeshBounds => new Bounds(GetComponent<Transform4>().GlobalPosition, new Vector3(100, 100, 100));

    public Material[] Materials => new Material[] { faceMaterial, wideEdgesMaterial, lineEdgesMaterial };
   


    public void UpdateEdgeWidth(float edgeWidth)
    {
        throw new NotImplementedException();
        Polytope4 polytope = GetComponent<Polytope4>();

        List<Triangle4> midFaceTriangles;
        List<Vector4> midFaceVertices = WideEdgesCreator.MidFaceVertices(polytope.GetVertices(), polytope.Faces, edgeWidth, out midFaceTriangles);

        List<Vector4> allVertices = new List<Vector4>(polytope.GetVertices());
        allVertices.AddRange(midFaceVertices);

        //mesh.SetVertexBufferData(allVertices, 0, 0, allVertices.Count);
    }

    public void UpdateFaceOpacity(float opacity)
    {
        Color prev = faceMaterial.color;
        faceMaterial.color = new Color(prev.r, prev.g, prev.b, opacity);
    }

    public void UpdateTransform(Transform selfTransform)
    {
        // Disable standard transform - Transform4 is used in the shader
        selfTransform.position = Vector3.zero;
        selfTransform.rotation = Quaternion.identity;
        selfTransform.localScale = new Vector3(1, 1, 1);
    }

    public void UpdateMaterialProperties()
    {
        // Update position and rotation
        Transform4 transform4 = GetComponent<Transform4>();

        foreach (Material material in Materials)
        {
            material.SetVector("_Position", transform4.Position);

            material.SetFloat("_RotationXy", transform4.Rotation.Xy);
            material.SetFloat("_RotationXz", transform4.Rotation.Xz);
            material.SetFloat("_RotationXw", transform4.Rotation.Xw);
            material.SetFloat("_RotationYz", transform4.Rotation.Yz);
            material.SetFloat("_RotationYw", transform4.Rotation.Yw);
            material.SetFloat("_RotationZw", transform4.Rotation.Zw);

            Transform4 cameraTransform4 = Camera.main.GetComponent<Transform4>();

            material.SetVector("_CameraPosition", cameraTransform4.GlobalPosition);

            // TODO: camera global rotation
            material.SetFloat("_CameraRotationXy", cameraTransform4.Rotation.Xy);
            material.SetFloat("_CameraRotationXz", cameraTransform4.Rotation.Xz);
            material.SetFloat("_CameraRotationXw", cameraTransform4.Rotation.Xw);
            material.SetFloat("_CameraRotationYz", cameraTransform4.Rotation.Yz);
            material.SetFloat("_CameraRotationYw", cameraTransform4.Rotation.Yw);
            material.SetFloat("_CameraRotationZw", cameraTransform4.Rotation.Zw);
            material.SetVector("_CameraForward", cameraTransform4.Forward);

            material.SetFloat("_ViewportDistance", viewportDistance);
        }
    }

    public Mesh InitializeMesh(float edgeWidth)
    {
        throw new NotImplementedException();
        //Polytope4 polytope = GetComponent<Polytope4>();
        //List<Vector4> vertices = polytope.StartVertices;
        //List<Edge4> edges = polytope.Edges;
        //List<Triangle4> triangles = polytope.Triangles;
        //List<List<int>> faces = polytope.Faces;

        //Mesh mesh = new Mesh();
        //mesh.subMeshCount = 3;

        //// Vertices
        //var vertexBufferLayout = new[]
        //{
        //    // 4-dimensional vertex position
        //    new VertexAttributeDescriptor(VertexAttribute.Position, VertexAttributeFormat.Float32, 4)
        //};

        //// Mid-Face vertices for rendering wide edges
        //List<Triangle4> midFaceTriangles;
        //List<Vector4> midFaceVertices = WideEdgesCreator.MidFaceVertices(vertices, faces, edgeWidth, out midFaceTriangles);

        //List<Vector4> allVertices = new List<Vector4>(vertices);
        //allVertices.AddRange(midFaceVertices);

        //mesh.SetVertexBufferParams(allVertices.Count, vertexBufferLayout);
        //mesh.SetVertexBufferData(allVertices, 0, 0, allVertices.Count);

        //MeshCalculations.InitializeSolidSubmesh(mesh, triangles, 0);
        //MeshCalculations.InitializeSolidSubmesh(mesh, midFaceTriangles, 1);
        //MeshCalculations.InitializeWireframeSubmesh(mesh, edges, 2);

        //this.mesh = mesh;
        //return mesh;
    }



}
