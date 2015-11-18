using UnityEngine;
using System.Collections;

public class SkillCombatRoll : Skill {

	bool rolling;
	float timer;
	GameObject knockback;

	public SkillCombatRoll(Mob mob) : base(mob) {}
	
	public override string getName() {
		return "Combat Roll";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/CombatRoll/whirlingBladesIcon");
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
		knockback = new GameObject();
		knockback.transform.position = mob.transform.position;
		knockback.transform.SetParent(mob.transform);
		knockback.AddComponent<CircleCollider2D>();
		knockback.AddComponent<Rigidbody2D>();
		Physics2D.IgnoreCollision(knockback.GetComponent<CircleCollider2D>(), mob.gameObject.GetComponent<CircleCollider2D>());
		knockback.GetComponent<Rigidbody2D>().isKinematic = true;
		knockback.GetComponent<CircleCollider2D>().radius = mob.gameObject.GetComponent<CircleCollider2D>().radius + 1f;
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/CombatRoll/dash"), mob.gameObject.transform.position);
	}

	public override void skillPassive() {
		if (timer > Time.fixedTime)
			mob.gameObject.GetComponent<Rigidbody2D>().velocity = mob.feetTransform.up * 70 * (timer-Time.fixedTime)/0.2f;
		else if (knockback)
			GameObject.Destroy(knockback);
	}
}
