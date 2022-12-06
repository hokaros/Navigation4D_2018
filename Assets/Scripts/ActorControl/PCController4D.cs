using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PCController4D : IInput4D
{
    private Camera camera;
    public float GetXAxis() => Input.GetAxis("Horizontal");
    public float GetYAxis() => Input.GetAxis("Forward"); 
    public float GetZAxis() => Input.GetAxis("Vertical");
    public float GetWAxis() => Input.GetAxis("4D_W");

    public float GetXYRotation() => Input.GetAxis("4D_xy");
    public float GetXZRotation() => Input.GetAxis("4D_xz");
    public float GetXWRotation() => Input.GetAxis("4D_xw");
    public float GetYZRotation() => Input.GetAxis("4D_yz");
    public float GetYWRotation() => Input.GetAxis("4D_yw");
    public float GetZWRotation() => Input.GetAxis("4D_zw");

    public bool TriggerRaycast() => Input.GetMouseButtonDown(0);

    private void SetCamera()
    {
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("CameraLZWP");
        camera = cameras[0].GetComponent<Camera>();
    }

    public RaycastHit[] RaycastClick()
    {
        if (camera == null) SetCamera();
        return Physics.RaycastAll(GetRay());
    }

    public Ray GetRay()
    {
        if (camera == null) SetCamera();
        return camera.ScreenPointToRay(Input.mousePosition);
    }

    public bool TriggerMenu()
    {
        return Input.GetKeyDown(KeyCode.T);
    }
}
