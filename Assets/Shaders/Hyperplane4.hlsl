struct Line4
{
    float4 direction;
    float4 handle;
};

Line4 lineFromPoints(float4 p1, float4 p2)
{
    Line4 res;
    res.direction = (0, 0, 0, 0);
    res.direction.xyz = p2.xyz - p1.xyz;
    res.direction.w = p2.w - p1.w;
    
    res.handle = p1;
    
    return res;
};


struct Hyperplane4
{
    float a;
    float b;
    float c;
    float d;
    float e;
};

Hyperplane4 hyperplaneFromHandleAndNormal(float4 handle, float4 normal)
{
    normal = normalize(normal);
    
    Hyperplane4 h;
    h.a = normal.x;
    h.b = normal.y;
    h.c = normal.z;
    h.d = normal.w;
    
    h.e = (-h.a * handle.x) - (h.b * handle.y) - (h.c * handle.z) - (h.d * handle.w);
    
    return h;
}

float4 crossingPoint(Hyperplane4 h, Line4 lin)
{
    float x0 = lin.handle.x;
    float y0 = lin.handle.y;
    float z0 = lin.handle.z;
    float w0 = lin.handle.w;
    
    float4 dir = normalize(lin.direction);
    float u1 = dir.x;
    float u2 = dir.y;
    float u3 = dir.z;
    float u4 = dir.w;
    
    float denominator = h.a * u1 + h.b * u2 + h.c * u3 + h.d * u4;
    
    float t = -(h.a * x0 + h.b * y0 + h.c * z0 + h.d * w0 + h.e) / denominator;
    
    float4 cross = (0, 0, 0, 0);
    cross.x = x0 + t * u1;
    cross.y = y0 + t * u2;
    cross.z = z0 + t * u3;
    cross.w = w0 + t * u4;
    
    return cross;
}