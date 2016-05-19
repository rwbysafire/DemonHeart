using UnityEngine;
using System.Collections;

public class SkillSlam : Skill {


    public SkillSlam() : base() {
        addBaseProperty("baseDamage", 150);
    }

    public override string getName() {
        return "Slam";
    }

    public override Sprite getImage() {
        return Resources.Load<Sprite>("Skills/Slam/slamIcon");
    }

    public override float getAttackSpeed() {
        return 1f;
    }

    public override float getMaxCooldown() {
        return 4;
    }

    public override void skillLogic(Entity entity, Stats stats) {
        Vector3 targetLocation = entity.feetTransform.position;

        GameObject hitIndicator = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Slam/SlamHitIndicator"));
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
            //GameObject explosion = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Mortar/MortarExplosion"));
            //explosion.transform.position = transform.position;
            //explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
            //explosion.AddComponent<ExplosionLogic>();
            //explosion.GetComponent<ExplosionLogic>().skill = skill;
            //explosion.GetComponent<ExplosionLogic>().stats = stats;
            Destroy(gameObject);
        }
    }
}
