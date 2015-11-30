using UnityEngine;
using System.Collections;

public class SkillStunArrow : Skill 
{
	public Projectile projectile;

	public SkillStunArrow(Mob mob) : base(mob) { }
	
	public override string getName () {
		return "Stun Arrow";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/StunArrow/stunArrowIcon");
	}
	
	public override float getAttackSpeed () {
		return 1f;
	}
	
	public override float getMaxCooldown () {
		return 3f * (1 - mob.stats.cooldownReduction / 100);
	}
	
	public override float getManaCost () {
		return 30;
	}
	
	public override void skillLogic() {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Skills/Arrow_Placeholder")) as GameObject;
		projectile = new StunArrowProjectile (basicArrow, mob);
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = mob.headTransform.position;
		basicArrow.transform.rotation = mob.headTransform.rotation;
		basicArrow.transform.localScale *= 4; 
		basicArrow.transform.Translate(Vector3.up * 0.7f);
		projectile.projectileOnStart();
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/pew"), mob.headTransform.position);
	}	
}

class StunArrowProjectile : Projectile {
	public StunArrowProjectile(GameObject gameObject, Mob mob) : base(gameObject, mob) {}
	public override void OnHit () {
		GameObject explosion = GameObject.Instantiate(Resources.Load("Skills/Explosion")) as GameObject;
		explosion.transform.position = collider.transform.position;
		explosion.transform.Translate((gameObject.transform.position - collider.transform.position).normalized * collider.bounds.size.x/2);
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
	}
	public override float getSpeed () {
		return 5;
	}
	public override float getDamage () {
		return 2 * mob.stats.attackDamage;
	}
	public override float getStunTime () {
		return 2;
	}
	public override float getDuration () {
		return 5;
	}
	public override float getPierceChance () {
		return 100;
	}
}