using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform4))]
public class BasicController : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float movementSpeed = 1.0f;
    [SerializeField] private float collisionDistance = 1f;
    [SerializeField] private bool collidingHead = true;
    [SerializeField] private bool collidingFeet = true;

    private Transform4 transform4;
    private Vector4 movement;
    private InputManager inputManager;

    private Camera[] cameras;

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

        if (collidingFeet)
        {
            Vector3 feetPosition = LzwpOrigin.GetPosition();

            bool collision = Physics.Raycast(feetPosition, direction, collisionDistance);
            Debug.DrawRay(feetPosition, direction.normalized, Color.red, collisionDistance);
            if (collision)
                return false;
        }

        if (collidingHead)
        {
            Vector3 headPosition = Lzwp.display.pointsOfView[0].position;

            bool collision = Physics.Raycast(headPosition, direction, collisionDistance);
            Debug.DrawRay(headPosition, direction.normalized, Color.red, collisionDistance);
            if (collision)
                return false;
        }

        return true;
    }

    private void Awake()
    {
        transform4 = GetComponent<Transform4>();
        inputManager = FindObjectOfType<InputManager>();
    }

    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
    }

    void Update()
    {
        UpdateRotation();
        UpdatePosition();
    }
}
