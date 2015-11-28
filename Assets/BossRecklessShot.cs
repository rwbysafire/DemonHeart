using UnityEngine;
using System.Collections;

public class BossRecklessShot : Skill
{
	public Projectile projectile;
	
	public BossRecklessShot(Mob mob) : base(mob) {}
	
	private int arrowCount = 30;
	private int i = 0;
	private bool fireOn = false;
	
	public override string getName () {
		return "Boss Reckless Shot";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/Scattershot/scattershotIcon");
	}
	
	public override float getMaxCooldown () {
		return 5f * (1 - mob.stats.cooldownReduction / 100);
	}
	
	public override float getManaCost () {
		return 25;
	}
	
	public override void skillLogic () {
		fireOn = true;
		i = 0;
	}

	public override void skillPassive ()
	{
		if (fireOn) {
			fireArrow (i);
			AudioSource.PlayClipAtPoint (Resources.Load<AudioClip> ("Skills/pew"), mob.headTransform.position);
			i += 30;
			if(i == 360){
				fireOn = false;
			}
		}
	}

	void fireArrow(int rotate = 0) {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Skills/Arrow_Placeholder")) as GameObject;
		projectile = new BossRecklessShotProjectile (basicArrow, mob);
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		basicArrow.GetComponent<PlayAnimation> ().fileName = "Sprite/fire";
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = mob.headTransform.position;
		basicArrow.transform.rotation = mob.headTransform.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
		projectile.projectileOnStart();
	}
}

class BossRecklessShotProjectile : Projectile {
	public BossRecklessShotProjectile(GameObject gameObject, Mob mob) : base(gameObject, mob) {}
	public override void OnHit () {
		GameObject explosion = GameObject.Instantiate(Resources.Load("Skills/Explosion")) as GameObject;
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
	}
	public override float getSpeed () {
		return 10;
	}
	public override float getDamage () {
		return (1 * mob.stats.basicAttackDamage) + (0.3f * mob.stats.attackDamage);
	}
	public override float getDuration () {
		return 3f;
	}
	public override float getPierceChance () {
		return 100;
	}
}

