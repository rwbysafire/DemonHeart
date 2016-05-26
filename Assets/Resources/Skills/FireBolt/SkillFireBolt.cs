using UnityEngine;
using System.Collections;

public class SkillFireBolt : Skill {

    public Projectile projectile;
    ProjectileSkill projectileSkill = new ProjectileSkill();
    AoeSkill aoeSkill = new AoeSkill();

    public SkillFireBolt() : base() {
        addSkillType(projectileSkill);
        addSkillType(aoeSkill);
    }

    public override string getName() {
        return "Fire Bolt";
    }

    public override Sprite getImage() {
        return Resources.Load<Sprite>("Skills/FireBolt/fireBoltIcon");
    }

    public override float getAttackSpeed() {
        return 0.5f;
    }

    public override float getMaxCooldown() {
        return 1f;
    }

    public override float getManaCost() {
        return 5;
    }

    public override void skillLogic(Entity mob, Stats stats) {
        attack(mob, stats);
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/pew"), mob.headTransform.position);
    }

    void attack(Entity mob, Stats stats) {
        float spreadCalcDistance = 6;
        float startDistance = 2;
        float baseAngle = 10;
        float ratio = (Mathf.Clamp(Vector3.Distance(mob.getTargetLocation(), mob.headTransform.position), startDistance, spreadCalcDistance + startDistance) - startDistance) / spreadCalcDistance;
        float angleOfSpread = (((1 - ratio) * 2) + ratio) * baseAngle;
        for (int i = 0; i < properties["projectileCount"]; i++) {
            fireArrow(mob, stats, ((properties["projectileCount"] - 1) * angleOfSpread / -2) + i * angleOfSpread);
        }
    }

    void fireArrow(Entity mob, Stats stats, float rotate = 0) {
        //Instantiates the projectile with some speed
        GameObject basicArrow = MonoBehaviour.Instantiate<GameObject>(Resources.Load<GameObject>("Skills/FireBolt/FireBolt"));
        projectile = new FireBoltProjectile(basicArrow, stats, this);
        basicArrow.GetComponent<basic_projectile>().setProjectile(projectile);
        //Initiates the projectile's position and rotation
        basicArrow.transform.position = mob.headTransform.position;
        basicArrow.transform.rotation = mob.headTransform.rotation;
        basicArrow.transform.Translate(Vector3.up * 0.7f);
        basicArrow.transform.RotateAround(basicArrow.transform.position, Vector3.forward, rotate);
        projectile.projectileOnStart();
        projectile.chainTimes = properties["chainCount"];
    }
}

class FireBoltProjectile : Projectile {
    public FireBoltProjectile(GameObject gameObject, Stats stats, Skill skill) : base(gameObject, stats, skill) { }
    public override void OnHit() {
        RaycastHit2D[] hit = Physics2D.LinecastAll(gameObject.transform.position - gameObject.transform.up * 0.47f, gameObject.transform.position + gameObject.transform.up * 2f);
        Vector3 target = gameObject.transform.position;
        foreach (RaycastHit2D x in hit) {
            if (x.collider.CompareTag(collider.tag)) {
                target = x.point;
                break;
            }
        }
        GameObject explosion = GameObject.Instantiate(Resources.Load("Skills/FireBolt/fireBoltExplosion")) as GameObject;
        explosion.GetComponent<FireBoltExplosion>().damage = 50 + (2f * stats.abilityPower);
        explosion.GetComponent<FireBoltExplosion>().enemyTag = Mob.getEnemyTag(stats.tag);
        explosion.transform.position = target;
        explosion.transform.localScale = new Vector3(explosion.transform.localScale.x * skill.properties["areaOfEffect"], explosion.transform.localScale.y * skill.properties["areaOfEffect"], explosion.transform.localScale.z);
        explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
        AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/boom"), explosion.transform.position);
    }
    public override float getSpeed() {
        return 15;
    }
    public override float getDamage() {
        return 0;
    }
    public override float getDuration() {
        return 3f;
    }
}