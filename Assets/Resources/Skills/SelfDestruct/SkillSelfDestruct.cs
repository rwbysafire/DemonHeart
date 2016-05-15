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

    public override float getAttackSpeed() {
        return 0.5f;
    }

    public override float getMaxCooldown () {
		return 1f;
	}
	
	public override float getManaCost () {
		return 0;
	}

	public override void skillLogic (Entity mob, Stats stats)
	{
		GameObject explosion = GameObject.Instantiate(Resources.Load<GameObject>("Skills/SelfDestruct/selfDestructExplosion"));
		explosion.transform.position = mob.feetTransform.position;
		explosion.GetComponent<bloodExplosion>().maxHealth = stats.maxHealth;
		explosion.GetComponent<bloodExplosion>().enemyTag = mob.getEnemyTag();
		mob.gameObject.GetComponent<Mob>().hurt(stats.health);
	}
}
