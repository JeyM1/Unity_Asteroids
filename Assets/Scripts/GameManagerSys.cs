using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerSys
{

	private static GameManagerSys instance;

	static GameObject deathScreenPrefab;

	public static bool isGamePaused = false;
	public static bool isShowingDeathScreen = false;

	static InGameOverlay inGameOverlay;
	public static bool isInMainMenu
	{
		get
		{
			return SceneManager.GetActiveScene().name == "MainMenu";
		}
	}
	public static bool isSwitchingLevel = false;

	private static int score;
	private static int bestScoreSession;

	public static int Score
	{
		get
		{
			return score;
		}
	}
	public static float PlayTimeSeconds
	{
		get
		{
			return inGameOverlay.PlayTimeSeconds;
		}
	}

	static void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (scene.name != "MainMenu")
		{
			inGameOverlay = GameObject.FindGameObjectWithTag("InGameOverlay").GetComponent<InGameOverlay>();
			StopGameTimer();
			ResetGameScoreAndTimer();
			StartGameTimer();
			isShowingDeathScreen = false;
		}
		isSwitchingLevel = false;
	}

	public static void Initialize()
	{
		if (instance == null)
		{
			instance = new GameManagerSys();
			SceneManager.sceneLoaded += OnLevelFinishedLoading;
			deathScreenPrefab = (GameObject)Resources.Load(@"Prefabs/DeathScreen");
			AudioManager.Initialize();
		}
	}
	
	public static void IncrementScore()
	{
		score++;
		inGameOverlay.UpdateScore(score);
	}

	public static void StopGameTimer()
	{
		inGameOverlay.StopGameTimer();
	}
	public static void StartGameTimer()
	{
		inGameOverlay.StartGameTimer();
	}
	public static void ResetGameScoreAndTimer()
	{
		score = 0;
		inGameOverlay.ResetGameTimer();
	}

	public static void OnPlayerShipDestroyed()
	{
		StopGameTimer();

		isShowingDeathScreen = true;
		DeathScreen deathScreen = GameObject.Instantiate(deathScreenPrefab, Vector3.zero, Quaternion.identity).GetComponent<DeathScreen>();
		deathScreen.ShowDeathScreen();
	}

	public static void PauseGame()
	{
		if (!isGamePaused)
		{
			Time.timeScale = 0;
			inGameOverlay.ShowPauseOverlay();
			isGamePaused = true;
		}
	}

	public static void ResumeGame()
	{
		if(isGamePaused)
		{
			inGameOverlay.HidePauseOverlay();
			isGamePaused = false;
			Time.timeScale = 1;
		}
	}
}
