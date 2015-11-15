using UnityEngine;
using System.Collections;

public class SkillVolley : Skill {
	
	public Projectile projectile;

	public SkillVolley(Mob mob) : base(mob) { }
	
	public override string getName () {
		return "Volley";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Sprite/volleyIcon");
	}
	
	public override float getMaxCooldown () {
		return 0.5f * (1 - (mob.stats.cooldownReduction / 100));
	}
	
	public override float getManaCost () {
		return 25;
	}
	
	public override void skillLogic() {
		fireArrow(-15);fireArrow(-10);fireArrow(-5);fireArrow(0);fireArrow(5);fireArrow(10);fireArrow(15);
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sound/pew"), mob.position);
	}

	void fireArrow(float rotate = 0) {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Arrow_Placeholder")) as GameObject;
		projectile = new VolleyProjectile (basicArrow, mob);
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = mob.position;
		basicArrow.transform.rotation = mob.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
		projectile.projectileOnStart();
	}
}

class VolleyProjectile : Projectile {
	public VolleyProjectile(GameObject gameObject, Mob mob) : base(gameObject, mob) {}
	public override void OnHit () {
		GameObject explosion = GameObject.Instantiate(Resources.Load("Explosion")) as GameObject;
		RaycastHit2D[] hit = Physics2D.LinecastAll(gameObject.transform.position - gameObject.transform.up * 0.47f, gameObject.transform.position + gameObject.transform.up * 2f);
		RaycastHit2D target = hit[0];
		foreach (RaycastHit2D x in hit) {
			if (x.collider.CompareTag(collider.tag)) {
				target = x;
				break;
			}
		}
		explosion.transform.position = target.point;
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sound/boom"), explosion.transform.position);
	}
	public override float getSpeed () {
		return 40;
	}
	public override float getDamage () {
		return (1 * mob.stats.basicAttackDamage) + (0.3f * mob.stats.attackDamage);
	}
	public override float getTurnSpeed () {
		return 200;
	}
	public override float getDuration () {
		return 0.5f;
	}
	public override int getChaining () {
		return 2;
	}
	public override bool getForking () {
		return true;
	}
	public override bool getHoming () {
		return true;
	}
}