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
		GameObject explosion = GameObject.Instantiate(Resources.Load("Explosion")) as GameObject;
		explosion.transform.position = collider.transform.position;
		explosion.transform.Translate((gameObject.transform.position - collider.transform.position).normalized * collider.bounds.size.x/2);
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
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
