using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class DisplayStats : MonoBehaviour {

	GameObject display;
	Text level, exp, Curexp, STR, DEX, INT, health, mana, attackSpeed, cooldown;
	Stats playerStats;
	Buff playerBuff;

	void Start () {
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Mob>().stats;
		display = transform.FindChild("StatsDisplayBackground").gameObject;
		level = display.transform.FindChild("level").gameObject.GetComponent<Text>();
		exp = display.transform.FindChild("exp").gameObject.GetComponent<Text>();
		Curexp = display.transform.FindChild("CurrentExp").gameObject.GetComponent<Text>();
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
		if (Input.GetKeyDown(KeyCode.X)) {
			if (Pause.IsPaused () && display.activeSelf) {
				// resume the game
				Pause.ResumeGame ();
				display.SetActive(false);
			} else if (!Pause.IsPaused () && !display.activeSelf) {
				// pause the game
				Pause.PauseGame ();
				display.SetActive(true);
			}
		}

		if (display.activeSelf) {
			if (GameObject.FindGameObjectWithTag ("Player")) {
				Mob player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Mob> ();
				playerStats = player.stats;
				playerBuff = player.buff;
			}
			level.text = "Level: " + playerStats.level.ToString();
			exp.text = "Exp: " + playerStats.exp.ToString();
			Curexp.text = "CurrentExp: " + playerStats.CurExp.ToString();
			STR.text = GetDisplayPropertyString ("STR", playerStats.strength, playerBuff.strength + playerStats.strengthActualAddon);
			DEX.text = GetDisplayPropertyString ("DEX", playerStats.dexterity, playerBuff.dexterity + playerStats.dexterityActualAddon);
			INT.text = GetDisplayPropertyString ("INT", playerStats.intelligence, playerBuff.intelligence + playerStats.intelligenceActualAddon);
			health.text = GetDisplayProportionString ("Health", playerStats.health, playerStats.maxHealth, playerBuff.maxHealth);
			mana.text = GetDisplayProportionString ("Mana", playerStats.mana, playerStats.maxMana, playerBuff.maxMana);
			attackSpeed.text = "Attack Speed: " + playerStats.attackSpeed.ToString();
			cooldown.text = "cooldown:     " + playerStats.cooldownReduction.ToString() + "%";
		}
	}

	private string GetDisplayPropertyString (string name, float statValue, float buffValue) {
		return name + ": " + (statValue - buffValue).ToString () + " (+" + buffValue.ToString () + ")";
	}

	private string GetDisplayProportionString (string name, float statValue, float maxValue, float buffValue) {
		return name + ": " + Math.Ceiling (statValue).ToString () + " / " + Math.Ceiling (maxValue - buffValue).ToString () + " (+" + Math.Ceiling (buffValue).ToString () + ")";
	}
	
	public void setLevel(int lvl) {
		GameObject.Find("Player").GetComponent<Mob>().stats.level += lvl;
	}
	
	public void setStr(int STR) {
		GameObject.Find("Player").GetComponent<Mob>().stats.baseStrength += STR;
	}
	
	public void setInt(int INT) {
		GameObject.Find("Player").GetComponent<Mob>().stats.baseIntelligence += INT;
	}
	
	public void setDex(int DEX) {
		GameObject.Find("Player").GetComponent<Mob>().stats.baseDexterity += DEX;
	}
}
