using UnityEngine;
using System.Collections;

public class SkillSelfDestruct : Skill {

	public SkillSelfDestruct() : base() { }
	
	public override string getName () {
		return "Self Destruct";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/SelfDestruct/SelfDestructIcon");
	}
	
	public override float getMaxCooldown () {
		return 1f;
	}
	
	public override float getManaCost () {
		return 0;
	}

	public override void skillLogic (Mob mob)
	{
		GameObject explosion = GameObject.Instantiate(Resources.Load<GameObject>("Skills/SelfDestruct/selfDestructExplosion"));
		explosion.transform.position = mob.transform.position;
		explosion.GetComponent<bloodExplosion>().maxHealth = mob.stats.maxHealth;
		explosion.GetComponent<bloodExplosion>().enemyTag = mob.getEnemyTag();
		mob.hurt(mob.stats.health);
	}
}
