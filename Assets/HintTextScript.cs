using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HintTextScript : MonoBehaviour {

	public Text titleText, paragraphText;

	// Use this for initialization
	void Start () {
		HideHint ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ShowHint (string title, string paragraph) {
		titleText.text = title;
		paragraphText.text = paragraph;
		gameObject.SetActive (true);
	}

	public void HideHint () {
		if (gameObject.activeSelf) {
			gameObject.SetActive (false);
		}
	}
}
