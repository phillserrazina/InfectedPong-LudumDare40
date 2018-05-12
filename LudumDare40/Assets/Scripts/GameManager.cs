using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// VARIABLES

	public int playerScore = 0;
	public int playerOTScore = 0;

	public int ramValue = 500;

	public Text playerScoreText;
	public Text playerOTScoreText;
	public GameObject pauseMenu;

	private float second = 1f;
	private bool gameIsPaused = false;

	private Text ramNumber;
	private AntiVirusScript antiVirus;
	private AudioManager audioManager;

	// METHODS

	void Start()
	{
		ramNumber = GameObject.Find ("RAMNumber").GetComponent<Text> ();
		audioManager = FindObjectOfType<AudioManager> ();

		try 
		{
			antiVirus = FindObjectOfType<AntiVirusScript>();	
		} 
		catch
		{
			Debug.LogWarning ("No Anti Virus found!");
		}

		StartCoroutine (IncreaseScoreOT ());
	}

	void Update()
	{
		playerScoreText.text = playerScore.ToString ();
		playerOTScoreText.text = playerOTScore.ToString ();
		ramNumber.text = ramValue.ToString ();

		if( (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) ) && !gameIsPaused)
		{
			PauseGame ();
			gameIsPaused = true;
		} 
		else if ( (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) ) && gameIsPaused)
		{
			UnpauseGame ();
			gameIsPaused = false;
		}

		if(antiVirus.antiVirusTab.activeSelf == true && Input.GetKeyDown(KeyCode.Q))
		{
			antiVirus.CloseAntiVirusTab ();
		}

		else if(antiVirus.antiVirusTab.activeSelf == false && Input.GetKeyDown(KeyCode.Q))
		{
			antiVirus.OpenAntiVirusTab ();
		}


		if(ramValue <= 0)
		{
			SceneManager.LoadScene ("GameOverScene");
		}
	}


	/// <summary>
	/// Gives the player a point per second.
	/// </summary>
	IEnumerator IncreaseScoreOT()
	{
		while(true) 
		{
			yield return new WaitForSeconds (second);

			playerOTScore++;
		}
	}

	/// <summary>
	/// Pauses the game.
	/// </summary>
	public void PauseGame()
	{
		// TODO: Play SE
		pauseMenu.SetActive (true);
		audioManager.MuteAudio ();
		Time.timeScale = 0.0f;
	}

	/// <summary>
	/// Unpauses the game.
	/// </summary>
	public void UnpauseGame()
	{
		pauseMenu.SetActive (false);
		audioManager.UnmuteAudio ();
		Time.timeScale = 1.0f;
	}

	// Save the score.
	void OnDestroy()
	{
		PlayerPrefs.SetInt ("playerOTScore", playerOTScore);
		PlayerPrefs.SetInt ("playerScore", playerScore);
	}
}