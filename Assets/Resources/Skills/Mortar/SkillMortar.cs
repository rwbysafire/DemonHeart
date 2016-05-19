using UnityEngine;
using System.Collections;

public class SkillMortar : Skill {


    public SkillMortar() : base() {
        addBaseProperty("baseDamage", 150);
        addBaseProperty("duration", 1);
    }

    public override string getName() {
        return "Mortar";
    }

    public override Sprite getImage() {
        return Resources.Load<Sprite>("Skills/Mortar/mortarIcon");
    }

    public override float getAttackSpeed() {
        return 0.5f;
    }

    public override float getMaxCooldown() {
        return 1.5f;
    }

    public override void skillLogic(Entity entity, Stats stats) {
        Vector3 targetLocation = entity.getTargetLocation();

        GameObject hitIndicator = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Mortar/MortarHitIndicator"));
        hitIndicator.transform.position = targetLocation;
        hitIndicator.AddComponent<HitIndicator>();
        hitIndicator.GetComponent<HitIndicator>().skill = this;
        hitIndicator.GetComponent<HitIndicator>().stats = stats;
    }

    class HitIndicator : MonoBehaviour {

        public Skill skill;
        public Stats stats;

        void Start() {
            StartCoroutine("delayedExplosion", skill.properties["duration"]);
        }

        IEnumerator delayedExplosion(float delay) {
            yield return new WaitForSeconds(delay);
            GameObject explosion = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Mortar/MortarExplosion"));
            explosion.transform.position = transform.position;
            explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
            explosion.AddComponent<ExplosionLogic>();
            explosion.GetComponent<ExplosionLogic>().skill = skill;
            explosion.GetComponent<ExplosionLogic>().stats = stats;
            Destroy(gameObject);
        }
    }

    class ExplosionLogic : MonoBehaviour {

        public Skill skill;
        public Stats stats;

        void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag(Mob.getEnemyTag(stats.tag))) {
                collider.GetComponent<Mob>().hurt(skill.properties["baseDamage"]);
            }
        }
    }
}