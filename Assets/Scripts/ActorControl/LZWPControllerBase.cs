using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class LZWPControllerBase
{
    protected float GetButtonAxis1()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Button1);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Button2);
        return GetButtonAxis(btnPositive, btnNegative);
    }

    protected float GetButtonAxis2()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Button3);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Button4);
        return GetButtonAxis(btnPositive, btnNegative);
    }
    protected float GetButtonAxis3()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Button1);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Button2);
        return GetButtonAxis(btnPositive, btnNegative);
    }
    protected float GetButtonAxis4()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Button3);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Button4);
        return GetButtonAxis(btnPositive, btnNegative);
    }

    protected float GetButtonAxisFire()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Fire);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Fire);
        return GetButtonAxis(btnPositive, btnNegative);
    }

    protected float GetButtonAxisJoystick()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Joystick);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Joystick);
        return GetButtonAxis(btnPositive, btnNegative);
    }

    protected float GetButtonAxis(LzwpInput.Button btnPositive, LzwpInput.Button btnNegative)
    {
        float positive = btnPositive.isActive ? 1f : 0f;
        float negative = btnNegative.isActive ? 1f : 0f;

        return positive - negative;
    }

    protected RaycastHit[] RaycastClick()
    {
        // TODO: test in LZWP
        Ray ray = GetRay();
        return Physics.RaycastAll(ray);
    }

    public Ray GetRay()
    {
        LzwpPose flystick = Lzwp.input.flysticks[1].pose;
        Vector3 origin = flystick.position;
        Vector3 direction = flystick.rotation * Vector3.forward;
        Ray ray = new Ray(origin, direction);
        return ray;
    }
    public bool TriggerRaycast()
    {
        // TODO: test in LZWP
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Fire);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Fire);
        return btnNegative.isActive && btnPositive.isActive;
    }

    public bool TriggerMenu()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Joystick);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Joystick);
        return (btnPositive.isActive && btnNegative.isActive);
    }
}
