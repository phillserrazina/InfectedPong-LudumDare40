using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	// VARIABLES

	public Text scoreText;

	private int finalScore;

	void Start()
	{
		finalScore = PlayerPrefs.GetInt ("playerOTScore") + PlayerPrefs.GetInt("playerScore");
	}

	void Update()
	{
		if(SceneManager.GetActiveScene().name == "GameOverScene")
			scoreText.text = "You scored:\n" + finalScore.ToString () + " points";
	}

	public void LoadScene (string sceneName)
	{
		SceneManager.LoadScene (sceneName);
	}

	public void ExitGame()
	{
		Application.Quit ();
	}
}
