using UnityEngine;
using System.Collections;

public class SkillPowershot : Skill
{
	public Projectile projectile;

	public SkillPowershot(GameObject gameObject) : base(gameObject) { }

	public override string getName ()
	{
		return "Powershot";
	}
	
	public override float getMaxCooldown ()
	{
		return 5f;
	}
	
	public override void skillLogic()
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		projectile = new PowerShotProjectile (basicArrow, getGameObject());
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.localScale = basicArrow.transform.localScale * 1;
		basicArrow.transform.position = this.getGameObject().transform.position;
		basicArrow.transform.rotation = this.getGameObject().transform.rotation;
		basicArrow.transform.Translate(Vector3.up * 0.7f);
	}
}

class PowerShotProjectile : Projectile {
	public PowerShotProjectile(GameObject gameObject, GameObject origin) : base(gameObject, origin) {}
	public override float getSpeed () {
		return 20;
	}
	public override float getDamage () {
		return 20;
	}
	public override float getPierceChance () {
		return 100;
	}
}