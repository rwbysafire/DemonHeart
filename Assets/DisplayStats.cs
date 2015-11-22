using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayStats : MonoBehaviour {

	GameObject display;
	Text level, exp, STR, DEX, INT, health, mana;
	Stats playerStats;

	void Start () {
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Mob>().stats;
		display = transform.FindChild("StatsDisplayBackground").gameObject;
		level = display.transform.FindChild("level").gameObject.GetComponent<Text>();
		exp = display.transform.FindChild("exp").gameObject.GetComponent<Text>();
		STR = display.transform.FindChild("str").gameObject.GetComponent<Text>();
		DEX = display.transform.FindChild("dex").gameObject.GetComponent<Text>();
		INT = display.transform.FindChild("int").gameObject.GetComponent<Text>();
		health = display.transform.FindChild("health").gameObject.GetComponent<Text>();
		mana = display.transform.FindChild("mana").gameObject.GetComponent<Text>();
		display.SetActive(false);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
			display.SetActive(true);
		}
		else if (Input.GetKeyUp(KeyCode.C))
			display.SetActive(false);
		if (display.activeSelf) {
			if (GameObject.FindGameObjectWithTag("Player"))
				playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Mob>().stats;
			level.text = "Level: " + playerStats.level.ToString();
			exp.text = "Exp: " + playerStats.exp.ToString();
			STR.text = "Str: " + playerStats.strength.ToString();
			DEX.text = "Dex: " + playerStats.dexterity.ToString();
			INT.text = "Int: " + playerStats.intelligence.ToString();
			health.text = "Health: " + Mathf.Ceil(playerStats.health).ToString() + " / " + Mathf.Ceil(playerStats.maxHealth).ToString();
			mana.text = "Mana: " + Mathf.Ceil(playerStats.mana).ToString() + " / " + Mathf.Ceil(playerStats.maxMana).ToString();

		}
	}
}
