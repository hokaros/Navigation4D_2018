#include "Transform4.hlsl"
#include "Hyperplane4.hlsl"

float4 normalize4(float4 vec)
{
    float4 normalized = vec;
    
    float len = length(vec);
    if (len == 0)
        return vec;
    
    normalized.x /= len;
    normalized.y /= len;
    normalized.z /= len;
    normalized.w /= len;
    return normalized;
}


float4 projectPerspectively(float4 vertex, float4 cameraPos, float6 cameraRotation, Hyperplane4 viewport, float4 viewportPos)
{
    Line4 lin = lineFromPoints(cameraPos, vertex);
    
    float4 cross = crossingPoint(viewport, lin);
    return cross;
    
    //: clipping
    // World-space crossing point to the viewport's local coords
    float4 localPoint = transformToLocal(cross, viewportPos, cameraRotation);
    return localPoint;
}

bool isPointBehind(float4 cameraPos, float4 cameraForward, float4 p)
{
    float4 cameraToPoint = p - cameraPos;

    float dotProduct =
            cameraToPoint.x * cameraForward.x +
            cameraToPoint.y * cameraForward.y +
            cameraToPoint.z * cameraForward.z +
            cameraToPoint.w * cameraForward.w;

    if (dotProduct < 0)
        return true;
    return false;
}