using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class DeathScreen : MonoBehaviour
{
	[SerializeField]
	TextMeshProUGUI endScoreText;
	[SerializeField]
	TextMeshProUGUI endTimeText;

	private string endScoreTextPrefix = "Total score: ";
	private string endTimeTextPrefix = "Total time: ";
	void Start()
	{
		gameObject.GetComponent<Canvas>().worldCamera = Camera.main;
	}
	public void RestartGame()
	{
		GameManagerSys.isSwitchingLevel = true;
		SceneManager.LoadScene("Scene0");
	}
	public void BackToMainMenu()
	{
		GameManagerSys.isSwitchingLevel = true;
		SceneManager.LoadScene("MainMenu");
	}
	public void ShowDeathScreen()
	{
		endScoreText.text = endScoreTextPrefix + GameManagerSys.Score;
		endTimeText.text = endTimeTextPrefix + TimeSpan.FromSeconds(GameManagerSys.PlayTimeSeconds).ToString(@"mm\:ss");
		gameObject.SetActive(true);
	}
}
