using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickSkill : MonoBehaviour {

	int index;
	Dictionary<int, bool> mark = new Dictionary<int, bool> (); 
//	Skill[] skills;
//
//	void Update () {
//		if (Input.GetKeyUp(KeyCode.V))
//			gameObject.SetActive(false);
//	}
	void Start()
	{
		mark [0] = true;
		for (int i = 1; i < 11; i++) 
		{
			mark [i] = false;
		}
	}

	public void setIndex(int i) 
	{
		index = i;
	}

	public void replaceSkill(int i) {
		if (mark [i] == false) {
			if (GameObject.Find ("Player")) {
				GameObject.Find ("Player").GetComponent<Mob> ().replaceSkill (index, GameObject.Find ("Player").GetComponent<Player> ().listOfSkills [i]);
				mark [i] = true;
			}
		}
	}
}
