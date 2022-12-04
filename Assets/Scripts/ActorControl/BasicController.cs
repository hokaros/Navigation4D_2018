using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform4))]
public class BasicController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float collisionDistance = 1f;

    private Transform4 transform4;
    private Vector4 movement;
    private InputManager inputManager;

   

    private void UpdateRotation()
    {
        transform4.RotateInLocal(Axis.x, Axis.y, inputManager.GetXYRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.x, Axis.z, inputManager.GetXZRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.x, Axis.w, inputManager.GetXWRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.y, Axis.z, inputManager.GetYZRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.y, Axis.w, inputManager.GetYWRotation() * Time.deltaTime * rotationSpeed);
        transform4.RotateInLocal(Axis.z, Axis.w, inputManager.GetZWRotation() * Time.deltaTime * rotationSpeed);
    }

    private void UpdatePosition()
    {
        //Debug.Log($"{Input.GetAxis("Forward")}, {Input.GetAxis("4D_W")}");
        movement = transform4.Forward * inputManager.GetZAxis(); //Input.GetAxis("Vertical");
        movement += transform4.Right * inputManager.GetXAxis(); //Input.GetAxis("Horizontal");
        movement += transform4.Up * inputManager.GetYAxis(); //Input.GetAxis("Forward");
        movement += transform4.WPositive * inputManager.GetWAxis(); // Input.GetAxis("4D_W");
        movement *= Time.deltaTime;
        movement *= movementSpeed;

        Vector3 movement3 = new Vector3(inputManager.GetXAxis(), inputManager.GetYAxis(), inputManager.GetZAxis());
        if (CanMove(movement3))
        {
            transform4.Position += movement;
        }
    }

    private bool CanMove(Vector3 direction)
    {
        bool collision = Physics.Raycast(transform.position, direction.normalized, collisionDistance);
        Debug.DrawRay(transform.position, direction.normalized, Color.red, collisionDistance);

        return !collision;
    }

    private void Awake()
    {
        transform4 = GetComponent<Transform4>();
        inputManager = FindObjectOfType<InputManager>();
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
