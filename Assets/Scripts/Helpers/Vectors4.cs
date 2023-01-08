using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vectors4 
{
    public static Vector4 Zero => new Vector4(0f, 0f, 0f, 0f);
    public static Vector4 Forward => new Vector4(0f, 0f, 1f, 0f);
    public static Vector4 Backward => new Vector4(0f, 0f, -1f, 0f);
    public static Vector4 Left => new Vector4(-1f, 0f, 0f, 0f);
    public static Vector4 Right => new Vector4(1f, 0f, 0f, 0f);
    public static Vector4 Up => new Vector4(0f, 1f, 0f, 0f);
    public static Vector4 Down => new Vector4(0f, -1f, 0f, 0f);
    public static Vector4 WPositive => new Vector4(0f, 0f, 0f, 1f);
    public static Vector4 WNegative => new Vector4(0f, 0f, 0f, -1f);
    public static Vector4 Sum(List<Vector4> vector)
    {
        Vector4 sum = Vector4.zero;
        foreach (Vector4 element in vector) sum += element;
        return sum;
    }


    public static bool AreEqual(Vector4 v1, Vector4 v2)
    {
        return AreEqual(v1, v2, Mathf.Epsilon);
    }

    public static bool AreEqual(Vector4 v1, Vector4 v2, float tolerance)
    {
        if (Mathf.Abs(v1.x - v2.x) > tolerance)
            return false;
        if (Mathf.Abs(v1.y - v2.y) > tolerance)
            return false;
        if (Mathf.Abs(v1.z - v2.z) > tolerance)
            return false;
        if (Mathf.Abs(v1.w - v2.w) > tolerance)
            return false;

        return true;
    }
}
