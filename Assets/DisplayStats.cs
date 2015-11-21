using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DisplayStats : MonoBehaviour {

	GameObject display;

	void Start () {
		display = GameObject.Find("StatsDisplayBackground");
		display.SetActive(false);
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.C)) {
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			display.SetActive(true);
			display.GetComponentsInChildren<Text>()[0].text = "STR: " + player.GetComponent<Mob>().stats.strength.ToString();
			display.GetComponentsInChildren<Text>()[1].text = "INT: " + player.GetComponent<Mob>().stats.intelligence.ToString();
			display.GetComponentsInChildren<Text>()[2].text = "DEX: " + player.GetComponent<Mob>().stats.dexterity.ToString();
		}
		else if (Input.GetKeyUp(KeyCode.C))
			display.SetActive(false);
	}
}
