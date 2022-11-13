using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public interface IShaderVisualizer
{
    void UpdateFaceOpacity(float opacity);
    void UpdateEdgeWidth(float edgeWidth);
    void UpdateTransform(Transform selfTransform);

    /// <summary>
    /// Tells if the mesh has to be recalculated every frame
    /// </summary>
    bool IsMeshDynamic { get; }
    Bounds MeshBounds { get; }
    Material[] Materials { get; }
    Mesh InitializeMesh(float edgeWidth);
    void UpdateMaterialProperties();

}
