using UnityEngine;
using System.Collections;

public class SkillExplosiveArrow : Skill {
	
	public Projectile projectile;

	public SkillExplosiveArrow(Mob mob) : base(mob) { }
	
	public override string getName () {
		return "Explosive Arrow";
	}
	
	public override float getMaxCooldown () {
		return 0.5f * (1 - (mob.stats.cooldownReduction / 100));
	}
	
	public override float getManaCost () {
		return 10;
	}
	
	public override void skillLogic () {
		fireArrow(-15);fireArrow(-10);fireArrow(-5);fireArrow(0);fireArrow(5);fireArrow(10);fireArrow(15);
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/pew"), mob.position);
	}

	void fireArrow(float rotate = 0) {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Arrow_Placeholder")) as GameObject;
		projectile = new ExplosiveArrowProjectile (basicArrow, mob);
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = mob.position;
		basicArrow.transform.rotation = mob.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
		projectile.projectileOnStart();
	}
}

class ExplosiveArrowProjectile : Projectile {
	public ExplosiveArrowProjectile(GameObject gameObject, Mob mob) : base(gameObject, mob) {}
	public override void OnExplode () {
		RaycastHit2D[] hit = Physics2D.LinecastAll(gameObject.transform.position - gameObject.transform.up * 0.47f, gameObject.transform.position + gameObject.transform.up * 2f);
		RaycastHit2D target = hit[0];
		foreach (RaycastHit2D x in hit) {
			if (x.collider.CompareTag(collider.tag)) {
				target = x;
				break;
			}
		}
		GameObject explosion = GameObject.Instantiate(Resources.Load<GameObject>("FireExplosion"));
		explosion.GetComponent<ExplosiveArrowExplosion>().damage = 2 * mob.stats.attackDamage;
		explosion.transform.position = target.point;
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
		if (tag == "Player" || tag == "Ally")
			explosion.GetComponent<ExplosiveArrowExplosion>().enemyTag = "Enemy"; 
		else
			explosion.GetComponent<ExplosiveArrowExplosion>().enemyTag = "Player";
	}
	public override float getSpeed () {
		return 40;
	}
	public override float getDuration () {
		return 0.5f;
	}
	public override float getDamage () {
		return 1 * mob.stats.basicAttackDamage;
	}
}

