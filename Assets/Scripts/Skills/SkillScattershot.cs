using UnityEngine;
using System.Collections;

public class SkillScattershot : Skill
{
	public Projectile projectile;

	public SkillScattershot(GameObject gameObject, Stats stats) : base(gameObject, stats) {}

	private int arrowCount = 9;

	public override string getName ()
	{
		return "Scattershot";
	}

	public override float getMaxCooldown ()
	{
		return 5f * (1 - getStats().cooldown / 100);
	}

	public override void skillLogic ()
	{
		for (int i = 0; i < arrowCount; i++) 
		{
			fireArrow(-45 + (i * 90 / (arrowCount - 1)) + Random.Range(-5,5));
		}
	}

	void fireArrow(int rotate = 0)
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Arrow_Placeholder")) as GameObject;
		projectile = new ScatterShotProjectile (basicArrow, getGameObject(), getStats());
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject ().transform.position;
		basicArrow.transform.rotation = this.getGameObject ().transform.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
	}
}

class ScatterShotProjectile : Projectile {
	public ScatterShotProjectile(GameObject gameObject, GameObject origin, Stats stats) : base(gameObject, origin, stats) {}
	public override float getSpeed () {
		return 5;
	}
	public override float getDamage () {
		return 1 * stats.attackDamage;
	}
	public override float getDuration () {
		return 0.5f;
	}
	public override float getPierceChance () {
		return 100;
	}
}