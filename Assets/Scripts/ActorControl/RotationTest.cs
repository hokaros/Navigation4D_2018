using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    [SerializeField] float spheresRadius = 0.5f;
    [SerializeField] float lookRayLength = 2f;
    [SerializeField] bool showRay = false;

    private Transform4 transform4;


    void Start()
    {
        transform4 = GetComponent<Transform4>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector4 testPoint = new Vector4(2, 3, 4, 5);

            Vector4 worldPoint = transform4.PointToWorld(testPoint);
            Vector4 outPoint = transform4.PointToLocal(worldPoint);
            Debug.Log($"In = {testPoint}. World = {worldPoint}. Out = {outPoint}");

        }
    }

    private void OnDrawGizmos()
    {
        if (!showRay)
            return;

        transform4 = GetComponent<Transform4>();
        Vector3 forward = transform4.Forward;
        forward = forward.normalized * lookRayLength;

        Vector3 globalPos = transform4.GlobalPosition;

        Vector3 lookPoint = transform4.PointToWorld(Vectors4.Forward*lookRayLength);

        Gizmos.DrawSphere(globalPos, spheresRadius);
        Gizmos.DrawRay(globalPos, forward);

        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(lookPoint, spheresRadius/2);
    }
}
