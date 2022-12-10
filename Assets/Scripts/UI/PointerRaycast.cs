using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PointerRaycast : MonoBehaviour {

	[SerializeField] private float rayLength;
	[SerializeField] private float enabledWidth, disabledWidth;
	[SerializeField] private GameObject targetMarker;

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
		Ray ray = inputManager.GetRay();
		transform.position = ray.origin;
		transform.forward = ray.direction;
		Vector3[] positions = { transform.position, transform.position + transform.forward * rayLength };
		lineRenderer.SetPositions(positions);
	}

	private void MarkTarget()
    {
		hits = inputManager.RaycastClick();

		var sorted = hits.ToList();
		if (sorted.Count > 0)
		{
			sorted.Sort((first, second) => first.distance.CompareTo(second.distance));

			targetMarker.transform.position = hits[hits.Length - 1].point;
			targetMarker.SetActive(true);
		}
		else
        {
			
			targetMarker.SetActive(false);
		}
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
		MarkTarget();
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
