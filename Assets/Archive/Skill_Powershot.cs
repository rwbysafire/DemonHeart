using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Skill_Powershot : MonoBehaviour {
	
	public KeyCode keyBind;
	public float speed, cooldown, damage, pierceChance;
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
			fire(player);
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

	void fire(GameObject origin)
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		basicArrow.GetComponent<basic_projectile>().speed = speed;
		basicArrow.GetComponent<basic_projectile>().damage = damage;
		basicArrow.GetComponent<basic_projectile>().pierceChance = pierceChance;
		//Initiates the projectile's position and rotation
		basicArrow.transform.localScale = basicArrow.transform.localScale * 4;
		basicArrow.transform.position = origin.transform.position;
		basicArrow.transform.rotation = origin.transform.rotation;
		basicArrow.transform.Translate(Vector3.up * 0.7f);
	}
}
