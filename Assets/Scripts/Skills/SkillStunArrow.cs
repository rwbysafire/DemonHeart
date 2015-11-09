using UnityEngine;
using System.Collections;

public class SkillStunArrow : Skill 
{
	public Projectile projectile;

	public SkillStunArrow(GameObject gameObject, Stats stats) : base(gameObject, stats) { }
	
	public override string getName ()
	{
		return "Stun Arrow";
	}
	
	public override float getMaxCooldown ()
	{
		return 3f * (1 - getStats().cooldown / 100);
	}
	
	public override void skillLogic()
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		projectile = new StunArrowProjectile (basicArrow, getGameObject(), getStats());
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject().transform.position;
		basicArrow.transform.rotation = this.getGameObject().transform.rotation;
		basicArrow.transform.localScale *= 4; 
		basicArrow.transform.Translate(Vector3.up * 0.7f);
		projectile.projectileOnStart();
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/pew"), getGameObject().transform.position);
	}	
}

class StunArrowProjectile : Projectile {
	public StunArrowProjectile(GameObject gameObject, GameObject origin, Stats stats) : base(gameObject, origin, stats) {}
	public override void OnHit () {
		GameObject explosion = GameObject.Instantiate(Resources.Load("Explosion")) as GameObject;
		explosion.transform.position = collider.transform.position;
		explosion.transform.Translate((gameObject.transform.position - collider.transform.position).normalized * collider.bounds.size.x/2);
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
	}
	public override float getSpeed () {
		return 5;
	}
	public override float getDamage () {
		return 2 * stats.attackDamage;
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