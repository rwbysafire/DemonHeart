using UnityEngine;
using System.Collections;

public class Pause : MonoBehaviour {
    bool showText = false;
    Rect textArea = new Rect(0, 0, Screen.width, Screen.height);
    //Shows a GUI in top right when Esc is pressed once saying "PAUSED", should change this when pause menu is finished
    void OnGUI()
    {
        if(showText == true)
        GUI.Label(textArea, "PAUSED");
    }
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
            {//stops the game when you press Esc, sets showText = true
                showText = true;
                Time.timeScale = 0;
            }
            else
            {//continues the game when pressing Esc while still paused, makes "PAUSED" disappear
                showText = false;
                Time.timeScale = 1;
            }
        }
	
	}
}
