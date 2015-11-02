using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float maxHealth, health, regen;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}

	// Update is called once per frame
	void Update () {
		health += regen * Time.deltaTime;
		if (health > maxHealth)
			health = maxHealth;
		if (health <= 0) {
			Destroy (gameObject);
		}
	}

	public void hurt (float damage) {
		health -= damage;
	}
}
