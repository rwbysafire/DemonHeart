using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ManaUI : MonoBehaviour {
	
	Slider slider;
	Text text;
	
	// Use this for initialization
	void Start () {
		slider = GetComponentInChildren<Slider>();
		text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find ("Player");
		if (player) {
			slider.value = player.GetComponent<Mob>().stats.mana / player.GetComponent<Mob>().stats.maxMana;
			float playerMana = Mathf.Ceil(player.GetComponent<Mob> ().stats.mana);
			if (playerMana < 0)
				playerMana = 0;
			else if (playerMana > player.GetComponent<Mob>().stats.maxMana)
				playerMana = player.GetComponent<Mob>().stats.maxMana;
			text.text = "MP: " + playerMana.ToString() + " / " + Mathf.Ceil(player.GetComponent<Mob>().stats.maxMana).ToString();
		}
	}
}