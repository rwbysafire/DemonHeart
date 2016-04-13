using UnityEngine;
using System.Collections;

public class mainMenu : MonoBehaviour {

	public void startGame() {
		Application.LoadLevel("Prototype");
	}
	
	public void exitGame() {
		Application.Quit();
	}

	public void Instruction() {
		Application.LoadLevel ("Instruction");
	}

}
