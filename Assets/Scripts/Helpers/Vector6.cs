using UnityEngine;
using System;

/// <summary>
/// Representation of a six-dimensional vector
/// </summary>
[Serializable]
public class Vector6
{
    [SerializeField] float xy;
    [SerializeField] float xz;
    [SerializeField] float xw;
    [SerializeField] float yz;
    [SerializeField] float yw;
    [SerializeField] float zw;

    public float Xy { get => xy; set => xy = value; }
    public float Xz { get => xz; set => xz = value; }
    public float Xw { get => xw; set => xw = value; }
    public float Yz { get => yz; set => yz = value; }
    public float Yw { get => yw; set => yw = value; }
    public float Zw { get => zw; set => zw = value; }

    public float this[int index]
    {
        get
        {
            switch (index)
            {
                case 0: return xy;
                case 1: return xz;
                case 2: return xw;
                case 3: return yz;
                case 4: return yw;
                case 5: return zw;
                default:
                    throw new IndexOutOfRangeException("Vector6 has only 6 coordinates");
            }
        }
        set
        {
            switch (index)
            {
                case 0:
                    xy = value;
                    break;
                case 1:
                    xz = value;
                    break;
                case 2:
                    xw = value;
                    break;
                case 3:
                    yz = value;
                    break;
                case 4:
                    yw = value;
                    break;
                case 5:
                    zw = value;
                    break;
                default:
                    throw new IndexOutOfRangeException("Vector6 has only 6 coordinates");
            }
        }
    }

    public override string ToString()
    {
        return $"({xy:F4}, {xz:F4}, {xw:F4}, {yz:F4}, {yw:F4}, {zw:F4})";
    }

    public static Vector6 operator +(Vector6 v1, Vector6 v2)
    {
        Vector6 result = new Vector6();
        for (int i = 0; i < 6; i++)
        {
            result[i] = v1[i] + v2[i];
        }

        return result;
    }

    public static Vector6 operator *(float f, Vector6 v)
    {
        Vector6 result = new Vector6();
        for (int i = 0; i < 6; i++)
        {
            result[i] = v[i] * f;
        }

        return result;
    }

    public static Vector6 operator -(Vector6 v)
    {
        Vector6 result = new Vector6();
        for (int i = 0; i < 6; i++)
        {
            result[i] = -v[i];
        }

        return result;
    }
}
