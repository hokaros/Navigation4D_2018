using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class PCController4D : IInput4D
{ 
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

    public RaycastHit[] RaycastClick()
    {
        Debug.Log(GameObject.FindGameObjectsWithTag("MainCamera").Length);
        return Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition));
    }
}
