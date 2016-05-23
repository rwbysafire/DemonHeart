using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PlayerCreationWindow: MonoBehaviour {

	GameObject display;
	Text STR, DEX, INT;
	List<int> CountSkills = new List<int> ();
	
	void Start () {
		display = transform.FindChild ("PlayerCreationBackground").gameObject;
		STR = display.transform.FindChild ("str").gameObject.GetComponent<Text> ();
		DEX = display.transform.FindChild ("dex").gameObject.GetComponent<Text> ();
		INT = display.transform.FindChild ("int").gameObject.GetComponent<Text> ();

		if (PlayerPrefs.GetInt (CharSaveLoadScript.PREFS_LOAD_GAME) == 0) {
			Pause.PauseGame ();
			display.SetActive (true);
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
		GameObject.Find("Player").GetComponent<Mob>().stats.baseStrength += STR;
	}

	public void setint(int INT) {
		GameObject.Find("Player").GetComponent<Mob>().stats.baseIntelligence += INT;
	}

	public void setdex(int DEX) {
		GameObject.Find ("Player").GetComponent<Mob> ().stats.baseDexterity += DEX;
	}

	void Update () {
		
		STR.text = "STR:    " + Mathf.Ceil(GameObject.Find ("Player").GetComponent<Mob> ().stats.baseStrength).ToString();
		DEX.text = "DEX:    " + Mathf.Ceil(GameObject.Find ("Player").GetComponent<Mob> ().stats.baseDexterity).ToString();
		INT.text = "INT:    " + Mathf.Ceil(GameObject.Find ("Player").GetComponent<Mob> ().stats.baseIntelligence).ToString();


		if(!Pause.IsPaused())
			display.SetActive(false);
//		if (Input.GetKeyDown (KeyCode.V)) {
//			if (Pause.IsPaused () && display.activeSelf) {
//				// resume the game
//				Pause.ResumeGame ();
//				display.SetActive(false);
//			} else if (!Pause.IsPaused () && !display.activeSelf) {
//				// pause the game
//				Pause.PauseGame ();
//				display.SetActive(true);
//			}
//		}

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
