using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class LZWPController4D : IInput4D
{
    public float GetXAxis() => Lzwp.input.flysticks[0].joysticks[1]; // 1st joystick horizontal axis
    public float GetYAxis() => Lzwp.input.flysticks[1].joysticks[0]; // 2nd joystick vertical axis
    public float GetZAxis() => Lzwp.input.flysticks[0].joysticks[0]; // 1st joystick vertical axis
    public float GetWAxis() => Lzwp.input.flysticks[1].joysticks[1]; // 2nd joystick horizontal axis

    public float GetXYRotation() => GetButtonAxis1();
    public float GetXZRotation() => GetButtonAxis2();
    public float GetXWRotation() => GetButtonAxis3();
    public float GetYZRotation() => GetButtonAxis4();
    public float GetYWRotation() => GetButtonAxisFire();
    public float GetZWRotation() => GetButtonAxisJoystick();


    private float GetButtonAxis1()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Button1);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Button2);
        return GetButtonAxis(btnPositive, btnNegative);
    }

    private float GetButtonAxis2()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Button3);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Button4);
        return GetButtonAxis(btnPositive, btnNegative);
    }
    private float GetButtonAxis3()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Button1);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Button2);
        return GetButtonAxis(btnPositive, btnNegative);
    }
    private float GetButtonAxis4()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Button3);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Button4);
        return GetButtonAxis(btnPositive, btnNegative);
    }

    private float GetButtonAxisFire()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Fire);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Fire);
        return GetButtonAxis(btnPositive, btnNegative);
    }

    private float GetButtonAxisJoystick()
    {
        LzwpInput.Button btnPositive = Lzwp.input.flysticks[0].GetButton(LzwpInput.Flystick.ButtonID.Joystick);
        LzwpInput.Button btnNegative = Lzwp.input.flysticks[1].GetButton(LzwpInput.Flystick.ButtonID.Joystick);
        return GetButtonAxis(btnPositive, btnNegative);
    }

    private float GetButtonAxis(LzwpInput.Button btnPositive, LzwpInput.Button btnNegative)
    {
        float positive = btnPositive.isActive ? 1f : 0f;
        float negative = btnNegative.isActive ? 1f : 0f;

        return positive - negative;
    }
}
