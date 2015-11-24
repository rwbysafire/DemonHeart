using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ChangeSkillButton : MonoBehaviour {

	public int index;

	void Update () {
		if (GameObject.Find("Player")) {
			Skill skill = GameObject.Find("Player").GetComponent<Mob>().skills[index];
			GetComponent<Image>().sprite =skill.getImage();
			GetComponentInChildren<Text>().text = skill.getName();
		}
	}
}
