using UnityEngine;
using System.Collections;

public class SkillSlash : Skill {

	public SkillSlash(Mob mob) : base(mob) { }

	public override string getName ()
	{
		return "Slash";
	}

	public override float getMaxCooldown ()
	{
		return 0.4f * (1 - (mob.stats.cooldownReduction / 100));
	}

	public override void skillLogic ()
	{
		GameObject slash = GameObject.Instantiate(Resources.Load<GameObject>("Slash"));
		slash.transform.position = mob.position;
		slash.transform.rotation = mob.rotation;
		slash.transform.SetParent(mob.gameObject.transform);
		slash.GetComponent<SlashLogic>().damage = 1f * mob.stats.attackDamage;
		if (mob.gameObject.tag == "Player" || mob.gameObject.tag == "Ally")
			slash.GetComponent<SlashLogic>().enemyTag = "Enemy"; 
		else
			slash.GetComponent<SlashLogic>().enemyTag = "Player";
	}
}
