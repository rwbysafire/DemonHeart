using UnityEngine;
using System.Collections;

public class SkillRighteousFire : Skill {

	public SkillRighteousFire(Mob mob) : base(mob) { }
	
	public override string getName () {
		return "Fire Aura";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/RighteousFire/righteousFireIcon");
	}
	
	public override float getMaxCooldown () {
		return 0.5f * (1 - (mob.stats.cooldownReduction / 100));
	}
	
	public override float getManaCost () {
		return 0;
	}
	
	public override void skillLogic () {
		if (mob.gameObject.transform.FindChild("RighteousFire(Clone)") == null) {
			GameObject righteousFire = GameObject.Instantiate(Resources.Load<GameObject>("Skills/RighteousFire/RighteousFire"));
			righteousFire.transform.position = mob.feetTransform.position;
			righteousFire.transform.SetParent(mob.gameObject.transform);
			righteousFire.GetComponent<RighteousFire>().mob = mob;
		}
	}
}
