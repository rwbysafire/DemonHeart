using UnityEngine;
using System.Collections;

public class SkillCombatRoll : Skill {

	float timer;
	bool rolling;

	public SkillCombatRoll(Mob mob) : base(mob) {}
	
	public override string getName() {
		return "Combat Roll";
	}
	
	public override float getMaxCooldown() {
		return 1f * (1 - mob.stats.cooldownReduction / 100);
	}
	
	public override float getManaCost () {
		return 5;
	}
	
	public override void skillLogic() {
		mob.disableMovement(0.2f);
		timer = Time.fixedTime + 0.2f;
	}

	public override void skillFixedUpdate() {
		if (timer > Time.fixedTime) {
			mob.gameObject.GetComponent<Rigidbody2D>().AddForce(mob.feetTransform.up * 300);
			rolling = true;
		} else if (rolling) {
			mob.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			rolling = false;
		}
	}
}
