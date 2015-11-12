using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Mana : MonoBehaviour {

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
			slider.maxValue = player.GetComponent<Mob> ().stats.maxMana;
			slider.value = player.GetComponent<Mob> ().stats.mana;
			float playerMana = Mathf.Ceil(player.GetComponent<Mob>().stats.mana);
			if (playerMana < 0)
				playerMana = 0;
			text.text = "Mana: " + playerMana.ToString() + " / " + Mathf.Ceil(player.GetComponent<Mob>().stats.maxMana).ToString();
		}
	}
}
