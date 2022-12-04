using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(ShaderVisualizerInterprojected))]
[RequireComponent(typeof(ShaderVisualizerIntersectioned))]
[ExecuteInEditMode]
public class ShaderVisualizer : MonoBehaviour
{
    public enum VisualizationType
    {
        Intersectioned,
        Interprojected,
    }

    [SerializeField] float edgeWidth = 0.85f;
    [SerializeField] VisualizationType visualizationType = VisualizationType.Intersectioned;

    IShaderVisualizer visualizer;
    MeshCollider collider;

    public void UpdateFaceOpacity(float opacity)
    {
        visualizer.UpdateFaceOpacity(opacity);
    }

    public void UpdateEdgeWidth(float edgeWidth)
    {
        visualizer.UpdateEdgeWidth(edgeWidth);
        this.edgeWidth = edgeWidth;
    }


    private void UpdateMesh()
    {
        Mesh boundingSubmesh;
        GetComponent<MeshFilter>().mesh = visualizer.InitializeMesh(edgeWidth, out boundingSubmesh);

        if (collider != null)
        {
            collider.sharedMesh = boundingSubmesh;
        }
    }

    private void Update()
    {
        visualizer.UpdateTransform(transform);

        if (visualizer.IsMeshDynamic)
        {
            UpdateMesh();
        }
        visualizer.UpdateMaterialProperties();
        // Update mesh bounds. The camera needs to know when to render this object
        GetComponent<MeshFilter>().sharedMesh.bounds = visualizer.MeshBounds;
    }

    private void Start()
    {
        UpdateMesh();

        GetComponent<MeshRenderer>().materials = visualizer.Materials;
    }

    private void Awake()
    {
        switch (visualizationType)
        {
            case VisualizationType.Interprojected:
                visualizer = GetComponent<ShaderVisualizerInterprojected>();
                break;
            case VisualizationType.Intersectioned:
                visualizer = GetComponent<ShaderVisualizerIntersectioned>();
                break;
        }

        collider = GetComponent<MeshCollider>();
    }
}
