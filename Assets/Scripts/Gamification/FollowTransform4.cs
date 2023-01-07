using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Transform4))]
public class FollowTransform4 : MonoBehaviour {

	[SerializeField] private Transform4 followedTransform;
	[SerializeField] private Vector4 offset;
	[SerializeField] bool correctHeadPosition;

	private Transform4 transform4;
	private Vector4 headOffset4;

	void Awake()
    {
		transform4 = GetComponent<Transform4>();
    }
	
	void LateUpdate () {
        if (correctHeadPosition)
        {
			Vector3 headOffset = Lzwp.display.pointsOfView[0].position;
			headOffset4 = new Vector4(headOffset.x, headOffset.y, headOffset.z, 0);
			transform4.Position = followedTransform.PointToWorld(offset + headOffset4);
		}
        else
        {
			transform4.Position = followedTransform.PointToWorld(offset);
		}
	}
}
