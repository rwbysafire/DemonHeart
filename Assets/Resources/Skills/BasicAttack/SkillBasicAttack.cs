using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillBasicAttack : Skill
{
	public Projectile projectile;
    SkillType projectileSkill = new ProjectileSkill();

	public SkillBasicAttack() : base() {
        addSkillType(projectileSkill);
//        addGem(0, new GemExtraProjectiles());
//        addGem(1, new chainLightningOnHitGem());
    }

    public override string getName() {
		return "Basic Attack";
	}

	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/BasicAttack/basicAttackIcon");
	}

	public override float getAttackSpeed () {
		return 1f;
	}

	public override float getManaCost () {
		return 0;
	}
	
	public override void skillLogic(Mob mob) {
        attack(mob);
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/pew"), mob.gameObject.transform.position);
	}

    void attack(Mob mob) {
        float y = Vector3.Distance(mob.getTargetLocation(), mob.transform.position);
        if (y > 6)
            y = 6;
        else if (y < 2)
            y = 2;
        float angleOfSpread = (((1-(y-2)/4)*3)+1)*5;
        for (int i = 0; i < properties["projectileCount"]; i++) {
            fireArrow(mob, ((properties["projectileCount"] - 1) * angleOfSpread / -2) + i * angleOfSpread);
        }
    }

	void fireArrow(Mob mob, float rotate = 0) {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Skills/Arrow_Placeholder")) as GameObject;
		GameObject arrowGlow = MonoBehaviour.Instantiate(Resources.Load("ShotGlow")) as GameObject;
		arrowGlow.transform.position = new Vector3(basicArrow.transform.position.x, basicArrow.transform.position.y, -0.3f);
		arrowGlow.transform.SetParent (basicArrow.transform);
		projectile = new BasicAttackProjectile (basicArrow, mob, this);
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = mob.headTransform.position;
		basicArrow.transform.rotation = mob.headTransform.rotation;
		basicArrow.transform.Translate (Vector3.up * 1.2f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
		projectile.projectileOnStart();
        projectile.chainTimes = properties["chainCount"];
	}
    
}

class BasicAttackProjectile : Projectile {
	public BasicAttackProjectile(GameObject gameObject, Mob mob, Skill skill) : base(gameObject, mob, skill) {}
	public override void OnHit () {
        if (collider.CompareTag("Enemy")) {
            mob.useMana(-10);
            foreach (Gem gem in skill.gems) {
                if (gem != null) {
                    gem.onHitEffect();
                }
            }
        }
		RaycastHit2D[] hit = Physics2D.LinecastAll(gameObject.transform.position - gameObject.transform.up * 0.47f, gameObject.transform.position + gameObject.transform.up * 2f);
		RaycastHit2D target = hit[0];
		foreach (RaycastHit2D x in hit) {
			if (x.collider.CompareTag(collider.tag)) {
				target = x;
				break;
			}
		}
		GameObject explosion = GameObject.Instantiate(Resources.Load("Skills/Explosion")) as GameObject;
		explosion.transform.position = target.point;
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/boom"), explosion.transform.position);
	}
	public override float getSpeed () {
		return 40;
	}
	public override float getDuration () {
		return 0.5f;
	}
	public override float getDamage () {
		return 1 * mob.stats.basicAttackDamage;
	}
}
