using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public struct Line4
{
    /// <summary>
    /// Normalized direction vector
    /// </summary>
    public Vector4 Direction { get; private set; }
    public Vector4 Point { get; private set; }

    public Line4(Vector4 point, Vector4 direction)
    {
        Point = point;
        Direction = direction.normalized;
    }

    public static Line4 FromPoints(Vector4 p1, Vector4 p2)
    {
        Vector4 direction = p2 - p1;
        return new Line4(p1, direction);
    }
}
