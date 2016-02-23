using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DisplayStats : MonoBehaviour {

	GameObject display;
	Text level, exp, STR, DEX, INT, health, mana, attackSpeed, cooldown;
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
		attackSpeed = display.transform.FindChild("attackSpeed").gameObject.GetComponent<Text>();
		cooldown = display.transform.FindChild("cooldown").gameObject.GetComponent<Text>();
		display.SetActive(false);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
			if (Pause.IsPaused ()) {
				// resume the game
				Pause.ResumeGame ();
				display.SetActive(false);
			} else {
				// pause the game
				Pause.PauseGame ();
				display.SetActive(true);
			}
		}

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
			attackSpeed.text = "Attack Speed: " + playerStats.attackSpeed.ToString();
			cooldown.text = "cooldown:     " + playerStats.cooldownReduction.ToString() + "%";
		}
	}
	
	public void setLevel(int lvl) {
		GameObject.Find("Player").GetComponent<Mob>().stats.level += lvl;
	}
	
	public void setStr(int STR) {
		GameObject.Find("Player").GetComponent<Mob>().stats.strength += STR;
	}
	
	public void setInt(int INT) {
		GameObject.Find("Player").GetComponent<Mob>().stats.intelligence += INT;
	}
	
	public void setDex(int DEX) {
		GameObject.Find("Player").GetComponent<Mob>().stats.dexterity += DEX;
	}
}
