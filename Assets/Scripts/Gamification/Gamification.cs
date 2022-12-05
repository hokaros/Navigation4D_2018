using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Timer))]
public class Gamification : MonoBehaviour {

	[SerializeField] private int totalTargets = 10;

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
		collectedTargets = -1;
		timer.Reset();
		timer.Start();
		target.Position = player.Position;
		NextTarget();
    }

	public void ExitToMenu()
    {
		SceneManager.LoadScene("MainMenu");
    }

	public void ResetPosition()
	{
		player.Position = originalPlayerPos;
	}

	void Awake () {
		timer = GetComponent<Timer>();
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
