using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTest : MonoBehaviour
{
    [SerializeField] float spheresRadius = 0.5f;
    [SerializeField] float lookRayLength = 2f;
    [SerializeField] bool showRay = false;

    private Transform4 transform4;

    private void CollectInput()
    {
        transform4.Rotation.Xy += Input.GetAxis("4D_xy") * Time.deltaTime;
        transform4.Rotation.Xz += Input.GetAxis("4D_xz") * Time.deltaTime;
        transform4.Rotation.Xw += Input.GetAxis("4D_xw") * Time.deltaTime;
        transform4.Rotation.Yz += Input.GetAxis("4D_yz") * Time.deltaTime;
        transform4.Rotation.Yw += Input.GetAxis("4D_yw") * Time.deltaTime;
        transform4.Rotation.Zw += Input.GetAxis("4D_zw") * Time.deltaTime;
        transform4.NormalizeRotation();
    }

    void Start()
    {
        transform4 = GetComponent<Transform4>();
    }

    void Update()
    {
        CollectInput();

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
