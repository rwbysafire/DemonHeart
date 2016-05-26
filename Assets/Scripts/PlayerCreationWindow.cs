using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerCreationWindow: MonoBehaviour {

	GameObject display;
	Text STR, DEX, INT, pts;
	Stats playerStats;
	List<int> CountSkills = new List<int> ();

	public List<Button> ptsButtons;
	
	void Start () {
		display = transform.FindChild ("PlayerCreationBackground").gameObject;
		STR = display.transform.FindChild ("str").gameObject.GetComponent<Text> ();
		DEX = display.transform.FindChild ("dex").gameObject.GetComponent<Text> ();
		INT = display.transform.FindChild ("int").gameObject.GetComponent<Text> ();
		pts = display.transform.FindChild ("pts").gameObject.GetComponent<Text> ();
		playerStats = GameObject.Find ("Player").GetComponent<Mob> ().stats;

		if (PlayerPrefs.GetInt (CharSaveLoadScript.PREFS_LOAD_GAME) == 0) {
			Pause.PauseGame ();
			display.SetActive (true);
			playerStats.pts = Stats.InitPoints;
		}

	}

	public void play()
	{
		if (CountSkills.Count == 5) 
		{
			Pause.ResumeGame ();
		}
	}

	public void setstr(int STR) {
		playerStats.baseStrength += STR;
		playerStats.pts--;
	}

	public void setint(int INT) {
		playerStats.baseIntelligence += INT;
		playerStats.pts--;
	}

	public void setdex(int DEX) {
		playerStats.baseDexterity += DEX;
		playerStats.pts--;
	}

	void Update () {
		
		STR.text = "STR:    " + Mathf.Ceil(playerStats.baseStrength).ToString();
		DEX.text = "DEX:    " + Mathf.Ceil(playerStats.baseDexterity).ToString();
		INT.text = "INT:    " + Mathf.Ceil(playerStats.baseIntelligence).ToString();
		pts.text = "Available Pts: " + playerStats.pts.ToString ();

		bool shouldShowButtons = playerStats.pts > 0;
		foreach (Button b in ptsButtons) {
			b.interactable = shouldShowButtons;
		}


		if(!Pause.IsPaused())
			display.SetActive(false);

	}

	public void FullSkills(int index)
	{
		bool f = false;
		for (int i = 0; i < CountSkills.Count; i++) 
		{
			if (CountSkills [i] == index)
				f = true;
		}
		if (f == false)
			CountSkills.Add (index);
	}
}
