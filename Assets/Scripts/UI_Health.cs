using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Health : MonoBehaviour {
	
	GameObject player;
	Slider slider;
	Text text;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		text = GetComponentInChildren<Text> ();
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag ("Player")) {
			slider.value = player.GetComponent<Health> ().health;
			text.text = "Life: " + (player.GetComponent<Health>().health).ToString() + " / " + (player.GetComponent<Health>().maxHealth).ToString();
		}
	}
}
