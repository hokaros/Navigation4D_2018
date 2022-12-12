using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform4))]
public class PerspectiveProjection : MonoBehaviour
{
    [SerializeField] private Hyperplane4Object viewport;
    private Transform4 transform4;

    public static PerspectiveProjection Instance { get; private set; }

    public Vector3? ProjectPerspectively(Vector4 original)
    {
        if (IsBehind(original))
            return null;

        Line4 line = Line4.FromPoints(transform4.GlobalPosition, original);
        Vector4 crossingPoint;
        bool visibleByViewport = viewport.TryCrossingPoint(line, out crossingPoint);

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    //Debug.Log($"camera:{transform4.GlobalPosition}, original:{original}, crossingPoint:{crossingPoint}");
        //}

        if (!visibleByViewport)
            return null;

        if (!IsOnNativeHyperplane(crossingPoint))
            return null;

        Vector4 localPoint = transform4.PointToLocal(crossingPoint);
        if (Mathf.Abs(localPoint.w) > Mathf.Epsilon)
            Debug.LogError($"Point {localPoint} on the native hyperplane has w != 0");

        // Use the projected local point in 3D world space
        Vector3 localPoint3 = new Vector3(localPoint.x, localPoint.y, localPoint.z);
        Debug.Log($"forward4: {transform4.Forward}, forward: {transform.forward}");
        Debug.Log($"Local point: {localPoint3}, camera 3D: (pos: {transform.position}, rot: {transform.rotation}), transformed: {transform.TransformPoint(localPoint3)}");
        return transform.TransformPoint(localPoint3);
        //return transform4.TransformPoint(localPoint);
    }

    public void Init()
    {
        transform4 = GetComponent<Transform4>();
    }

    private bool IsOnNativeHyperplane(Vector4 point)
    {
        Hyperplane4 nativeHyperplane = transform4.GetNativeHyperplane();
        return nativeHyperplane.Contains(point);
    }

    private bool IsBehind(Vector4 point)
    {
        Vector4 cameraForward = transform4.Forward;
        Vector4 cameraToPoint = point - transform4.GlobalPosition;

        float dotProduct = 
            cameraToPoint.x * cameraForward.x +
            cameraToPoint.y * cameraForward.y +
            cameraToPoint.z * cameraForward.z +
            cameraToPoint.w * cameraForward.w;

        if (dotProduct < 0)
            return true;
        return false;
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;

        Init();
    }

    private void OnDestroy()
    {
        if(Instance==this) Instance = null;
    }
}
