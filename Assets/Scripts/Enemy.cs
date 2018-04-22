using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public float MaxSpeed = 100f;
	public float MinSpeed = 40f;
	public Rigidbody rb;
	public static bool hasLostLife = false;

	void Update () 
	{
		float Speed = Random.Range(MinSpeed, MaxSpeed);
		rb.AddForce(0f, 0f, -Speed * Time.deltaTime);

		if(transform.position.z < -10)
		{
			Player.Lives--;
			Player.isDecreased = false;
			hasLostLife = true;
			Destroy(gameObject);
		}
	}
}