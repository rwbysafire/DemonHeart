using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChangeSkillButton : MonoBehaviour {

	public int index;
	bool mark = false;
//	Dictionary<int, string> skills = new Dictionary<int, string>();

	void Update () 
	{
		if (index == 0 && !mark)
		{
			if (GameObject.Find("Player")) 
			{
				Skill skill = GameObject.Find("Player").GetComponent<Mob>().skills[index];
				GetComponent<Image>().sprite =skill.getImage();
				GetComponentInChildren<Text>().text = skill.getName();
//				skills.Add(index, skill.getName());
			}
			mark = true;
		}

		if (GameObject.Find("Player") && index != 0) {
			Skill skill = GameObject.Find("Player").GetComponent<Mob>().skills[index];
//			if (skills.ContainsValue(skill.getName()) == false)
//			{
			GetComponent<Image> ().sprite = skill.getImage ();
			GetComponentInChildren<Text> ().text = skill.getName ();
//				skills.Add(index, skill.getName());
//			}
		}
	}
}
