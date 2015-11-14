using UnityEngine;
using System.Collections;

public class SkillBasicAttack : Skill
{
	public Projectile projectile;

	public SkillBasicAttack(Mob mob) : base(mob) {}

	public override string getName() {
		return "Basic Attack";
	}
	
	public override float getMaxCooldown() {
		return 0.25f * (1 - mob.stats.cooldownReduction / 100);
	}

	public override float getManaCost () {
		return 0;
	}
	
	public override void skillLogic() {
		fireArrow(Random.Range(-10, 10)/10f);
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sound/pew"), mob.gameObject.transform.position);
	}

	void fireArrow(float rotate = 0) {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Arrow_Placeholder")) as GameObject;
		projectile = new BasicAttackProjectile (basicArrow, mob);
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = mob.position;
		basicArrow.transform.rotation = mob.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
		projectile.projectileOnStart();
	}
}

class BasicAttackProjectile : Projectile {
	public BasicAttackProjectile(GameObject gameObject, Mob mob) : base(gameObject, mob) {}
	public override void OnHit () {
		if (collider.CompareTag("Enemy"))
			mob.useMana(-2);
		RaycastHit2D[] hit = Physics2D.LinecastAll(gameObject.transform.position - gameObject.transform.up * 0.47f, gameObject.transform.position + gameObject.transform.up * 2f);
		RaycastHit2D target = hit[0];
		foreach (RaycastHit2D x in hit) {
			if (x.collider.CompareTag(collider.tag)) {
				target = x;
				break;
			}
		}
		GameObject explosion = GameObject.Instantiate(Resources.Load("Explosion")) as GameObject;
		explosion.transform.position = target.point;
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sound/boom"), explosion.transform.position);
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
