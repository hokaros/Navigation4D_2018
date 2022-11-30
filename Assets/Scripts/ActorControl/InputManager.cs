using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	[SerializeField] private bool useLZWPInput = true;

	private PCController4D pcInput = new PCController4D();
	private LZWPController4D lzwpInput = new LZWPController4D();
	private IInput4D input => useLZWPInput ? (IInput4D)lzwpInput : (IInput4D)pcInput;

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
}
