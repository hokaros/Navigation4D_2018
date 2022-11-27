using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

/// <summary>
/// Polytope4 with GPU acceleration
/// </summary>
public class Polytope4Accelerated : Polytope4
{
    [SerializeField] ComputeShader meshCalculationsShader;
    [SerializeField] ComputeShader edgeIntersectionShader;

    protected override Dictionary<int, List<int>> GetFacesForEdgesUncached()
    {
        // Zamiana ścian na trójkąty razem z powiązaniem ze ścianami
        Dictionary<int, int> triangleToFace = new Dictionary<int, int>(); // index of triangle -> index of face
        List<Vector3Int> facesTriangles = new List<Vector3Int>(); // All triangles from all faces

        for (int i = 0; i < faces.Count; i++)
        {
            foreach (Vector3Int triangle in FaceToTriangles(faces[i]))
            {
                facesTriangles.Add(triangle);
                triangleToFace.Add(facesTriangles.Count - 1, i);
            }
        }
        // Załadowanie do bufora
        ComputeBuffer triangleBuffer = new ComputeBuffer(facesTriangles.Count, sizeof(int) * 3);
        triangleBuffer.SetData(facesTriangles);

        // Załadowanie krawędzi
        Vector2Int[] edgesArray = edges.Select(e => new Vector2Int(e.startId, e.endId)).ToArray();
        ComputeBuffer edgesBuffer = new ComputeBuffer(edgesArray.Length, sizeof(int) * 2);
        edgesBuffer.SetData(edgesArray);

        // Bufor wyniku
        Vector3Int[] edgeTriangles = new Vector3Int[facesTriangles.Count];
        ComputeBuffer edgeTrianglesBuffer = new ComputeBuffer(edgeTriangles.Length, sizeof(int) * 3);

        // Wykonanie shadera
        int kernelIndex = meshCalculationsShader.FindKernel("EdgeTriangles");
        meshCalculationsShader.SetInt("edgeCount", edges.Count);
        meshCalculationsShader.SetInt("triangleCount", edgeTriangles.Length);
        meshCalculationsShader.SetBuffer(kernelIndex, "vertexTriangles", triangleBuffer);
        meshCalculationsShader.SetBuffer(kernelIndex, "edges", edgesBuffer);
        meshCalculationsShader.SetBuffer(kernelIndex, "edgeTriangles", edgeTrianglesBuffer);

        uint threadGroupSizeX;
        uint threadGroupSizeY;
        uint threadGroupSizeZ;
        meshCalculationsShader.GetKernelThreadGroupSizes(kernelIndex, out threadGroupSizeX, out threadGroupSizeY, out threadGroupSizeZ);
        meshCalculationsShader.Dispatch(kernelIndex, (int)Mathf.Ceil(facesTriangles.Count / (float)threadGroupSizeX), 1, 1);

        // Pobranie wyniku z shadera: listy trójkątów wyrażonych jako indeksy krawędzi,
        // gdzie -1 oznacza użycie w trójkącie krawędzi, która nie istnieje
        edgeTrianglesBuffer.GetData(edgeTriangles);

        triangleBuffer.Dispose();
        edgesBuffer.Dispose();
        edgeTrianglesBuffer.Dispose();
        return GetFacesForEdges(edgeTriangles, (triangleIndex) => triangleToFace[triangleIndex]);
    }

