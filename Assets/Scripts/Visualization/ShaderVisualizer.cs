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

    [SerializeField] float viewportDistance = 10;
    [SerializeField] float edgeWidth = 0.85f;
    [SerializeField] VisualizationType visualizationType;

    IShaderVisualizer visualizer;

    public void UpdateFaceOpacity(float opacity)
    {
        visualizer.UpdateFaceOpacity(opacity);
    }

    public void UpdateEdgeWidth(float edgeWidth)
    {
        visualizer.UpdateEdgeWidth(edgeWidth);
        this.edgeWidth = edgeWidth;
    }


    private void Update()
    {
        visualizer.UpdateTransform(transform);

        if (visualizer.IsMeshDynamic)
        {
            GetComponent<MeshFilter>().mesh = visualizer.InitializeMesh(edgeWidth);
        }
        visualizer.UpdateMaterialProperties();
        // Update mesh bounds. The camera needs to know when to render this object
        GetComponent<MeshFilter>().sharedMesh.bounds = visualizer.MeshBounds;
    }

    private void Start()
    {
        GetComponent<MeshFilter>().mesh = visualizer.InitializeMesh(edgeWidth);
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
    }
}
