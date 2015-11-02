using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float damage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionStay2D (Collision2D collision) {
		if (collision.collider.tag == "Player")
			collision.gameObject.GetComponent<Health>().hurt(damage);
	}
}
