using UnityEngine;
using System.Collections;

public class SkillBasicAttack : Skill
{
	public Projectile projectile;

	public SkillBasicAttack(GameObject gameObject) : base(gameObject) { }
	
	public override string getName ()
	{
		return "Basic Attack";
	}
	
	public override float getMaxCooldown ()
	{
		return 0.1f;
	}
	
	public override void skillLogic()
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		projectile = new BasicAttackProjectile (basicArrow, getGameObject());
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject().transform.position;
		basicArrow.transform.rotation = this.getGameObject().transform.rotation;
		basicArrow.transform.Translate(Vector3.up * 0.7f);
	}
}

class BasicAttackProjectile : Projectile {
	public BasicAttackProjectile(GameObject gameObject, GameObject origin) : base(gameObject, origin) {}
	public override float getSpeed () {
		return 10;
	}
	public override float getDamage () {
		return 10;
	}
}