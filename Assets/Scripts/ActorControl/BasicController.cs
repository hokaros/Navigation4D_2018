using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform4))]
public class BasicController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private bool useLZWPInput = true;

    private Transform4 transform4;
    private Vector4 movement;

    private PCController4D pcInput = new PCController4D();
    private LZWPController4D lzwpInput = new LZWPController4D();
    private IInput4D input => useLZWPInput ? (IInput4D)lzwpInput : (IInput4D)pcInput;

    private void UpdateRotation()
    {
        transform4.RotateInLocal(Axis.x, Axis.y, input.GetXYRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.x, Axis.z, input.GetXZRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.x, Axis.w, input.GetXWRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.y, Axis.z, input.GetYZRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.y, Axis.w, input.GetYWRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.z, Axis.w, input.GetZWRotation() * Time.deltaTime * rotationSpeed);
    }

    private void UpdatePosition()
    {
        //Debug.Log($"{Input.GetAxis("Forward")}, {Input.GetAxis("4D_W")}");
        movement = transform4.Forward * input.GetZAxis(); //Input.GetAxis("Vertical");
        movement += transform4.Right * input.GetXAxis(); //Input.GetAxis("Horizontal");
        movement += transform4.Up * input.GetYAxis(); //Input.GetAxis("Forward");
        movement += transform4.WPositive * input.GetWAxis(); // Input.GetAxis("4D_W");
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