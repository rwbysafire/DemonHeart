using UnityEngine;
using System.Collections;

public class SkillSlash : Skill {

	public SkillSlash() : base() { }

	public override string getName () {
		return "Slash";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/Slash/slashIcon");
	}
	
	public override float getAttackSpeed () {
		return 0.5f;
	}
	
	public override float getManaCost () {
		return 0;
	}

	public override void skillLogic (Mob mob) {
		GameObject slash = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Slash/Slash"));
		slash.transform.position = mob.headTransform.position;
		slash.transform.rotation = mob.headTransform.rotation;
		slash.transform.SetParent(mob.gameObject.transform);
		slash.GetComponent<SlashLogic>().damage = 1f * mob.stats.attackDamage;
		if (mob.gameObject.tag == "Player" || mob.gameObject.tag == "Ally")
			slash.GetComponent<SlashLogic>().enemyTag = "Enemy"; 
		else
			slash.GetComponent<SlashLogic>().enemyTag = "Player";
	}
}
