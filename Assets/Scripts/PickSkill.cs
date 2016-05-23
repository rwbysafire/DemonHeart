using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PickSkill : MonoBehaviour 
{

	int index;

	public void setIndex(int i)
	{
		index = i;
	}

	public void replaceSkill(int i)
	{
		if (GameObject.Find ("Player") && index != 0 && i != 0)
		{
			bool mark = false;
			for (int j = 0; j < 6; j++)
			{
				if (GameObject.Find ("Player").GetComponent<Mob> ().skills [j] != null)
				{
					if (GameObject.Find ("Player").GetComponent<Mob> ().skills [j].getName () == GameObject.Find ("Player").GetComponent<Player> ().listOfSkills [i].getName ())
						mark = true;
					else
						mark = false;
				}
			}
			if (mark == false)
				GameObject.Find ("Player").GetComponent<Mob> ().replaceSkill (index, GameObject.Find ("Player").GetComponent<Player> ().listOfSkills [i]);
		}
	}
}
	