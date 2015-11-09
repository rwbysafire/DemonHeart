using UnityEngine;
using System.Collections;

public class SkillPowershot : Skill
{
	public Projectile projectile;

	public SkillPowershot(GameObject gameObject, Stats stats) : base(gameObject, stats) { }

	public override string getName ()
	{
		return "Powershot";
	}
	
	public override float getMaxCooldown ()
	{
		return 5f * (1 - getStats().cooldown / 100);
	}
	
	public override void skillLogic()
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		projectile = new PowerShotProjectile (basicArrow, getGameObject(), getStats());
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.localScale = basicArrow.transform.localScale * 1;
		basicArrow.transform.position = this.getGameObject().transform.position;
		basicArrow.transform.rotation = this.getGameObject().transform.rotation;
		basicArrow.transform.Translate(Vector3.up * 0.7f);
		projectile.projectileOnStart();
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/sniperShot"), getGameObject().transform.position);
	}
}

class PowerShotProjectile : Projectile {
	public PowerShotProjectile(GameObject gameObject, GameObject origin, Stats stats) : base(gameObject, origin, stats) {}

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
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/boom"), explosion.transform.position);
	}
	public override float getSpeed () {
		return 40;
	}
	public override float getDamage () {
		return 4 * stats.attackDamage;
	}
	public override float getDuration ()
	{
		return 0.5f;
	}
	public override float getPierceChance () {
		return 100;
	}
}