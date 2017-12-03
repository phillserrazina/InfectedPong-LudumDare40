using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPaddle : MonoBehaviour {

	public BallScript ball;

	public float speed = 20f;

	public float lerpTweak = 2f;

	private Rigidbody2D rb;
	private GameManager gameManager;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		gameManager = FindObjectOfType<GameManager> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if(40 > gameManager.playerScore && gameManager.playerScore >= 20)
		{
			speed = 30f;
		}
		else if(gameManager.playerScore >= 40)
		{
			speed = 50f;
		}

		if(ball.transform.position.y > transform.position.y)
		{
			Vector2 direction = new Vector2 (0, 1).normalized;

			rb.velocity = Vector2.Lerp (rb.velocity, direction * speed, lerpTweak * Time.deltaTime);
		}
		else if(ball.transform.position.y < transform.position.y)
		{
			Vector2 direction = new Vector2 (0, -1).normalized;

			rb.velocity = Vector2.Lerp (rb.velocity, direction * speed, lerpTweak * Time.deltaTime);
		}
		else
		{
			Vector2 direction = new Vector2 (0, 0).normalized;

			rb.velocity = Vector2.Lerp (rb.velocity, direction * speed, lerpTweak * Time.deltaTime);
		}
	}
}
