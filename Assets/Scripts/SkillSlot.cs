using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SkillSlot : MonoBehaviour {
	
	public KeyCode keyBind;
	Slider slider;
	Text text;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider>();
		text = GetComponentInChildren<Text>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void setCooldown (float cooldown) {
		slider.maxValue = cooldown;
	}

	public void displayCooldown (float remainingCD) {
		slider.value = remainingCD;
		if (remainingCD >= 0)
			text.text = ((int)remainingCD + 1).ToString ();
		else
			text.text = "";
	}
}
