using UnityEngine;
using System.Collections;

public class SkillSlash : Skill {

	public SkillSlash(GameObject gameObject, Stats stats) : base(gameObject, stats) { }

	public override string getName ()
	{
		return "Slash";
	}

	public override float getMaxCooldown ()
	{
		return 0.5f * (1 - (getStats().cooldown / 100));
	}

	public override void skillLogic ()
	{
		GameObject slash = GameObject.Instantiate(Resources.Load<GameObject>("Slash"));
		slash.transform.position = getGameObject().transform.position;
		slash.transform.rotation = getGameObject().transform.rotation;
		slash.transform.SetParent(getGameObject().transform);
		slash.GetComponent<SlashLogic>().damage = 2f * getStats().attackDamage;
		if (getGameObject().tag == "Player" || getGameObject().tag == "Ally")
			slash.GetComponent<SlashLogic>().enemyTag = "Enemy"; 
		else
			slash.GetComponent<SlashLogic>().enemyTag = "Player";
	}
}
