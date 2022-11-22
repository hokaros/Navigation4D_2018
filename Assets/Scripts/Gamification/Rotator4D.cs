using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform4))] 
public class Rotator4D : MonoBehaviour {

	[SerializeField] private float speed;

	private Vector6 rotationStep;

	private Transform4 transform4;
	void Start () {
		transform4 = GetComponent<Transform4>();
		rotationStep = speed * new Vector6(1, 1, 1, 1, 1, 1);
	}
	
	void Update () {
		transform4.Rotation += Time.deltaTime * rotationStep;
	}
}
