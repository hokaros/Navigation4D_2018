using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[RequireComponent(typeof(Transform4))]
public class Hyperplane4Object : MonoBehaviour
{
    [SerializeField] Vector3 size = new Vector3(1, 1, 1);

    private Transform4 transform4;
    private Hyperplane4 hyperplane;

    public bool TryCrossingPoint(Line4 line, out Vector4 crossingPoint)
    {
        crossingPoint = hyperplane.CrossingPoint(line);

        Vector4 projectedPoint = transform4.PointToLocal(crossingPoint);
        if (Mathf.Abs(projectedPoint.w) > Mathf.Epsilon) 
            Debug.LogWarning($"local 'w' coordinate of a projected point {projectedPoint} not eqals 0");

        if(Mathf.Abs(projectedPoint.x)<= size.x/2 && 
            Mathf.Abs(projectedPoint.y) <= size.y / 2 && 
            Mathf.Abs(projectedPoint.z) <= size.z / 2)
        {
            return true;
        }

        return false;
    }

    public void Init()
    {
        transform4 = GetComponent<Transform4>();
        hyperplane = new Hyperplane4(transform4.GlobalPosition, transform4.Forward);
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        hyperplane.Update(transform4.GlobalPosition, transform4.Forward);
    }
}
