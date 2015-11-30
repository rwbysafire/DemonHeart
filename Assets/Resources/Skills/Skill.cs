using UnityEngine;
using System.Collections;

public abstract class Skill  
{
	public Mob mob;
	private float cooldown;


	public Skill(Mob mob) {
		this.mob = mob;
		cooldown = 0.0f; 
	}

	public bool useSkill() {
		if (cooldown <= Time.fixedTime && !mob.isAttacking && mob.useMana(getManaCost())) {
			mob.attack(this, getAttackSpeed()/mob.stats.attackSpeed);
			cooldown = Time.fixedTime + getMaxCooldown(); 
			return true;
		}
		return false; 
	}

	public float remainingCooldown() {
		float tempTime = cooldown - Time.fixedTime;
		if (tempTime < 0)
			tempTime = 0;
		return tempTime;
	}

	public abstract string getName();
	public abstract Sprite getImage();
	public virtual float getMaxCooldown() {return 0;}
	public virtual float getAttackSpeed() {return 0;}
	public abstract float getManaCost();
	public abstract void skillLogic(); 
	public virtual void skillPassive() {}
	public virtual void skillFixedUpdate() {}
}
