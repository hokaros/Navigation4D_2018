using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Applies Transform4 values to the original Transform
/// </summary>
[ExecuteInEditMode]
public class Transform4To3 : MonoBehaviour
{
    private Transform4 transform4;

    private Vector3 prevPos;

    public void Init()
    {
        prevPos = transform.position;

        transform4 = GetComponent<Transform4>();
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        Vector3 dPos = (Vector3)transform4.GlobalPosition - transform.position;
        transform.position += dPos;
        transform.forward = transform4.Forward;

        prevPos = transform.position;
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        UpdateTransform();
    }
}
