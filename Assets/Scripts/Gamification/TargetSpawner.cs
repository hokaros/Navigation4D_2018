using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour {

	[SerializeField] private Grid4D grid;
	[SerializeField] private GameObject targetPrefab;

	public List<Vector4> targetPositions;

	void Start () {
		foreach(Vector4 pos in targetPositions)
        {
			GameObject newTarget = Instantiate(targetPrefab, transform);
			Transform4 t4 = newTarget.GetComponent<Transform4>();
			t4.Position = pos;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
