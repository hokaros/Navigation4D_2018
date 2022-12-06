using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LZWPCarController : LZWPControllerBase, IInput4D
{
    public float GetZAxis() => GetButtonAxisFire();

    // Disable the rest of movements
    public float GetXAxis() => 0f;
    public float GetYAxis() => 0f;
    public float GetWAxis() => 0f;

    public float GetXYRotation() => Lzwp.input.flysticks[0].joysticks[0]; // 1st joystick horizontal axis

    public float GetXZRotation() => Lzwp.input.flysticks[1].joysticks[0]; // 2nd joystick horizontal axis

    public float GetXWRotation() => GetButtonAxis1();

    public float GetYZRotation() => Lzwp.input.flysticks[0].joysticks[1]; // 1st joystick vertical axis 

    public float GetYWRotation() => Lzwp.input.flysticks[1].joysticks[1]; // 2nd joystick vertical axis

    public float GetZWRotation() => GetButtonAxis3();


    new public bool TriggerRaycast()
    {
        return base.TriggerRaycast();
    }

    new public RaycastHit[] RaycastClick()
    {
        return base.RaycastClick();
    }

    new public bool TriggerMenu()
    {
        return base.TriggerMenu();
    }
}
