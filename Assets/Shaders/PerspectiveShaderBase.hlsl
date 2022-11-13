#include "PerspectiveProjection.hlsl"

// Parameters for shader stages
struct appdata
{
    float4 vertex : POSITION;
};

struct v2g
{
    float4 vertex : SV_POSITION;
    nointerpolation bool shouldRender : TEXCOORD;
    float opacity : TEXTCOORD;
};

struct g2f
{
    float4 position : SV_POSITION;
    float opacity : TEXTCOORD;
};

// Input properties
float4 _MainTex_ST;

float4 _Position;
float4 _Offset;

float _FogAttenuationDistance;

float _RotationXy;
float _RotationXz;
float _RotationXw;
float _RotationYz;
float _RotationYw;
float _RotationZw;

float4 _CameraPosition;
float _CameraRotationXy;
float _CameraRotationXz;
float _CameraRotationXw;
float _CameraRotationYz;
float _CameraRotationYw;
float _CameraRotationZw;
float4 _CameraForward;

float _ViewportDistance;

float4 _Color;


float4 transformToWorld(float4 vertex)
{
    float6 rotation =
    {
        _RotationXy,
                    _RotationXz,
                    _RotationXw,
                    _RotationYz,
                    _RotationYw,
                    _RotationZw
    };

    return transformToWorld(vertex, _Position, rotation);
}

float3 project4to3(float4 vertex)
{
    float6 cameraRotation =
    {
        _CameraRotationXy,
                    _CameraRotationXz,
                    _CameraRotationXw,
                    _CameraRotationYz,
                    _CameraRotationYw,
                    _CameraRotationZw
    };

    float4 cameraForward = (0, 0, 0, 0);
    cameraForward.z = 1;

    float4 cameraViewportHandle = cameraForward * _ViewportDistance;

    float4 worldViewportHandle = transformToWorld(cameraViewportHandle, _CameraPosition, cameraRotation);

    float4 worldViewportNormal = transformToWorld(cameraForward, _CameraPosition, cameraRotation) - _CameraPosition;

    Hyperplane4 viewport = hyperplaneFromHandleAndNormal(worldViewportHandle, worldViewportNormal);

    float4 projectedPoint = projectPerspectively(vertex, _CameraPosition, cameraRotation, viewport, worldViewportHandle);

    float4 projectedPointCameraSpace = transformToLocal(projectedPoint, _CameraPosition, cameraRotation);
    return projectedPointCameraSpace.xyz + _CameraPosition.xyz;
}


// Vertex shader
v2g vert(appdata v)
{
    v2g o;

    v.vertex = transformToWorld(v.vertex);

    o.shouldRender = !isPointBehind(_CameraPosition, _CameraForward, v.vertex);

    o.opacity = clamp((_FogAttenuationDistance - sqrt(pow(_CameraPosition.x - v.vertex.x, 2) + pow(_CameraPosition.y - v.vertex.y, 2) + pow(_CameraPosition.z - v.vertex.z, 2) + pow(_CameraPosition.w - v.vertex.w, 2))) / _FogAttenuationDistance, 0, 1);


    float3 v3 = project4to3(v.vertex);

    v3 += _Offset;

    o.vertex = UnityObjectToClipPos(v3);

    return o;
}

// Pixel shader
fixed4 frag(g2f i) : SV_Target
{
    fixed4 col = _Color;
    col.w = i.opacity;
    return col;
}