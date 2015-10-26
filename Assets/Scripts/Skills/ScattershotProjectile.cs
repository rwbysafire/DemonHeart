using UnityEngine;
using System.Collections;

class ScattershotProjectile : MonoBehaviour {
	
	void Update()
	{
		//Destroy (gameObject);
	}
	
	void OnCollisionEnter(Collision collision)
	{
		Destroy (collision.gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log ("Collision Detected");
	}
}

