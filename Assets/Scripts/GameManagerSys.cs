using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManagerSys : MonoBehaviour
{
	InGameOverlay inGameOverlay;
	public bool isInMainMenu
	{
		get
		{
			return SceneManager.GetActiveScene().name == "MainMenu";
		}
	}

	private int score;
	private int bestScoreSession;

    void Start()
    {
		DontDestroyOnLoad(gameObject);
	}

	void OnEnable()
	{
		//Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		//Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}

	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		if(scene.name != "MainMenu")
		{
			inGameOverlay = GameObject.FindGameObjectWithTag("InGameOverlay").GetComponent<InGameOverlay>();
		}
	}

	public void UpdateScore()
	{
		score++;
		inGameOverlay.UpdateScore(score);
	}

}
