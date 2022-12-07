using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {

	[SerializeField] private float getDistance = 1f;
	[SerializeField] private Transform4 player;

	private Gamification gamification;
	private Transform4 transform4;

	void Awake () {
		gamification = FindObjectOfType<Gamification>();
		transform4 = GetComponent<Transform4>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector4.Distance(transform4.Position, player.Position) <= getDistance)
        {
			gamification.NextTarget();
        }
	}
}
