using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExperienceBar : MonoBehaviour {

	Slider slider;
	Text text;

	// Use this for initialization
	void Start () {
		slider = GetComponentInChildren<Slider>();
		text = GetComponentInChildren<Text>();
	}

	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.Find ("Player");
		if (player) 
		{
			slider.value = player.GetComponent<Mob>().stats.CurExp / player.GetComponent<Mob>().stats.threshold;
			text.text = "Exp:\n" + Mathf.Ceil(player.GetComponent<Mob>().stats.CurExp).ToString() + " / " + Mathf.Ceil(player.GetComponent<Mob>().stats.threshold).ToString();
		}
	}
}
