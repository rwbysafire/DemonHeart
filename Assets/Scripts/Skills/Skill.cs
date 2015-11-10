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
		if (cooldown <= Time.fixedTime) {
			skillLogic();
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
	public abstract float getMaxCooldown();
	public abstract void skillLogic(); 
}
