using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {

	public void startGame() {
		Application.LoadLevel("BrianTest");
	}
	
	public void exitGame() {
		Application.Quit();
	}
}
