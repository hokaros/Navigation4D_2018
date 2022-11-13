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

    public void Init()
    {
        transform4 = GetComponent<Transform4>();
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        transform.position = transform4.GlobalPosition;
        transform.forward = transform4.Forward;
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
