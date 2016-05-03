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
	
	public override void skillLogic(Entity mob, Stats stats) {
        attack(mob, stats);
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/pew"), mob.gameObject.transform.position);
	}

    void attack(Entity mob, Stats stats) {
        float spreadCalcDistance = 6;
        float startDistance = 2;
        float baseAngle = 5;
        float ratio = (Mathf.Clamp(Vector3.Distance(mob.getTargetLocation(), mob.headTransform.position), startDistance, spreadCalcDistance + startDistance) - startDistance) / spreadCalcDistance;
        float angleOfSpread = (((1 - ratio) * 4) + ratio) * baseAngle;
        for (int i = 0; i < properties["projectileCount"]; i++) {
            fireArrow(mob, stats, ((properties["projectileCount"] - 1) * angleOfSpread / -2) + i * angleOfSpread);
        }
    }

	void fireArrow(Entity mob, Stats stats, float rotate = 0) {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Skills/Arrow_Placeholder")) as GameObject;
		GameObject arrowGlow = MonoBehaviour.Instantiate(Resources.Load("ShotGlow")) as GameObject;
		arrowGlow.transform.position = new Vector3(basicArrow.transform.position.x, basicArrow.transform.position.y, -0.3f);
		arrowGlow.transform.SetParent (basicArrow.transform);
		projectile = new BasicAttackProjectile (basicArrow, stats, this);
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
	public BasicAttackProjectile(GameObject gameObject, Stats stats, Skill skill) : base(gameObject, stats, skill) {}
	public override void OnHit () {
        if (collider.CompareTag("Enemy")) {
            stats.mana += 10;
            foreach (Gem gem in skill.gems) {
                if (gem != null) {
                    gem.onHitEffect(gameObject.GetComponent<basic_projectile>(), stats);
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
		return 1 * stats.basicAttackDamage;
	}
}
