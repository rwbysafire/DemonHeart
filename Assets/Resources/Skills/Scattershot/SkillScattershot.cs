using UnityEngine;
using System.Collections;

public class SkillScattershot : Skill
{
	public Projectile projectile;
    ProjectileSkill projectileSkill = new ProjectileSkill();

    public SkillScattershot() : base() {
        projectileSkill.setProjectileCount(10);
        addSkillType(projectileSkill);
    }

	public override string getName () {
		return "Scattershot";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/Scattershot/scattershotIcon");
	}
	
	public override float getAttackSpeed () {
		return 1f;
	}

	public override float getMaxCooldown () {
		return 5f;
	}
	
	public override float getManaCost () {
		return 25;
	}

	public override void skillLogic (Entity mob, Stats stats) {
        attack(mob, stats);
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/pew"), mob.headTransform.position);
	}

    void attack(Entity mob, Stats stats) {
        float y = Vector3.Distance(mob.getTargetLocation(), mob.feetTransform.position);
        if (y > 6)
            y = 6;
        else if (y < 2)
            y = 2;
        float angleOfSpread = (((1 - (y - 2) / 4) * 3) + 1) * 5;
        for (int i = 0; i < properties["projectileCount"]; i++) {
            fireArrow(mob, stats, ((properties["projectileCount"] - 1) * angleOfSpread / -2) + i * angleOfSpread);
        }
    }

    void fireArrow(Entity mob, Stats stats, float rotate = 0) {
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Skills/Arrow_Placeholder")) as GameObject;
		projectile = new ScatterShotProjectile (basicArrow, stats);
		basicArrow.GetComponent<basic_projectile> ().setProjectile (projectile);
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = mob.headTransform.position;
		basicArrow.transform.rotation = mob.headTransform.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
		projectile.projectileOnStart();
        projectile.chainTimes = properties["chainCount"];
    }
}

class ScatterShotProjectile : Projectile {
	public ScatterShotProjectile(GameObject gameObject, Stats stats) : base(gameObject, stats) {}
	public override void OnHit () {
		GameObject explosion = GameObject.Instantiate(Resources.Load("Skills/Explosion")) as GameObject;
        RaycastHit2D[] hit = Physics2D.LinecastAll(gameObject.transform.position - gameObject.transform.up * 0.47f, gameObject.transform.position + gameObject.transform.up * 2f);
        Vector3 target = gameObject.transform.position;
        foreach (RaycastHit2D x in hit) {
            if (x.collider.CompareTag(collider.tag)) {
                target = x.point;
                break;
            }
        }
        explosion.transform.position = target;
		explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
	}
	public override float getSpeed () {
		return 20;
	}
	public override float getDamage () {
		return (1 * stats.basicAttackDamage) + (0.3f * stats.attackDamage);
	}
	public override float getDuration () {
		return 0.125f;
	}
	public override float getPierceChance () {
		return 100;
	}
}