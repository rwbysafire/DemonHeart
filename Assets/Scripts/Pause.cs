using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Pause : MonoBehaviour {
    bool showText = false;
	Rect textArea = new Rect(0, 0, Screen.width, Screen.height);
	GameObject pause;
	GameObject pauseWindow;
	GameObject options;
    //Shows a GUI in top right when Esc is pressed once saying "PAUSED", should change this when pause menu is finished

	void Start() {
		pause = transform.FindChild("Pause").gameObject;
		pauseWindow = pause.transform.FindChild("PauseWindow").gameObject;
		options = pause.transform.FindChild("Options").gameObject;
	}

    void OnGUI()
    {
        if(showText == true)
        GUI.Label(textArea, "PAUSED");
    }
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (!Pause.IsPaused ())
            {//stops the game when you press Esc, sets showText = true
                showText = true;
				Pause.PauseGame ();
				pause.SetActive(true);
				options.SetActive(false);
				pauseWindow.SetActive(true);
            }
            else
            {//continues the game when pressing Esc while still paused, makes "PAUSED" disappear
                showText = false;
				Pause.ResumeGame ();
				pause.SetActive(false);
            }
        }
	
	}

	public static void PauseGame() {
		Time.timeScale = 0;
	}

	public static void ResumeGame() {
		Time.timeScale = 1;
	}

	public static bool IsPaused() {
		return Time.timeScale == 0;
	}

	public void respawnPlayer() {
		if(GameObject.Find("Player"))
			Destroy(GameObject.Find("Player"));
		GameObject player = Instantiate(Resources.Load<GameObject>("Player"));
		player.name = "Player";
	}

	public void muteAudio() {
		Camera.main.GetComponent<AudioListener>().enabled = !Camera.main.GetComponent<AudioListener>().enabled;
	}

	public void mainMenu() {
		Application.LoadLevel("MainMenu");
	}
	
	public void exitGame() {
		Application.Quit();
	}
}
