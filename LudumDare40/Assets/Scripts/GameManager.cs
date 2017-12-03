using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int playerScore = 0;
	public int playerOTScore = 0;

	public int ramValue = 500;

	private float second = 1f;

	private Text ramNumber;
	public Text playerScoreText;
	public Text playerOTScoreText;

	private AudioManager audioManager;

	void Start()
	{
		ramNumber = GameObject.Find ("RAMNumber").GetComponent<Text> ();
		audioManager = FindObjectOfType<AudioManager> ();

		StartCoroutine (IncreaseScoreOT ());
	}

	void Update()
	{
		playerScoreText.text = playerScore.ToString ();
		playerOTScoreText.text = playerOTScore.ToString ();
		ramNumber.text = ramValue.ToString ();

		if(Input.GetKeyDown(KeyCode.Escape))
		{
			SceneManager.LoadScene ("MenuScene");
		}

		if(ramValue <= 0)
		{
			SceneManager.LoadScene ("GameOverScene");
		}
	}


	IEnumerator IncreaseScoreOT()
	{
		while(true) 
		{
			yield return new WaitForSeconds (second);

			playerOTScore++;
		}
	}

	void OnDestroy()
	{
		PlayerPrefs.SetInt ("playerOTScore", playerOTScore);
		PlayerPrefs.SetInt ("playerScore", playerScore);
	}
}