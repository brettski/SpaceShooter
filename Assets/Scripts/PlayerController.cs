﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate = 0.5f;

	private float nextFire = 0.0f;
	private GameController gameController;

	void Start()
	{
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if(gameControllerObject != null)
			gameController = gameControllerObject.GetComponent<GameController>();
		if(gameController == null)
			Debug.Log ("Cannot find 'GameController' script");
	}
	
	void Update()
	{
		if(Input.GetKey("right shift") && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			gameController.AddShot();
			//GameObject clone = 
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation); //as GameObject;
			audio.Play ();
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3
		(
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f, 
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
		);
		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x * -tilt);
	}


}
