using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputType { PcInput, LzwpFree, LzwpCar }

public class InputManager : MonoBehaviour 
{
	[SerializeField] InputType inputType = InputType.PcInput;

	private PCController4D pcInput = new PCController4D();
	private LZWPController4D lzwpInput = new LZWPController4D();
	private LZWPCarController lzwpCarInput = new LZWPCarController();

	private IInput4D input
    {
        get
        {
            switch (inputType)
            {
				case InputType.LzwpFree:
					return lzwpInput;
				case InputType.LzwpCar:
					return lzwpCarInput;
				default:
					return pcInput;
            }
        }
    }

	public float GetXAxis() { return input.GetXAxis(); }
	public float GetYAxis() { return input.GetYAxis(); }
	public float GetZAxis() { return input.GetZAxis(); }
	public float GetWAxis() { return input.GetWAxis(); }

	public float GetXYRotation() { return input.GetXYRotation(); }
	public float GetXZRotation() { return input.GetXZRotation(); }
	public float GetXWRotation() { return input.GetXWRotation(); }
	public float GetYZRotation() { return input.GetYZRotation(); }
	public float GetYWRotation() { return input.GetYWRotation(); }
	public float GetZWRotation() { return input.GetZWRotation(); }

	public bool TriggerMenu() { return input.TriggerMenu(); }
	public bool TriggerRaycast() { return input.TriggerRaycast(); }
	public RaycastHit[] RaycastClick() { return input.RaycastClick(); }

	public Ray GetRay() { return input.GetRay(); }
	public void DisableController()
    {
		BasicController controller = GetComponent<BasicController>();
        if (controller != null)
        {
			controller.enabled = false;
        }
    }

	public void EnableController()
	{
		BasicController controller = GetComponent<BasicController>();
		if (controller != null)
		{
			controller.enabled = true;
		}
	}
}
