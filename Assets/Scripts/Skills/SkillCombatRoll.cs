using UnityEngine;
using System.Collections;

public class SkillCombatRoll : Skill {

	float timer;
	bool rolling;

	public SkillCombatRoll(Mob mob) : base(mob) {}
	
	public override string getName() {
		return "Combat Roll";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Sprite/whirlingBladesIcon");
	}
	
	public override float getMaxCooldown() {
		return 1f * (1 - mob.stats.cooldownReduction / 100);
	}
	
	public override float getManaCost () {
		return 15;
	}
	
	public override void skillLogic() {
		mob.disableMovement(0.2f);
		timer = Time.fixedTime + 0.2f;
		mob.gameObject.GetComponent<Rigidbody2D>().velocity = mob.feetTransform.up * 50;
	}
}
