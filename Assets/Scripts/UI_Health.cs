using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Health : MonoBehaviour {

	Slider slider;
	Text text;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		slider.maxValue = GameObject.Find ("Player").GetComponent<Mob> ().stats.maxHealth;
		text = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.Find ("Player")) {
			GameObject player = GameObject.Find ("Player");
			slider.value = player.GetComponent<Mob> ().stats.health;
			float playerHealth = Mathf.Ceil(player.GetComponent<Mob> ().stats.health);
			if (playerHealth < 0)
				playerHealth = 0;
			text.text = "Life: " + playerHealth.ToString() + " / " + Mathf.Ceil(player.GetComponent<Mob> ().stats.maxHealth).ToString();
		}
	}
}
