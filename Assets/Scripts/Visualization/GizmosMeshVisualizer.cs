using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class GizmosMeshVisualizer : MonoBehaviour
{
    public enum VisualizationType
    {
        Orthographical,
        Perspective,
        Native
    }

    [SerializeField] float vertexRadius = 0.3f;
    [SerializeField] VisualizationType visualizationType = VisualizationType.Perspective;
    [SerializeField] float faceCoplanarityTolerance = 10e-3f;

    private Polytope4 polytope;


    private Vector3? ProjectOrtographicaly(Vector4 original)
    {
        return new Vector3(original.x, original.y, original.z);
    }

    private Vector3? ProjectPerspectively(Vector4 original)
    {
        return PerspectiveProjection.Instance.ProjectPerspectively(original);
    }

    private Vector3? ProjectDispatch(Vector4 original)
    {
        switch (visualizationType)
        {
            case VisualizationType.Orthographical:
                return ProjectOrtographicaly(original);

            case VisualizationType.Perspective:
                return ProjectPerspectively(original);

            default:
                return original;
        }
    }


    /// <summary>
    /// Visualization for those kinds of visualizations which are based on transforming vertices
    /// </summary>
    private void OnDrawGizmosVertexMapping()
    {
        List<Vector4> vertices = polytope.VerticesWorld;
        if (vertices == null)
            return;

        foreach (var vertex in vertices)
        {
            Vector3? vector3 = ProjectDispatch(vertex);

            if (vector3.HasValue)
            {
                Gizmos.DrawSphere(vector3.Value, vertexRadius);
            }
        }

        List<Edge> edges = polytope.Edges;
        if (edges == null)
            return;
        foreach (var edge in edges)
        {
            int edgeStart = edge.startId;
            int edgeEnd = edge.endId;
            Vector3? edgeStart3 = ProjectDispatch(vertices[edgeStart]);
            Vector3? edgeEnd3 = ProjectDispatch(vertices[edgeEnd]);

            if (edgeStart3.HasValue && edgeEnd3.HasValue)
            {
                Gizmos.DrawLine(edgeStart3.Value, edgeEnd3.Value);
            }
        }
    }

    private void DrawNativeVisualization()
    {
        Polytope3 intersection = GeometryIntersection.GetNativeIntersection(polytope, faceCoplanarityTolerance);

        // Draw vertices
        foreach(Vector3 v in intersection.vertices)
        {
            Gizmos.DrawSphere(v, vertexRadius);
        }

        // Draw edges
        foreach(var edge in intersection.edges)
        {
            int edgeStartId = edge.startId;
            int edgeEndId = edge.endId;
            Vector3 edgeStart = intersection.vertices[edgeStartId];
            Vector3 edgeEnd = intersection.vertices[edgeEndId];
            Gizmos.DrawLine(edgeStart, edgeEnd);
        }

        // Draw faces
        Gizmos.color = Color.red;
        foreach(List<int> face in intersection.faces)
        {
            foreach(int edgeIndex in face)
            {
                Vector3 offset = new Vector3(0.001f, 0, 0);
                Vector3 edgeStart = intersection.vertices[intersection.edges[edgeIndex].startId];
                Vector3 edgeEnd = intersection.vertices[intersection.edges[edgeIndex].endId];
                Gizmos.DrawLine(edgeStart + offset, edgeEnd + offset);
            }
        }
        Gizmos.color = Color.black;
        foreach(List<int> face in intersection.GetVertexFaces())
        {
            Vector3 sum = Vector3.zero;
            foreach(int vertexIndex in face)
            {
                Vector3 vertex = intersection.vertices[vertexIndex];
                sum += vertex;
            }
            sum /= face.Count;
            Gizmos.DrawSphere(sum, vertexRadius/2);
        }
    }

    private void Start()
    {
        polytope = GetComponent<Polytope4>();
    }

    private void OnDrawGizmos()
    {
        if (polytope == null)
            return;

        Gizmos.color = Color.blue;

        if(visualizationType == VisualizationType.Orthographical || visualizationType == VisualizationType.Perspective)
        {
            OnDrawGizmosVertexMapping();
        }
        else
        {
            DrawNativeVisualization();
        }
    }
}
