using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerSys
{

	private static GameManagerSys instance;

	static InGameOverlay inGameOverlay;
	public static bool isInMainMenu
	{
		get
		{
			return SceneManager.GetActiveScene().name == "MainMenu";
		}
	}

	private static int score;
	private static int bestScoreSession;

	static void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if (scene.name != "MainMenu")
		{
			inGameOverlay = GameObject.FindGameObjectWithTag("InGameOverlay").GetComponent<InGameOverlay>();
		}
	}

	public static void Initialize()
	{
		if (instance == null)
		{
			instance = new GameManagerSys();
			SceneManager.sceneLoaded += OnLevelFinishedLoading;
			Debug.Log("Constructing GameManager");
		}
	}

	
	public static void IncrementScore()
	{
		score++;
		inGameOverlay.UpdateScore(score);
	}

}
