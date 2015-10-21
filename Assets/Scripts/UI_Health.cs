using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Health : MonoBehaviour {

	public Slider slider;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag ("Player"))
			slider.value = GameObject.FindWithTag ("Player").GetComponent<Health> ().health;
	}
}
