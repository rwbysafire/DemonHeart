using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingHealthBar : MonoBehaviour {
	
	Slider slider;
	GameObject healthBar;
	Mob mob;

	// Use this for initialization
	void Start () {
		slider = GetComponentInChildren<Slider>();
		healthBar = transform.FindChild("FloatingHealthBar").gameObject;
		mob = gameObject.GetComponent<Mob>();
	}
	
	// Update is called once per frame
	void Update () {
		if (mob.lastHit > Time.fixedTime - 3) {
			float health = Mathf.Ceil(GetComponentInParent<Mob> ().stats.health);
			if (health < mob.stats.maxHealth) {
				healthBar.SetActive(true);
				slider.value = mob.stats.health / mob.stats.maxHealth;
			}
			else
				healthBar.SetActive(false);
		}
		else
			healthBar.SetActive(false);
	}
}
