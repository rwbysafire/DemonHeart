using UnityEngine;
using System.Collections;

public class SkillBasicAttack : Skill
{
	public Projectile projectile;

	public SkillBasicAttack(GameObject gameObject, Stats stats) : base(gameObject, stats) {}

	public override string getName() {
		return "Basic Attack";
	}
	
	public override float getMaxCooldown() {
		return 0.1f * (1 - getStats().cooldown / 100);
	}
	
	public override void skillLogic() {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		projectile = new BasicAttackProjectile (basicArrow, getGameObject(), getStats());
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject().transform.position;
		basicArrow.transform.rotation = this.getGameObject().transform.rotation;
		basicArrow.transform.Translate(Vector3.up * 0.7f);
	}
}

class BasicAttackProjectile : Projectile {
	public BasicAttackProjectile(GameObject gameObject, GameObject origin, Stats stats) : base(gameObject, origin, stats) {}
	public override float getSpeed () {
		return 15;
	}
	public override float getDamage () {
		return 1 * stats.attackDamage;
	}
}
