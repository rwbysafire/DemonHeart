using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skill_Template : MonoBehaviour {

	// All skills have the following features:
	//  - a keybind
	//  - a cooldown
	//  - a name
	//  - an icon
	//  - a mana cost
	//  - an action

	protected KeyCode keyBind;
	protected float cooldown;
	protected string title;
	protected Image icon;
	protected float manaCost;
	protected float remainingCD;

	Slider slider;
	Text text; 
	
	public void setKeyBind(KeyCode bind) {
		keyBind = bind;
	}
	
	public void setCooldown(float CD) {
		cooldown = CD;
	}
	
	public void setTitle(string str) {
		// Override me
	}
	
	public void setIcon(Image image) {
		// Override me
	}

	public void setManaCost(float cost) {
		// Override me
	}

	public void activateSkill() {
		// Override me
	}

	void Start () {
		slider = GetComponent<Slider> ();
		text = GetComponentInChildren<Text> ();
	}

	void Update () {
		// Updates the skill's cooldown on the HUD
		slider.maxValue = cooldown;
		slider.value = remainingCD;
		if (remainingCD >= 0)
			text.text = ((int)remainingCD + 1).ToString ();
		else
			text.text = "";
	}
}
