using UnityEngine;
using System.Collections;

public class SkillMortar : Skill {


    public SkillMortar() : base() {
        addBaseProperty("baseDamage", 150);
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
        return 2;
    }

    public override void skillLogic(Entity entity, Stats stats) {
        Vector3 targetLocation = entity.getTargetLocation();

        GameObject hitIndicator = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Mortar/MortarHitIndicator"));
        hitIndicator.transform.position = targetLocation;
        hitIndicator.AddComponent<HitIndicator>();
        hitIndicator.GetComponent<HitIndicator>().skill = this;
        hitIndicator.GetComponent<HitIndicator>().stats = stats;
        hitIndicator.GetComponent<HitIndicator>().delay = 1;
    }

    class HitIndicator : MonoBehaviour {

        public Skill skill;
        public Stats stats;
        public float delay = 1;

        void Start() {
            StartCoroutine("delayedExplosion", delay);
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