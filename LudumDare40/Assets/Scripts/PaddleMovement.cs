using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour {

	public float speed = 30f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float vertPress = Input.GetAxisRaw ("Vertical");

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, vertPress) * speed;
	}
}
