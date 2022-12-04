using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller3D : MonoBehaviour 
{
	[SerializeField] float movementSpeed = 1f;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (Input.GetKey(KeyCode.W))
        {
			transform.position += Vector3.forward * Time.deltaTime * movementSpeed;
        }
	}
}
