using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Health : MonoBehaviour {

	Slider slider;
	Text text;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		text = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find ("Player");
		if (player) {
			slider.maxValue = player.GetComponent<Mob> ().stats.maxHealth;
			slider.value = player.GetComponent<Mob> ().stats.health;
			float playerHealth = Mathf.Ceil(player.GetComponent<Mob> ().stats.health);
			if (playerHealth < 0)
				playerHealth = 0;
			text.text = "Life: " + playerHealth.ToString() + " / " + Mathf.Ceil(player.GetComponent<Mob> ().stats.maxHealth).ToString();
		}
	}
}
