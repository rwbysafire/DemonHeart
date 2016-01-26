using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Make sure UI is dynamic so that it can work for whatever name/character you are using
public class HealthUI : MonoBehaviour {
	
	Slider slider, slider1;
	Text text;

	//Code commented out below to demonstrate health bar in UITests.unity. Apologies if this breaks something. - Greg
	void Start () {
		slider = GetComponentsInChildren<Slider>()[0];
		slider1 = GetComponentsInChildren<Slider>()[1];
		text = GetComponentInChildren<Text>();
	}

	void Update () {
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (player) {
			setSlider(player.GetComponent<Mob>().stats);
			float playerHealth = Mathf.Ceil(player.GetComponent<Mob> ().stats.health);
			if (playerHealth < 0)
				playerHealth = 0;
			else if (playerHealth > player.GetComponent<Mob>().stats.maxHealth)
				playerHealth = player.GetComponent<Mob>().stats.maxHealth;
			text.text = "HP: " + playerHealth.ToString() + " / " + Mathf.Ceil(player.GetComponent<Mob>().stats.maxHealth).ToString();
		}
	}

	void setSlider(Stats stats) {
		slider.value = (2 * stats.health / stats.maxHealth - 1);
		if (stats.health > stats.maxHealth/2) {
			slider1.value = 1;
		} else {
			slider1.value = stats.health / (stats.maxHealth/2) * 0.67f + 0.33f;
		}
	}
}