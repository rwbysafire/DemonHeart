using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class DisplayStats : MonoBehaviour {

	public List<Button> ptsButtons;
	GameObject display;
	Text level, exp, Curexp, STR, DEX, INT, health, mana, attackSpeed, cooldown, pts;
	Stats playerStats;
	Buff playerBuff;

	void Start () {
		playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<Mob>().stats;
		display = transform.FindChild("StatsDisplayBackground").gameObject;
		level = display.transform.FindChild("level").gameObject.GetComponent<Text>();
		STR = display.transform.FindChild("str").gameObject.GetComponent<Text>();
		DEX = display.transform.FindChild("dex").gameObject.GetComponent<Text>();
		INT = display.transform.FindChild("int").gameObject.GetComponent<Text>();
		health = display.transform.FindChild("health").gameObject.GetComponent<Text>();
		mana = display.transform.FindChild("mana").gameObject.GetComponent<Text>();
		attackSpeed = display.transform.FindChild("attackSpeed").gameObject.GetComponent<Text>();
		cooldown = display.transform.FindChild("cooldown").gameObject.GetComponent<Text>();
		pts = display.transform.FindChild("pts").gameObject.GetComponent<Text>();
	}

	void Update () {

		if (display.activeSelf) {
			if (GameObject.FindGameObjectWithTag ("Player")) {
				Mob player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Mob> ();
				playerStats = player.stats;
				playerBuff = player.buff;
			}
			level.text = "Level: " + playerStats.level.ToString();
			STR.text = GetDisplayPropertyString ("STR", playerStats.strength, playerBuff.strength + playerStats.strengthActualAddon);
			DEX.text = GetDisplayPropertyString ("DEX", playerStats.dexterity, playerBuff.dexterity + playerStats.dexterityActualAddon);
			INT.text = GetDisplayPropertyString ("INT", playerStats.intelligence, playerBuff.intelligence + playerStats.intelligenceActualAddon);
			health.text = GetDisplayProportionString ("Health", playerStats.health, playerStats.maxHealth, playerBuff.maxHealth);
			mana.text = GetDisplayProportionString ("Mana", playerStats.mana, playerStats.maxMana, playerBuff.maxMana);
			attackSpeed.text = "Atk Speed: " + playerStats.attackSpeed.ToString();
			cooldown.text = "Cooldown: " + playerStats.cooldownReduction.ToString() + "%";
			pts.text = "Available Pts: " + playerStats.pts.ToString();

			bool shouldShowButtons = playerStats.pts > 0; 
			foreach (Button b in ptsButtons) {
				b.interactable = shouldShowButtons;
			}
		}
	}

	private string GetDisplayPropertyString (string name, float statValue, float buffValue) {
		return name + ": " + (statValue - buffValue).ToString () + " (+" + buffValue.ToString () + ")";
	}

	private string GetDisplayProportionString (string name, float statValue, float maxValue, float buffValue) {
		return name + ": " + Math.Ceiling (statValue).ToString () + " / " + Math.Ceiling (maxValue - buffValue).ToString () + " (+" + Math.Ceiling (buffValue).ToString () + ")";
	}
	
	public void setStr(int STR) {
		playerStats.baseStrength += STR;
		playerStats.pts--;
	}
	
	public void setInt(int INT) {
		playerStats.baseIntelligence += INT;
		playerStats.pts--;
	}
	
	public void setDex(int DEX) {
		playerStats.baseDexterity += DEX;
		playerStats.pts--;
	}
}
