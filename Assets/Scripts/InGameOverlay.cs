using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class InGameOverlay : MonoBehaviour
{
	[SerializeField]
	List<GameObject> showOnPause = new List<GameObject>();

	TextMeshProUGUI scoreText;
	TextMeshProUGUI timeText;

	float playTimeSeconds = 0f;
	public bool isTimerStopped { get; private set; } = false;

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
		GameObject[] inGameTextObj = GameObject.FindGameObjectsWithTag("InGameData");
		scoreText = inGameTextObj[0].GetComponent<TextMeshProUGUI>();
		timeText = inGameTextObj[1].GetComponent<TextMeshProUGUI>();
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

	public void StopGameTimer()
	{
		isTimerStopped = true;
	}
	public void StartGameTimer()
	{
		isTimerStopped = false;
	}
	public void ResetGameTimer()
	{
		playTimeSeconds = 0;
	}

	public void HidePauseOverlay()
	{
		foreach (GameObject item in showOnPause)
		{
			item.SetActive(false);
		}
	}
	public void ShowPauseOverlay()
	{
		foreach (GameObject item in showOnPause)
		{
			item.SetActive(true);
		}
	}

	public void BackToMainMenu()
	{
		HidePauseOverlay();
		GameManagerSys.isSwitchingLevel = true;
		SceneManager.LoadScene("MainMenu");
	}
}
