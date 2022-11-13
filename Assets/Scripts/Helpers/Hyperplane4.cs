using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct Hyperplane4
{
    /// <summary>
    /// Współczynniki równania hiperpłaszczyzny
    /// </summary>
    private float a, b, c, d, e;

    public void Update(Vector4 point, Vector4 normalVector)
    {
        a = normalVector.x;
        b = normalVector.y;
        c = normalVector.z;
        d = normalVector.w;

        e = -a * point.x - b * point.y - c * point.z - d * point.w;
    }

    public Hyperplane4(Vector4 point, Vector4 normalVector)
    {
        a = normalVector.x;
        b = normalVector.y;
        c = normalVector.z;
        d = normalVector.w;

        e = -a * point.x - b * point.y - c * point.z - d * point.w;
    }

public Vector4 CrossingPoint(Line4 line)
    {
        // A*x + B*y + C*z + D*w + E = 0
        // x = x0 + t*u1
        // y = y0 + t*u2
        // z = z0 + t*u3
        // w = w0 + t*u4
        float x0 = line.Point.x;
        float y0 = line.Point.y;
        float z0 = line.Point.z;
        float w0 = line.Point.w;

        float u1 = line.Direction.x;
        float u2 = line.Direction.y;
        float u3 = line.Direction.z;
        float u4 = line.Direction.w;

        float denominator = a * u1 + b * u2 + c * u3 + d * u4;
        
        if (denominator == 0) throw new DivideByZeroException();

        float t = -(a * x0 + b * y0 + c * z0 + d * w0 + e) / denominator;
        Vector4 crossingPoint = new Vector4(x0 + t * u1, y0 + t * u2, z0 + t * u3, w0 + t * u4);
        return crossingPoint;
    }
	
    public Plane4 CrossingPlane(Hyperplane4 other)
    {
        Vector4 normal = new Vector4(a, b, c, d);
        Vector4 otherNormal = new Vector4(other.a, other.b, other.c, other.d);

        float x = -(e + other.e) / (a - (b * other.a / other.b));
        float y = -(e + a * x) / b;
        Vector4 point = new Vector4(x, y, 0, 0);

        Plane4 crossingPlane = new Plane4(point, normal, otherNormal);
        return crossingPlane;
    }

    public bool Contains(Vector4 point)
    {
        return Mathf.Abs(Vector4.Dot(point, new Vector4(a, b, c, d)) + e) < Mathf.Epsilon;
    }

    public override string ToString()
    {
        string xPart = (a >= 0) ? $"{a}x" : $"({a}x)";
        string yPart = (b >= 0) ? $"{b}y" : $"({b}y)";
        string zPart = (c >= 0) ? $"{c}z" : $"({c}z)";
        string wPart = (d >= 0) ? $"{d}w" : $"({d}w)";
        string freePart = (e >= 0) ? $"{e}" : $"({e})";
        return $"{xPart} + {yPart} + {zPart} + {wPart} + {freePart} = 0";
    }
}
