using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ChangeSkillButton : MonoBehaviour 
{
	public int index;
	bool mark = false;

	void Update()
	{
		if (index == 0 && mark == false) 
		{
			if (GameObject.Find ("Player"))
			{
				Skill skill = GameObject.Find ("Player").GetComponent<Mob> ().skills [index];
				GetComponent<Image> ().sprite = skill.getImage ();
				GetComponentInChildren<Text> ().text = skill.getName ();
			}
			mark = true;
		}

		if (GameObject.Find ("Player") && index != 0)
		{
			Skill skill = GameObject.Find ("Player").GetComponent<Mob> ().skills [index];
			if (skill != null) 
			{
				GetComponent<Image> ().sprite = skill.getImage ();
				GetComponentInChildren<Text> ().text = skill.getName ();
			}
		}
	}
}