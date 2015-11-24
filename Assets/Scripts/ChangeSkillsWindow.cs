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
		if (Input.GetKeyDown(KeyCode.V))
			display.SetActive(true);
		else if (Input.GetKeyUp(KeyCode.V))
			display.SetActive(false);
	}
}
