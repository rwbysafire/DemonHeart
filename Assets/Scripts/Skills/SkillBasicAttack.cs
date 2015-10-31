using UnityEngine;
using System.Collections;

public class SkillBasicAttack : Skill
{
	public SkillBasicAttack(GameObject gameObject) : base(gameObject) {}

<<<<<<< HEAD
	public SkillBasicAttack(GameObject gameObject) : base(gameObject) {}

=======
>>>>>>> origin/master
	public override string getName() {
		return "Basic Attack";
	}
	
	public override float getMaxCooldown() {
		return 0.1f;
	}
	
	public override void skillLogic() {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		//basicArrow.GetComponent<basic_projectile>().speed = 10;
		//basicArrow.GetComponent<basic_projectile>().damage = 10;
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject().transform.position;
		basicArrow.transform.rotation = this.getGameObject().transform.rotation;
		basicArrow.transform.Translate(Vector3.up * 0.7f);
	}
}

<<<<<<< HEAD
class BasicAttackProjectile : Projectile {
	public BasicAttackProjectile(GameObject gameObject, GameObject origin) : base(gameObject, origin) {}
	public override float getSpeed () {
		return 15;
	}
	public override float getDamage () {
		return 10;
	}
}
=======
>>>>>>> origin/master
