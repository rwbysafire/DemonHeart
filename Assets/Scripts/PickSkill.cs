using UnityEngine;
using System.Collections;

public class PickSkill : MonoBehaviour {

	int index;
	Skill[] skills;

	void Update () {
		if (Input.GetKeyUp(KeyCode.V))
			gameObject.SetActive(false);
	}

	public void setIndex(int i) {
		index = i;
	}

	public void replaceSkill(int i) {
		if (GameObject.Find("Player")) {
			GameObject.Find("Player").GetComponent<Mob>().replaceSkill(index, GameObject.Find("Player").GetComponent<Player>().listOfSkills[i]);
		}
	}
}
