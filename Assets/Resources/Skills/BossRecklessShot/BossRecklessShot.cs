using UnityEngine;
using System.Collections;

public class BossRecklessShot : Skill
{
	public Projectile projectile;
    ProjectileSkill projectileSkill = new ProjectileSkill();
    AoeSkill aoeSkill = new AoeSkill();

    public BossRecklessShot() : base() {
        projectileSkill.setProjectileCount(15);
        addSkillType(projectileSkill);
        addSkillType(aoeSkill);
    }
	
	public override string getName () {
		return "Boss Reckless Shot";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/Scattershot/scattershotIcon");
	}
	
	public override float getMaxCooldown () {
		return 5f;
	}
	
	public override float getManaCost () {
		return 25;
	}
	
	public override void skillLogic (Entity mob, Stats stats) {
        GameObject recklessShotAttack = new GameObject("RecklessShotAttack");
        recklessShotAttack.transform.position = mob.feetTransform.position;
        recklessShotAttack.transform.rotation = mob.headTransform.rotation;
        recklessShotAttack.transform.SetParent(mob.gameObject.transform);
        recklessShotAttack.AddComponent<BossRecklessShotAttack>();
        recklessShotAttack.GetComponent<BossRecklessShotAttack>().skill = this;
        recklessShotAttack.GetComponent<BossRecklessShotAttack>().stats = stats;

    }
}

class BossRecklessShotAttack : MonoBehaviour {

    public Skill skill;
    public Stats stats;
    private float angle;
    private int projectileCount;

    void Start() {
        projectileCount = (int)skill.properties["projectileCount"];
        angle = 360f / (projectileCount-1);
        StartCoroutine("delayedAttack", 0.1f);
    }

    IEnumerator delayedAttack(float delay) {
        for (int i = 0; i < projectileCount; i++) {
            fireArrow(i * angle);
            fireArrow(i * angle + 180);
            AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/pew"), transform.position);
            yield return new WaitForSeconds(delay);
        }
        Destroy(gameObject);
    }

    void fireArrow(float rotate = 0) {
        //Instantiates the projectile with some speed
        GameObject basicArrow = Instantiate<GameObject>(Resources.Load<GameObject>("Skills/Firebolt/FireBolt"));
        Projectile projectile = new BossRecklessShotProjectile(basicArrow, stats, skill);
        basicArrow.GetComponent<basic_projectile>().setProjectile(projectile);
        //Initiates the projectile's position and rotation
        basicArrow.transform.position = transform.position;
        basicArrow.transform.rotation = transform.rotation;
        basicArrow.transform.RotateAround(basicArrow.transform.position, Vector3.forward, rotate);
        basicArrow.transform.Translate(Vector3.up * 0.7f);
        projectile.projectileOnStart();
        projectile.chainTimes = skill.properties["chainCount"];
    }
}

class BossRecklessShotProjectile : Projectile {
	public BossRecklessShotProjectile(GameObject gameObject, Stats stats, Skill skill) : base(gameObject, stats, skill) {}
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