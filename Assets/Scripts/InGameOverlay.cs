using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InGameOverlay : MonoBehaviour
{
	TextMeshProUGUI scoreText;
	TextMeshProUGUI timeText;

	float playTimeSeconds = 0f;
	public bool isTimerStopped = false;

	public float PlayTimeSeconds
	{
		get
		{
			return playTimeSeconds;
		}
	}
		

	private string scoreTextPrefix = "Score: ";
	private string timeTextPrefix = "Time: ";

	void Start()
    {
		TextMeshProUGUI[] allText = GetComponentsInChildren<TextMeshProUGUI>();
		scoreText = allText[0];
		timeText = allText[1];
		scoreText.text = scoreTextPrefix + "0";
		timeText.text = timeTextPrefix + "0:00";
	}

	public void UpdateScore(int score)
	{
		scoreText.text = scoreTextPrefix + score;
	}


    void FixedUpdate()
    {
		if(!isTimerStopped)
		{
			playTimeSeconds += Time.fixedDeltaTime;
			timeText.text = timeTextPrefix + TimeSpan.FromSeconds(playTimeSeconds).ToString(@"mm\:ss");
		}
    }
}
