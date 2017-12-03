using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusPopScript : MonoBehaviour {

	// This is the script that will handle the viruses,
	// each consume a portion of ram while they're opened, 
	// it's destroyed once it's closed and the RAM is given back.

	// VARIABLES

	private int virusHP;
	private int ramStealValue;

	private int popUpRAMSteal = 50;
	private int addRAMSteal = 75;
	private int virusRAMSteal = 100;

	private PlayerScript player;
	private VirusSpawner virusSpawner;
	private GameManager gameManager;

	// FUNCTIONS

	void Start()
	{
		player = FindObjectOfType<PlayerScript> ();
		virusSpawner = FindObjectOfType<VirusSpawner> ();
		gameManager = FindObjectOfType<GameManager> ();

		if(this.gameObject.name == "BasicPopUp(Clone)")
		{
			this.virusHP = 1;
			ramStealValue = popUpRAMSteal;
		}
		else if(this.gameObject.name == "BasicAddBody(Clone)")
		{
			this.virusHP = 2;
			ramStealValue = addRAMSteal;
		}
		else if(this.gameObject.name == "BasicVirusBody(Clone)")
		{
			this.virusHP = 3;
			ramStealValue = virusRAMSteal;
		}

		gameManager.ramValue -= ramStealValue;
	}
		
	public void ClosePopUp()
	{
		this.virusHP--;

		if(this.virusHP <= 0) 
		{	
			Destroy (this.gameObject);
		}
	}

	void OnDestroy()
	{
		virusSpawner.virusCount--;

		gameManager.playerOTScore += 2;

		gameManager.ramValue += ramStealValue;
	}
}
