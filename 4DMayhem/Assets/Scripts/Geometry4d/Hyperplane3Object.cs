//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;

//public class Hyperplane3Object : MonoBehaviour
//{
//    [SerializeField] Vector2 size = new Vector2(1, 1);

//    private Hyperplane3 hyperplane;

//    public bool TryCrossingPoint(Line3 line, out Vector3 crossingPoint)
//    {
//        crossingPoint = hyperplane.CrossingPoint(line);

//        Vector3 projectedPoint = transform.InverseTransformPoint(crossingPoint);
//        if (Mathf.Abs(projectedPoint.z) > Mathf.Epsilon)
//            Debug.LogWarning($"local 'z' coordinate of a projected point {projectedPoint} not eqals 0");

//        if (Mathf.Abs(projectedPoint.x) <= size.x / 2 &&
//            Mathf.Abs(projectedPoint.y) <= size.y / 2)
//        {
//            return true;
//        }

//        return false;
//    }

//    public void Init()
//    {
//        hyperplane = new Hyperplane3(transform.position, transform.forward);
//    }

//    private void Awake()
//    {
//        Init();
//    }

//    private void Update()
//    {
//        hyperplane.Update(transform.position, transform.forward);
//    }
//}
