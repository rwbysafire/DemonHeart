using UnityEngine;
using System.Collections;

public class SkillVolley : Skill {
	
	public Projectile projectile;

	public SkillVolley(GameObject gameObject, Stats stats) : base(gameObject, stats) { }
	
	public override string getName ()
	{
		return "Volley";
	}
	
	public override float getMaxCooldown ()
	{
		return 0.5f * (1 - (getStats().cooldown / 100));
	}
	
	public override void skillLogic()
	{
		fireArrow(-15);fireArrow(-10);fireArrow(-5);fireArrow(0);fireArrow(5);fireArrow(10);fireArrow(15);
	}

	void fireArrow(float rotate = 0)
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Arrow_Placeholder")) as GameObject;
		projectile = new VolleyProjectile (basicArrow, getGameObject(), getStats());
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject ().transform.position;
		basicArrow.transform.rotation = this.getGameObject ().transform.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
		projectile.projectileOnStart();
	}
}

class VolleyProjectile : Projectile {
	public VolleyProjectile(GameObject gameObject, GameObject origin, Stats stats) : base(gameObject, origin, stats) {}
	public override void OnHit () {
		GameObject explosion = GameObject.Instantiate(Resources.Load("Explosion")) as GameObject;
		RaycastHit2D[] hit = Physics2D.RaycastAll(gameObject.transform.position, gameObject.transform.up);
		explosion.transform.position = hit[1].point;
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
	}
	public override float getSpeed () {
		return 40;
	}
	public override float getDamage () {
		return 0.5f * stats.attackDamage;
	}
	public override float getTurnSpeed ()
	{
		return 200;
	}
	public override float getDuration ()
	{
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