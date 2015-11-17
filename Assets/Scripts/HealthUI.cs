using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUI : MonoBehaviour {
	
	Slider slider, slider1;
	Text text;

	[Header("Value between 0 and 100")]
	public float initialHealth;
	public float decay;

	float health;

	//Code commented out below to demonstrate health bar in UITests.unity. Apologies if this breaks something. - Greg
	void Start () {
		slider = GetComponentsInChildren<Slider>()[0];
		slider1 = GetComponentsInChildren<Slider>()[1];
		//text = GetComponentInChildren<Text>();

		health = initialHealth;

		//Initialize slider values
		slider.value = Mathf.Lerp(0.0f, 0.67f, ((health / 100)-0.5f)*2.0f);
		slider1.value = Mathf.Lerp (0f, 1f, (health / 100)*2.0f);
	}

	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		health -= decay;

		if (health < 0) {
			health = 0;
		}
		if (health > 100) {
			health = 100;
		}

		//if (player) {
		//	setSlider(player.GetComponent<Mob>().stats);
		//	float playerHealth = Mathf.Ceil(player.GetComponent<Mob> ().stats.health);
		//	if (playerHealth < 0)
		//		playerHealth = 0;
		//	else if (playerHealth > player.GetComponent<Mob>().stats.maxHealth)
		//		playerHealth = player.GetComponent<Mob>().stats.maxHealth;
		//	text.text = "HP: " + playerHealth.ToString() + " / " + Mathf.Ceil(player.GetComponent<Mob>().stats.maxHealth).ToString();
		//}

		if (health / 100 >= 0.5) {
			// 0.92 to 0.25
			slider.value = Mathf.Lerp(0.0f, 0.67f, ((health / 100)-0.5f)*2.0f);
		} else {
			// 1.0 to 0.0
			slider1.value = Mathf.Lerp (0f, 1f, (health / 100)*2.0f);
		}
	}

	void setSlider(Stats stats) {
		//slider.value = 2 * stats.health / stats.maxHealth - 1;
		//if (stats.health > stats.maxHealth/2)
		//	slider1.value = 0.653f;
		//else
		//	slider1.value = 1.306f * stats.health / stats.maxHealth;
	}
}