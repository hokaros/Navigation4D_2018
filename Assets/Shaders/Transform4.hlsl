struct float6
{
    float xy;
    float xz;
    float xw;
    float yz;
    float yw;
    float zw;
};

float4 getRotatedPoint(float4 p, float angle, int axis1, int axis2)
{
    int a1 = min(axis1, axis2);
    int a2 = max(axis1, axis2);

    float4 result = p;
    float s = sin(angle);
    float c = cos(angle);

    result[a1] = p[a1] * c + p[a2] * (-s);
    result[a2] = p[a1] * s + p[a2] * c;

    return result;
}

float4 getRotatedPoint(float4 po, float6 rotation)
{
    float4 p = po;
    p = getRotatedPoint(p, rotation.xy, 0, 1);
    p = getRotatedPoint(p, rotation.xz, 0, 2);
    p = getRotatedPoint(p, rotation.xw, 0, 3);
    p = getRotatedPoint(p, rotation.yz, 1, 2);
    p = getRotatedPoint(p, rotation.yw, 1, 3);
    p = getRotatedPoint(p, rotation.zw, 2, 3);
    return p;
}

float4 getUnrotatedPoint(float4 po, float6 rotation)
{
    float4 p = po;
    p = getRotatedPoint(p, -rotation.zw, 2, 3);
    p = getRotatedPoint(p, -rotation.yw, 1, 3);
    p = getRotatedPoint(p, -rotation.yz, 1, 2);
    p = getRotatedPoint(p, -rotation.xw, 0, 3);
    p = getRotatedPoint(p, -rotation.xz, 0, 2);
    p = getRotatedPoint(p, -rotation.xy, 0, 1);

    return p;
}


float4 transformToWorld(float4 vertex, float4 position, float6 rotation)
{
    float4 rotatedPoint = getRotatedPoint(
        vertex,
        rotation
    );

    float4 worldPos = rotatedPoint + position;
    // Not applying the parent's transform
    return worldPos;
}

float4 transformToLocal(float4 vertex, float4 position, float6 rotation)
{
    float4 toPointWorld = vertex - position;
    return getUnrotatedPoint(
        toPointWorld,
        rotation
    );

}