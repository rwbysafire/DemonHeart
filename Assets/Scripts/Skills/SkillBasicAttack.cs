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
		return 0.25f * (1 - getStats().cooldown / 100);
	}
	
	public override void skillLogic() {
		fireArrow(Random.Range(-10, 10)/10f);
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/pew"), getGameObject().transform.position);
	}

	void fireArrow(float rotate = 0)
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Arrow_Placeholder")) as GameObject;
		projectile = new BasicAttackProjectile (basicArrow, getGameObject(), getStats());
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject ().transform.position;
		basicArrow.transform.rotation = this.getGameObject ().transform.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
		projectile.projectileOnStart();
	}
}

class BasicAttackProjectile : Projectile {
	public BasicAttackProjectile(GameObject gameObject, GameObject origin, Stats stats) : base(gameObject, origin, stats) {}
	public override void OnHit () {
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
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/boom"), explosion.transform.position);
	}
	public override float getSpeed () {
		return 40;
	}
	public override float getDuration ()
	{
		return 0.5f;
	}
	public override float getDamage () {
		return 1 * stats.attackDamage;
	}
}
