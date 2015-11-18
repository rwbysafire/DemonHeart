using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingHealthBar : MonoBehaviour {
	
	Slider slider;

	// Use this for initialization
	void Start () {
		slider = GetComponentInChildren<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
		slider.value = GetComponentInParent<Mob>().stats.health / GetComponentInParent<Mob>().stats.maxHealth;
		float health = Mathf.Ceil(GetComponentInParent<Mob> ().stats.health);
		if (health < 0)
			health = 0;
		else if (health >= GetComponentInParent<Mob>().stats.maxHealth)
			GetComponent<Canvas>().enabled = false;
		else
			GetComponent<Canvas>().enabled = true;
	}
}
