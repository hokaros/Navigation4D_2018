using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class TransformExtensions
{
    public static Transform4 GetParent4(this Transform transform)
    {
        if (transform.parent == null)
            return null;

        Transform4 parent4 = transform.parent.GetComponent<Transform4>();
        if (parent4 == null)
            Debug.LogWarning("Parent is not null but parent4 is");
        return parent4;
    }
}
