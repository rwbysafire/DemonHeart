using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public int maxHealth, health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	// Update is called once per frame
	void Update () {
		if (health <= 0)
			Destroy(gameObject);
	}

	public void hurt (int damage) {
		health -= damage;
	}
}
