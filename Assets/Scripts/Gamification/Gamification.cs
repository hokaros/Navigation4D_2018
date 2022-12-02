using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamification : MonoBehaviour {

	[SerializeField] private Transform4 player;
	[SerializeField] private Transform4 target;
	[SerializeField] private float targetSpawnRadius;

	[SerializeField] private Text xPlayerPosText;
	[SerializeField] private Text yPlayerPosText;
	[SerializeField] private Text zPlayerPosText;
	[SerializeField] private Text wPlayerPosText;

	[SerializeField] private Text xTargetPosText;
	[SerializeField] private Text yTargetPosText;
	[SerializeField] private Text zTargetPosText;
	[SerializeField] private Text wTargetPosText;


	private void UpdatePlayerPositionLabels()
    {
		xPlayerPosText.text = player.Position.x.ToString("F2");
		yPlayerPosText.text = player.Position.y.ToString("F2");
		zPlayerPosText.text = player.Position.z.ToString("F2");
		wPlayerPosText.text = player.Position.w.ToString("F2");
    }

	private void UpdateTargetPositionLabels()
	{
		xTargetPosText.text = target.Position.x.ToString("F2");
		yTargetPosText.text = target.Position.y.ToString("F2");
		zTargetPosText.text = target.Position.z.ToString("F2");
		wTargetPosText.text = target.Position.w.ToString("F2");
	}

	private void NextTarget()
    {
		Vector4 newTargetPos = player.Position + new Vector4(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * targetSpawnRadius;
		target.Position = newTargetPos;
		UpdateTargetPositionLabels();
    }

	void Start () {
		
	}
	
	void Update () {
		UpdatePlayerPositionLabels();
        if (Input.GetKeyDown(KeyCode.Y))
        {
			NextTarget();
        }
	}
}
