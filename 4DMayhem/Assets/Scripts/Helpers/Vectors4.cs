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
}
