using UnityEngine;
using System.Collections;

public class SkillMortar : Skill {


    public override string getName() {
        return "Mortar";
    }

    public override Sprite getImage() {
        return Resources.Load<Sprite>("Skills/Mortar/mortarIcon");
    }

    public override float getAttackSpeed() {
        return 0.5f;
    }

    public override void skillLogic(Entity entity, Stats stats) {
        Vector3 targetLocation = entity.getTargetLocation();

        GameObject hitIndicator = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Mortar/MortarHitIndicator"));
        hitIndicator.transform.position = targetLocation;
        hitIndicator.AddComponent<SpawnExplosion>();
        hitIndicator.GetComponent<SpawnExplosion>().delay = 1;
    }
}

class SpawnExplosion : MonoBehaviour {

    public float delay = 1;

    void Start() {
        StartCoroutine("delayedExplosion", delay);
    }

    IEnumerator delayedExplosion(float delay) {
        yield return new WaitForSeconds(delay);
        GameObject explosion = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Mortar/MortarExplosion"));
        explosion.transform.position = transform.position;
        explosion.transform.RotateAround(explosion.transform.position, Vector3.forward, Random.Range(0, 360));
        Destroy(gameObject);
    }
}