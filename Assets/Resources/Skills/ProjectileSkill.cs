using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProjectileSkill : SkillType {

    private Dictionary<string, float> Properties = new Dictionary<string, float>();
    public Dictionary<string, float> properties {
        get {
            return Properties;
        }
    }

    public ProjectileSkill() {
        Properties.Add("projectileCount", 1);
        Properties.Add("projectileSpeed", 40);
        Properties.Add("chainCount", 0);
    }

    public void setProjectileCount(int projectileCount) {
        Properties["projectileCount"] = projectileCount;
    }

    //public void shoot(Mob mob) {
    //    float y = Vector3.Distance(mob.getTargetLocation(), mob.transform.position);
    //    if (y > 6)
    //        y = 6;
    //    else if (y < 2)
    //        y = 2;
    //    float angleOfSpread = (((1 - (y - 2) / 4) * 3) + 1) * 5;
    //    for (int i = 0; i < properties["projectileCount"]; i++) {
    //        fireProjectile(mob, ((properties["projectileCount"] - 1) * angleOfSpread / -2) + i * angleOfSpread);
    //    }
    //}

    //void fireProjectile(Mob mob, float rotate = 0) {
    //    //Instantiates the projectile with some speed
    //    GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Skills/Arrow_Placeholder")) as GameObject;
    //    GameObject arrowGlow = MonoBehaviour.Instantiate(Resources.Load("ShotGlow")) as GameObject;
    //    arrowGlow.transform.position = new Vector3(basicArrow.transform.position.x, basicArrow.transform.position.y, -0.3f);
    //    arrowGlow.transform.SetParent(basicArrow.transform);
    //    projectile = new BasicAttackProjectile(basicArrow, mob);
    //    basicArrow.GetComponent<basic_projectile>().setProjectile(projectile);
    //    //Initiates the projectile's position and rotation
    //    basicArrow.transform.position = mob.headTransform.position;
    //    basicArrow.transform.rotation = mob.headTransform.rotation;
    //    basicArrow.transform.Translate(Vector3.up * 1.2f);
    //    basicArrow.transform.RotateAround(basicArrow.transform.position, Vector3.forward, rotate);
    //    projectile.projectileOnStart();
    //    projectile.chainTimes = properties["chainCount"];
    //}
}
