using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skill_Template : MonoBehaviour {
	
	public KeyCode keyBind;
	public float cooldown;
	float remainingCD;
	Slider slider;
	Text text;
	
	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		text = GetComponentInChildren<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(keyBind) && remainingCD <= 0 && GameObject.FindWithTag ("Player")) {
			GameObject player = GameObject.FindWithTag("Player");
			// Place the skill's code in here \/\/\/

			remainingCD = cooldown;
		}
		remainingCD -= Time.deltaTime;
		
		// Updates the skill's cooldown on the HUD
		slider.maxValue = cooldown;
		slider.value = remainingCD;
		if (remainingCD >= 0)
			text.text = ((int)remainingCD + 1).ToString ();
		else
			text.text = "";
	}
}
