using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PointerRaycast : MonoBehaviour {

	private InputManager inputManager;
	private RaycastHit[] hits;

	void ManageRaycastingMouse()
    {

    }

	void ManageRaycastingLZWP()
	{

	}

	void Awake () {
		inputManager = FindObjectOfType<InputManager>();
	}
	
	void Update () {
        if (inputManager.TriggerRaycast())
        {
			hits = inputManager.RaycastClick();
			if (hits.Length <= 0) { }

			var sorted = hits.ToList()
				.FindAll(hit => hit.collider.gameObject.GetComponent<IClickable>() != null);

			if (sorted.Count > 0)
			{
				sorted.Sort((first, second) => first.distance.CompareTo(second.distance));
				sorted[0].collider.GetComponent<IClickable>().OnClick();
			}
		}
	}
}
