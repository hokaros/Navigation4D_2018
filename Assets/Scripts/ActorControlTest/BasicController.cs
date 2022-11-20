using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform4))]
public class BasicController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float movementSpeed = 1.0f;

    private Transform4 transform4;
    private Vector4 movement;

    private void UpdateRotation()
    {
        transform4.Rotation.Xy += Input.GetAxis("4D_xy") * Time.deltaTime * rotationSpeed;
        transform4.Rotation.Xz += Input.GetAxis("4D_xz") * Time.deltaTime * rotationSpeed;
        transform4.Rotation.Xw += Input.GetAxis("4D_xw") * Time.deltaTime * rotationSpeed;
        transform4.Rotation.Yz += Input.GetAxis("4D_yz") * Time.deltaTime * rotationSpeed;
        transform4.Rotation.Yw += Input.GetAxis("4D_yw") * Time.deltaTime * rotationSpeed;
        transform4.Rotation.Zw += Input.GetAxis("4D_zw") * Time.deltaTime * rotationSpeed;
        transform4.NormalizeRotation();
    }

    private void UpdatePosition()
    {
        //Debug.Log($"{Input.GetAxis("Forward")}, {Input.GetAxis("4D_W")}");
        movement = transform4.Forward * Lzwp.input.flysticks[0].joysticks[0]; //Input.GetAxis("Vertical");
        movement += transform4.Right * Lzwp.input.flysticks[0].joysticks[1]; //Input.GetAxis("Horizontal");
        movement += transform4.Up * Lzwp.input.flysticks[1].joysticks[0]; //Input.GetAxis("Forward");
        movement += transform4.WPositive * Lzwp.input.flysticks[1].joysticks[1]; // Input.GetAxis("4D_W");
        movement *= Time.deltaTime;
        movement *= movementSpeed;
        transform4.Position += movement;
    }

    private void Awake()
    {
        transform4 = GetComponent<Transform4>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }
}
