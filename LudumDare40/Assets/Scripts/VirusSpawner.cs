using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour {

	// VARIABLES

	public GameObject[] virusList;

	public bool isSpawning = true;

	public int startWait;
	public int virusCount = 0;

	public float spawnWait;
	public float spawnMostWait;
	public float spawnLeastWait;

	private float spreeDuration = 0.5f;
	private int randomVirusChoice;
	private int availableVirus;

	private float spawnPosLeast = -100f;
	private float spawnPosMost = 100f;

	private bool isHarder = false;
	private bool isHarder2 = false;
	private bool isHarder3 = false;

	private Vector3 spawnValues;
	private GameManager gameManager;
	private AudioManager audioManager;

	// FUNCTIONS

	// Use this for initialization
	void Start () 
	{
		gameManager = FindObjectOfType<GameManager> ();
		audioManager = FindObjectOfType<AudioManager> ();

		StartCoroutine (SpawnerWaitTime ());
	}

	// Update is called once per frame
	void Update () 
	{
		spawnWait = Random.Range (spawnLeastWait, spawnMostWait);

		if (gameManager.playerOTScore < 20)
		{
			availableVirus = 1;
		}
		else if (gameManager.playerOTScore >= 20 && gameManager.playerOTScore < 50)
		{
			availableVirus = 2;
		}
		else if (gameManager.playerOTScore >= 50)
		{
			availableVirus = 3;
		}
	}

	IEnumerator SpawnerWaitTime()
	{
		yield return new WaitForSeconds (startWait);

		while (true) 
		{
			randomVirusChoice = Random.Range (0, availableVirus);

			Vector3 spawnPosition = new Vector3 (Random.Range (spawnPosLeast, spawnPosMost), 
				Random.Range (spawnPosLeast, spawnPosMost), 0f);

			if (this.isSpawning) 
			{
				Instantiate (virusList [randomVirusChoice], spawnPosition, gameObject.transform.rotation);

				virusCount++;

				VirusIntensityHandle ();
				// VirusAttack ();

				audioManager.Play ("VirusSE");
			}
			yield return new WaitForSeconds (spawnWait);
		}
	}


	void VirusIntensityHandle()
	{
		// Handle intensity through the number of existing
		// viruses.
		if(virusCount > 2 && isHarder == false)
		{
			spawnLeastWait -= 0.5f;
			spawnMostWait -= 0.5f;

			isHarder = true;
		}
		else if (virusCount <= 2 && isHarder == true)
		{
			spawnLeastWait += 0.5f;
			spawnMostWait += 0.5f;

			isHarder = false;
		}


		// Handle intensity through player's score
		if((gameManager.playerOTScore >= 20 && gameManager.playerOTScore < 50) && isHarder2 == false)
		{
			spawnLeastWait -= 0.5f;
			spawnMostWait -= 0.5f;

			isHarder2 = true;
		}

		if((gameManager.playerOTScore >= 50) && isHarder3 == false)
		{
			spawnLeastWait -= 0.5f;
			spawnMostWait -= 0.5f;

			isHarder3 = true;
		}
	}

	/*void VirusAttack()
	{
		if(Random.value < 0.1f)
		{
			StartCoroutine (VirusSpree ());
		}
	}

	IEnumerator VirusSpree()
	{
		float tempLeastWait = spawnLeastWait;
		float tempMostWait = spawnMostWait;

		spawnLeastWait = 0.1f;
		spawnMostWait = 0.2f;

		yield return new WaitForSeconds (spreeDuration);

		spawnLeastWait = tempLeastWait;
		spawnMostWait = tempMostWait;
	}*/
}
