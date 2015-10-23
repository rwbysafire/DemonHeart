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
		if (GameObject.FindWithTag ("Player")) {
			GameObject player = GameObject.FindWithTag ("Player");
			slider.value = player.GetComponent<Health> ().health;
			float playerHealth = Mathf.Ceil(player.GetComponent<Health>().health);
			if (playerHealth < 0)
				playerHealth = 0;
			text.text = "Life: " + playerHealth.ToString() + " / " + Mathf.Ceil(player.GetComponent<Health>().maxHealth).ToString();
		}
	}
}
