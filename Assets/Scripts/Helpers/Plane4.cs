using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct Plane4
{
    /// <summary>
    /// Współczynniki równania płaszczyzny
    /// </summary>
    private Vector4 normal1;
    private Vector4 normal2;
    private Vector4 point;

    public void Update(Vector4 point, Vector4 normal1, Vector4 normal2)
    {
        this.normal1 = normal1;
        this.normal2 = normal2;
        this.point = point;
    }

    public Plane4(Vector4 point, Vector4 normal1, Vector4 normal2)
    {
        this.normal1 = normal1;
        this.normal2 = normal2;
        this.point = point;
    }

    public void SpanningVectors(out Vector4 span1, out Vector4 span2)
    {
        // we need two spanning vectors. For now let's assume it's not an edge case and they are not paralell to any axis.
        // let's choose vectors (?, ?, 1, 2) and (?, ?, 2, 1) among infinity of possible options
        float i, j, k = 1f, l=2f, m, n, o=2f, p=1f;
        float a = normal1.x;
        float b = normal1.y;
        float c = normal1.z;
        float d = normal1.w;
        float e = normal2.x;
        float f = normal2.y;
        float g = normal2.z;
        float h = normal2.w;
        j = (e * c * k + e * d * l - a * g * k - a * h * l) / ( a * f - e * b );
        i = -( b * j + c* k + d * l) / a;
        n = (e * c * o + e * d * p - a * g * o - a * h * p) / (a * f - e * b);
        m = -(b * n + c * o + d * p) / a;

        span1 = new Vector4(i, j, k, l);
        span2 = new Vector4(m, n, o, p);
    }

    public bool Contains(Vector4 point)
    {
        Hyperplane4 h1 = new Hyperplane4(this.point, normal1);
        Hyperplane4 h2 = new Hyperplane4(this.point, normal2);
        return h1.Contains(point) && h2.Contains(point);
    }

    public static bool AreCoplanar(Vector4 a, Vector4 b, Vector4 c, Vector4 d)
    {
        return AreCoplanar(a, b, c, d, Mathf.Epsilon);
    }

    public static bool AreCoplanar(Vector4 a, Vector4 b, Vector4 c, Vector4 d, float tolerance)
    {
        Vector4 handle = a;
        Vector4 spanning1 = b - a;
        Vector4 spanning2 = c - a;

        // d = handle + t*spanning1 + s*spanning2
        // d_x = handle_x + t*spanning1_x + s*spanning2_x
        // d_y = handle_y + t*spanning1_y + s*spanning2_y
        // ...

        // t*spanning1_x = -handle_x - s*spanning2_x + d_x
        // d_y = handle_y + (-handle_x - s*spanning2_x + d_x)*spanning1_y/spanning1_x + s*spanning2_y
        // d_y = handle_y + (-handle_x + d_x)*spanning1_y/spanning1_x + s*(spanning2_y - spanning2_x*spanning1_y/spanning1_x)
        // s = (d_y - handle_y + (handle_x - d_x)*spanning1_y/spanning1_x) / (spanning2_y - spanning2_x*spanning1_y/spanning1_x)

        float s;
        float t;
        if (spanning1.x == 0)
        {
            // d_x = handle_x + 0 + s*spanning2_x
            if(spanning2.x != 0)
            {
                // s = (d_x - handle_x) / spanning2_x
                s = (d.x - handle.x) / spanning2.x;
                // d_y = handle_y + t*spanning1_y + s*spanning2_y
                // d_y - handle_y - s*spanning2_y = t*spanning1_y
                if(spanning1.y == 0)
                {
                    // d_z = handle_z + t*spanning1_z + s*spanning2_z
                    if(spanning1.z == 0)
                    {
                        // d_w = handle_w + t*spanning1_w + s*spanning2_w
                        if(spanning1.w == 0)
                        {
                            // If spanning1 is 0, then point A is equal to point B
                            // Every 3 points are coplanar
                            return true;
                        }
                        else
                        {
                            // d_w - handle_w - s*spanning2_w = t*spanning1_w
                            // t = (d_w - handle_w - s*spanning2_w)/spanning1_w
                            t = (d.w - handle.w - s * spanning2.w) / spanning1.w;
                        }
                    }
                    else
                    {
                        t = (d.z - handle.z - s * spanning2.z) / spanning1.z;
                    }
                }
                else
                {
                    t = (d.y - handle.y - s * spanning2.y) / spanning1.y;
                }
            }
            else
            {
                // spanning1.x == 0 && spanning2.x == 0
                if (Mathf.Abs(d.x - handle.x) > tolerance)
                    return false;
                // 3D case
                Plane plane3d = new Plane(
                    new Vector3(a.y, a.z, a.w),
                    new Vector3(b.y, b.z, b.w),
                    new Vector3(c.y, c.z, c.w)
                    );
                return plane3d.GetDistanceToPoint(new Vector3(d.y, d.z, d.w)) <= tolerance;
            }
        }
        else
        { // spanning1.x != 0
            if(spanning2.y - spanning2.x * spanning1.y / spanning1.x == 0)
            {

                // d_z = handle_z + (-handle_x - s*spanning2_x + d_x)*spanning1_z/spanning1_x + s*spanning2_z
                // d_z = handle_z + (-handle_x + d_x)*spanning1_z/spanning1_x + s*(spanning2_z - spanning2_x*spanning1_z/spanning1_x)
                if(spanning2.z - spanning2.x * spanning1.z / spanning1.x == 0)
                {
                    // d_w = handle_w + (-handle_x - s*spanning2_x + d_x)*spanning1_w/spanning1_x + s*spanning2_w
                    // d_w = handle_w + (-handle_x + d_x)*spanning1_w/spanning1_x + s*(spanning2_w - spanning2_x*spanning1_w/spanning1_x)
                    // s = (d_w - handle_w + (handle_x - d_x)*spanning1_w/spanning1_x) / (spanning2_w - spanning2_x*spanning1_w/spanning1_x)
                    if(spanning2.w - spanning2.x * spanning1.w / spanning1.x == 0)
                    {
                        // Means that:
                        // spanning2.y/spanning2.x == spanning1.y/spanning1.x &&
                        // spanning2.z/spanning2.x == spanning1.z/spanning1.x &&
                        // spanning2.w/spanning2.x == spanning1.w/spanning1.x
                        // i.e. spanning1 and spanning2 are colinear
                        return true;
                    }
                    else
                    {
                        s = (d.w - handle.w + (handle.x - d.x) * spanning1.w / spanning1.x) / (spanning2.w - spanning2.x * spanning1.w / spanning1.x);
                    }
                }
                else
                {
                    s = (d.z - handle.z + (handle.x - d.x) * spanning1.z / spanning1.x) / (spanning2.z - spanning2.x * spanning1.z / spanning1.x);
                }
            }
            else
            {
                // s = (d_y - handle_y + (handle_x - d_x)*spanning1_y/spanning1_x) / (spanning2_y - spanning2_x*spanning1_y/spanning1_x)
                s = (d.y - handle.y + (handle.x - d.x) * spanning1.y / spanning1.x) / (spanning2.y - spanning2.x * spanning1.y / spanning1.x);
            }

            t = (-handle.x - s * spanning2.x + d.x) / spanning1.x;
        }

        Vector4 coplanarD = handle + t * spanning1 + s * spanning2;
        // Compare vectors
        return Vectors4.AreEqual(coplanarD, d, tolerance);
    }
}