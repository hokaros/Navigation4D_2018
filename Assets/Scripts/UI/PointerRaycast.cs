using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PointerRaycast : MonoBehaviour {

	[SerializeField] private float rayLength;
	[SerializeField] private float enabledWidth, disabledWidth;

	private InputManager inputManager;
	private RaycastHit[] hits;
	private LineRenderer lineRenderer;

	void Awake () {
		inputManager = FindObjectOfType<InputManager>();
		lineRenderer = GetComponent<LineRenderer>();
		SetWidth(disabledWidth);
	}

	private void SetWidth(float width)
    {
		lineRenderer.startWidth = width;
		lineRenderer.endWidth = width;
	}

	private void UpdateTransform()
    {
		transform.position = inputManager.GetPointerPosition();
		transform.rotation = inputManager.GetPointerRotation();
		Vector3[] positions = { transform.position, transform.position + transform.forward * rayLength };
		lineRenderer.SetPositions(positions);
	}

	private void OnTriggerRaycast()
    {
		SetWidth(enabledWidth);

		hits = inputManager.RaycastClick();
		foreach (RaycastHit hit in hits)
		{
			Button button = hit.collider.GetComponent<Button>();
			if (button != null)
			{
				button.onClick.Invoke();
			}
		}
		if (hits.Length <= 0) { }

		var sorted = hits.ToList()
			.FindAll(hit => hit.collider.gameObject.GetComponent<IClickable>() != null);

		if (sorted.Count > 0)
		{
			sorted.Sort((first, second) => first.distance.CompareTo(second.distance));
			sorted[0].collider.GetComponent<IClickable>().OnClick();
		}
	}
	
	void Update () {
		UpdateTransform();
        if (inputManager.TriggerRaycast())
        {
			OnTriggerRaycast();
        }
        else
        {
			SetWidth(disabledWidth);
        }
	}
}
