using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiVirusScript : MonoBehaviour {

	// VARIABLES

	[Header("Buttons")]
	public GameObject antiVirusTab;
	public GameObject autoUpdaterButton;
	public GameObject upgradeFirewallButton;
	public GameObject addBlockButton;

	private int updaterCost = 20;
	private int fireWallCost = 10;
	private int formatCost = 50;
	private int addBlockCost = 30;

	private float updaterDuration = 20f;
	private float firewallDuration = 10f;
	private float addBlockInterval = 5f;

	private bool boughtAutoUpdater;

	private VirusSpawner virusSpawner;
	private GameManager gameManager;
	private AudioManager audioManager;

	// FUNCTIONS

	void Start()
	{
		virusSpawner = FindObjectOfType<VirusSpawner> ();
		gameManager = FindObjectOfType<GameManager> ();
		audioManager = FindObjectOfType<AudioManager> ();
	}

	public void OpenAntiVirusTab()
	{
		antiVirusTab.SetActive (true);
		Time.timeScale = 0;
	}

	public void CloseAntiVirusTab()
	{
		antiVirusTab.SetActive (false);
		Time.timeScale = 1;
	}

	IEnumerator AutoUpdater()
	{
		gameManager.playerScore -= updaterCost;

		gameManager.ramValue += 100;

		autoUpdaterButton.SetActive (false);

		yield return new WaitForSeconds (updaterDuration);

		gameManager.ramValue -= 100;

		autoUpdaterButton.SetActive (true);
	}


	IEnumerator FirewallUpgrade()
	{
		gameManager.playerScore -= fireWallCost;

		virusSpawner.spawnLeastWait += 6;
		virusSpawner.spawnMostWait += 10;

		upgradeFirewallButton.SetActive (false);

		yield return new WaitForSeconds(firewallDuration);

		virusSpawner.spawnLeastWait -= 6;
		virusSpawner.spawnMostWait -= 10;

		upgradeFirewallButton.SetActive (true);
	}

	IEnumerator AddBlock()
	{
		gameManager.playerScore -= addBlockCost;

		addBlockButton.SetActive (false);

		while(true) 
		{
			yield return new WaitForSeconds (addBlockInterval);

			Destroy (GameObject.FindGameObjectWithTag ("Virus"));
		}
	}

	public void BuyAutoUpdater ()
	{
		if(gameManager.playerScore >= updaterCost) 
		{
			audioManager.Play ("UpgradeSE");

			StartCoroutine (AutoUpdater ());
		}
		else
		{
			Debug.Log ("Not enough points!");
		}
	}

	public void UpgradeFirewall()
	{
		if(gameManager.playerScore >= fireWallCost) 
		{
			audioManager.Play ("UpgradeSE");

			StartCoroutine (FirewallUpgrade ());
		}
		else
		{
			Debug.Log ("Not enough points!");
		}
	}

	public void KillAllVirus()
	{
		if(gameManager.playerScore >= formatCost) 
		{
			gameManager.playerScore -= formatCost;

			StartCoroutine (FirewallUpgrade ());

			audioManager.Play ("UpgradeSE");

			GameObject[] allViruses = GameObject.FindGameObjectsWithTag ("Virus");

			foreach (GameObject virus in allViruses) 
			{
				Destroy (virus);
			}
		}
		else
		{
			Debug.Log ("Not enough points!");
		}
	}

	public void BuyAddBlock()
	{
		if(gameManager.playerScore >= addBlockCost)
		{
			audioManager.Play ("UpgradeSE");

			StartCoroutine (AddBlock ());
		}
		else
		{
			Debug.Log ("Not enough points!");
		}
	}
}