    /// <summary>
    /// Intersects hyperplane with faces of the polytope
    /// </summary>
    /// <param name="hyperplane"></param>
    /// <returns>A dictionary where: Key = index of the face; Value = intersection points with the edges of the face. </returns>
    public override Dictionary<int, List<Vector4>> FacesIntersections(Hyperplane4 hyperplane)
    {
        List<Vector4> vertices = VerticesWorld;

        Dictionary<int, List<Vector4>> faceIntersections = new Dictionary<int, List<Vector4>>(); // faceIndex -> intersection points

        int kernelIndex = edgeIntersectionShader.FindKernel("IntersectEdges");
        // Load data into the shader
        ComputeBuffer vertexBuffer = new ComputeBuffer(vertices.Count, sizeof(float) * 4);
        vertexBuffer.SetData(vertices);
        edgeIntersectionShader.SetBuffer(kernelIndex, "vertices", vertexBuffer);
        edgeIntersectionShader.SetInt("vertexCount", vertices.Count);

        Vector2Int[] edgesArray = edges.Select(e => new Vector2Int(e.startId, e.endId)).ToArray();
        ComputeBuffer edgeBuffer = new ComputeBuffer(edgesArray.Length, sizeof(int) * 2);
        edgeBuffer.SetData(edgesArray);
        edgeIntersectionShader.SetBuffer(kernelIndex, "edges", edgeBuffer);
        edgeIntersectionShader.SetInt("edgeCount", edgesArray.Length);

        // Load hyperplane
        edgeIntersectionShader.SetFloat("hyperplaneA", hyperplane.A);
        edgeIntersectionShader.SetFloat("hyperplaneB", hyperplane.B);
        edgeIntersectionShader.SetFloat("hyperplaneC", hyperplane.C);
        edgeIntersectionShader.SetFloat("hyperplaneD", hyperplane.D);
        edgeIntersectionShader.SetFloat("hyperplaneE", hyperplane.E);

        // Result buffer
        IntersectionInfo[] intersectionsArray = new IntersectionInfo[edgesArray.Length];
        ComputeBuffer intersectionData = new ComputeBuffer(intersectionsArray.Length, sizeof(int) + sizeof(float) * 4);
        edgeIntersectionShader.SetBuffer(kernelIndex, "intersections", intersectionData);
        // Init result count as 0
        int[] intersectionCountArray = new int[1];
        intersectionCountArray[0] = 0;
        ComputeBuffer intersectionCountBuffer = new ComputeBuffer(intersectionCountArray.Length, sizeof(int));
        intersectionCountBuffer.SetData(intersectionCountArray);
        edgeIntersectionShader.SetBuffer(kernelIndex, "intersectionCount", intersectionCountBuffer);

        // Execute shader
        uint threadGroupSizeX;
        uint threadGroupSizeY;
        uint threadGroupSizeZ;
        edgeIntersectionShader.GetKernelThreadGroupSizes(kernelIndex, out threadGroupSizeX, out threadGroupSizeY, out threadGroupSizeZ);
        edgeIntersectionShader.Dispatch(kernelIndex, (int)Mathf.Ceil(edgesArray.Length / (float)threadGroupSizeX), 1, 1);

        // Get edge intersections from the shader
        intersectionData.GetData(intersectionsArray);
        intersectionCountBuffer.GetData(intersectionCountArray);

        // Clean up
        vertexBuffer.Dispose();
        edgeBuffer.Dispose();
        intersectionData.Dispose();
        intersectionCountBuffer.Dispose();

        return EdgeIntersectionsToFaceIntersections(intersectionsArray, intersectionCountArray[0]);
    }

    private Dictionary<int, List<Vector4>> EdgeIntersectionsToFaceIntersections(IntersectionInfo[] intersections, int intersectionCount)
    {
        Dictionary<int, List<Vector4>> faceIntersections = new Dictionary<int, List<Vector4>>();

        for(int i = 0; i < intersectionCount; i++)
        {
            int edgeIndex = intersections[i].edgeIndex;
            Vector4 intersectionPoint = intersections[i].intersectionPoint;

            // Save the face which the edge belongs to, clone record for every face
            foreach (int faceIndex in GetEdgeFaces(edgeIndex))
            {
                // Add the intersection to the face's record
                if (faceIntersections.ContainsKey(faceIndex))
                {
                    faceIntersections[faceIndex].Add(intersectionPoint);
                }
                else
                {
                    faceIntersections.Add(faceIndex, new List<Vector4> { intersectionPoint });
                }
            }
        }

        return faceIntersections;
    }

    private Dictionary<int, List<int>> GetFacesForEdges(Vector3Int[] edgeTriangles, Func<int, int> triangleToFace)
    {
        // Wprowadzenie danych do słownika
        Dictionary<int, List<int>> facesForEdges = new Dictionary<int, List<int>>(); // edge index -> face indices
        for (int i = 0; i < edgeTriangles.Length; i++)
        {
            Vector3Int edgesInTriangle = edgeTriangles[i];

            int faceIndex = triangleToFace(i);

            Action<int> insertEdgeFaceRelation = (edgeIndex) =>
            {
                if (edgeIndex >= 0)
                {
                    if (!facesForEdges.ContainsKey(edgeIndex))
                        facesForEdges.Add(edgeIndex, new List<int>());

                    facesForEdges[edgeIndex].Add(faceIndex);
                }
            };
            insertEdgeFaceRelation(edgesInTriangle.x);
            insertEdgeFaceRelation(edgesInTriangle.y);
            insertEdgeFaceRelation(edgesInTriangle.z);
        }

        return facesForEdges;
    }


    public static IEnumerable<Vector3Int> FaceToTriangles(List<int> faceVertices)
    {
        for (int i = 2; i < faceVertices.Count; i++)
        {
            yield return new Vector3Int(faceVertices[i - 1], faceVertices[i], faceVertices[0]); // triangle fan
        }
    }

    private struct IntersectionInfo
    {
        public int edgeIndex; // Index of the intersected edge
        public Vector4 intersectionPoint;
    }
}

