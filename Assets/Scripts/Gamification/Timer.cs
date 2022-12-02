using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	private float value = 0.0f;
	private bool timerEnabled = false;

    public float Value { get => value; }

    public void Stop()
    {
		timerEnabled = false;
    }

	public void Resume()
    {
		timerEnabled = true;
    }

	public void Start()
    {
		value = 0.0f;
		timerEnabled = true;
    }

	public void Reset()
    {
		timerEnabled = false;
		value = 0.0f;
    }
	
	void Update () {
        if (timerEnabled)
        {
			value += Time.deltaTime;
        }
	}
}
