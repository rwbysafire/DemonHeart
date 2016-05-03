using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class mainMenu : MonoBehaviour {

	public Button continueButton;

	public void startGame() {
		SceneManager.LoadScene("Prototype");
		PlayerPrefs.SetInt (CharSaveLoadScript.PREFS_LOAD_GAME, 0);
	}
	
	public void exitGame() {
		Application.Quit();
	}

	public void Instruction() {
		SceneManager.LoadScene ("Instruction");
	}

	public void ContinueGame () {
		SceneManager.LoadScene("Prototype");
		PlayerPrefs.SetInt (CharSaveLoadScript.PREFS_LOAD_GAME, 1);
	}

	void Start () {
		if (CharSaveLoadScript.HasSavedData ()) {
			continueButton.interactable = true;
		} else {
			continueButton.interactable = false;
		}
	}

	void Update () {
		
	}

}
