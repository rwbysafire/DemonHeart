using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthUI : MonoBehaviour {
	
	Slider slider, slider1;
	Text text;
	
	// Use this for initialization
	void Start () {
		slider = GetComponentsInChildren<Slider>()[0];
		slider1 = GetComponentsInChildren<Slider>()[1];
		text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find ("Player");
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
		slider.value = 2 * stats.health / stats.maxHealth - 1;
		if (stats.health > stats.maxHealth/2)
			slider1.value = 0.653f;
		else
			slider1.value = 1.306f * stats.health / stats.maxHealth;
	}
}