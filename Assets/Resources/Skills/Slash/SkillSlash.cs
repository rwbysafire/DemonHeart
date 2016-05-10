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

	public override void skillLogic (Entity mob, Stats stats) {
		GameObject slash = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Slash/Slash"));
		slash.transform.position = mob.headTransform.position;
		slash.transform.rotation = mob.headTransform.rotation;
		slash.transform.SetParent(mob.headTransform);
		slash.GetComponent<SlashLogic>().damage = 1f * stats.attackDamage;
		slash.GetComponent<SlashLogic>().attackSpeed = properties["attackSpeed"] / stats.attackSpeed /2;
		slash.GetComponent<SlashLogic>().enemyTag = mob.getEnemyTag();
	}
}
