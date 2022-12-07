using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Timer))]
public class Gamification : MonoBehaviour {

	[SerializeField] private int totalTargets = 10;
	[SerializeField] private int layersOfObstacles = 2;

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

	[SerializeField] private Text timerText;
	[SerializeField] private Text targetCountText;

	[SerializeField] private GameObject startButton;

	private Timer timer;
	private int collectedTargets = 0;
	private bool gameIsOn = false;
	private Vector4 originalPlayerPos;
	private WallGenerator wallGenerator;

	public Vector4 PlayerHeadPosition()
	{
		Vector3 headOffsetLocal = Lzwp.display.pointsOfView[0].position - LzwpOrigin.GetPosition();
		Vector4 playerHeadPos = player.PointToWorld(new Vector4(headOffsetLocal.x, headOffsetLocal.y, headOffsetLocal.z, 0));
		return playerHeadPos;
	}


	private void UpdatePlayerPositionLabels()
    {
		Vector4 playerPos = PlayerHeadPosition();
		xPlayerPosText.text = playerPos.x.ToString("F2");
		yPlayerPosText.text = playerPos.y.ToString("F2");
		zPlayerPosText.text = playerPos.z.ToString("F2");
		wPlayerPosText.text = playerPos.w.ToString("F2");
    }

	private void UpdateTargetPositionLabels()
	{
		xTargetPosText.text = target.Position.x.ToString("F2");
		yTargetPosText.text = target.Position.y.ToString("F2");
		zTargetPosText.text = target.Position.z.ToString("F2");
		wTargetPosText.text = target.Position.w.ToString("F2");
	}

	public void FirstTarget()
    {
		Vector4 newTargetPos = player.Position + new Vector4(0f, 0f, 30f, 0f);
		target.Position = newTargetPos;
		UpdateTargetPositionLabels();
		wallGenerator.ClearWalls();
		wallGenerator.SpawnWalls(player.Position, newTargetPos, layersOfObstacles);
	}

	public void NextTarget()
    {
		Vector4 newTargetPos = player.Position + new Vector4(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * targetSpawnRadius;
		originalPlayerPos = target.Position;
		target.Position = newTargetPos;
		UpdateTargetPositionLabels();
		collectedTargets++;
		targetCountText.text = $"{collectedTargets}/{totalTargets}";
		if(collectedTargets >= totalTargets)
        {
			EndGame();
        }
        else
        {
			wallGenerator.ClearWalls();
			wallGenerator.SpawnWalls(player.Position, newTargetPos, layersOfObstacles);
			timer.Resume();
		}	
    }

	private void EndGame()
    {
		timer.Stop();
		targetCountText.text = "Result:";
		startButton.SetActive(true);
		gameIsOn = false;
	}

	public void StartGame()
    {
		gameIsOn = true;
		startButton.SetActive(false);
		collectedTargets = 0;
		timer.Reset();
		timer.Start();
		FirstTarget();
    }

	public void ExitToMenu()
    {
		Lzwp.sync.LoadScene("MainMenu");
    }

	public void ResetPosition()
	{
		player.Position = originalPlayerPos;
	}

	void Awake () {
		timer = GetComponent<Timer>();
		wallGenerator = FindObjectOfType<WallGenerator>();
	}
	
	void Update () {
		UpdatePlayerPositionLabels();

        if (gameIsOn)
        {
			timerText.text = timer.Value.ToString("F2");
			if (Input.GetKeyDown(KeyCode.Y))
			{
				NextTarget();
			}
		}
	}
}
