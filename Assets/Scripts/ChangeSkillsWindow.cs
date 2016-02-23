using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSkillsWindow: MonoBehaviour {

	GameObject display;
	
	void Start () {
		display = transform.FindChild("ChangeSkillsBackground").gameObject;
		display.SetActive(false);
	}
	
	void Update () {
		if (Input.GetKeyDown (KeyCode.V)) {
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
	}
}
