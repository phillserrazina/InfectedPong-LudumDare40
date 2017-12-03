using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallScript : MonoBehaviour {

	private int startingDirection;

	private float speed = 75f;

	private Vector3 paddleToBallVector;
	private Vector3 playerStartPosition;

	private Rigidbody2D rb;

	private AudioManager audioManager;

	private GameManager gameManager;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();
		audioManager = FindObjectOfType<AudioManager> ();
		gameManager = FindObjectOfType<GameManager> ();

		if(Random.value < 0.5f)
		{
			startingDirection = 1;
		}
		else
		{
			startingDirection = -1;
		}

		rb.velocity = new Vector2 (startingDirection, 0f) * speed;
	}

	void Update()
	{
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if((col.gameObject.name == "LeftPaddle") || (col.gameObject.name == "RightPaddle"))
		{
			HandlePaddleHit (col);
		}

		if((col.gameObject.name == "LeftWall") || (col.gameObject.name == "RightWall"))
		{
			if(col.gameObject.name == "RightWall")
			{
				IncreaseScore ("playerScore", 10);
			}

			if(col.gameObject.name == "LeftWall")
			{
				IncreaseScore ("enemyScore", 10);
			}
		}
	}

	void HandlePaddleHit(Collision2D col)
	{
		float y = BallHitPaddleWhere (transform.position, col.transform.position, col.collider.bounds.size.y);

		Vector2 direction = new Vector2 ();

		if(col.gameObject.name == "LeftPaddle")
		{
			direction = new Vector2 (1, y).normalized;
			audioManager.Play("PaddleSE");
		}

		if(col.gameObject.name == "RightPaddle")
		{
			direction = new Vector2 (-1, y).normalized;
			audioManager.Play("PaddleSE");
		}

		rb.velocity = direction * speed;
	}

	float BallHitPaddleWhere(Vector2 ball, Vector2 paddle, float paddleHeight)
	{
		return (ball.y - paddle.y) / paddleHeight;
	}

	void IncreaseScore(string whoseScore, int scoreAmmount)
	{
		if(whoseScore == "playerScore")
		{
			gameManager.playerScore += scoreAmmount;
		}
		else if (whoseScore == "enemyScore")
		{
			SceneManager.LoadScene ("GameOverScene");
		}
			
		this.transform.position = new Vector3 (0f, 0f, 0f);

		if(Random.value < 0.5f)
		{
			startingDirection = 1;
		}
		else
		{
			startingDirection = -1;
		}

		rb.velocity = new Vector2 (startingDirection, 0f) * speed;
	}
}
