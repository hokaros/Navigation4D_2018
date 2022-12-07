using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LZWPController4D : LZWPControllerBase, IInput4D
{
    public float GetXAxis() => Lzwp.input.flysticks[0].joysticks[0]; // 1st joystick horizontal axis
    public float GetYAxis() => Lzwp.input.flysticks[1].joysticks[1]; // 2nd joystick vertical axis
    public float GetZAxis() => Lzwp.input.flysticks[0].joysticks[1]; // 1st joystick vertical axis
    public float GetWAxis() => Lzwp.input.flysticks[1].joysticks[0]; // 2nd joystick horizontal axis

    public float GetXYRotation() => GetButtonAxis1();
    public float GetXZRotation() => GetButtonAxis2();
    public float GetXWRotation() => GetButtonAxis3();
    public float GetYZRotation() => GetButtonAxis4();
    public float GetYWRotation() => GetButtonAxisFire();
    public float GetZWRotation() => GetButtonAxisJoystick();


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
