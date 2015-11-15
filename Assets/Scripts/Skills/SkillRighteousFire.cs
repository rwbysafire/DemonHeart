﻿using UnityEngine;
using System.Collections;

public class SkillRighteousFire : Skill {

	public SkillRighteousFire(Mob mob) : base(mob) { }
	
	public override string getName () {
		return "Righteous Fire";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Sprite/righteousFireIcon");
	}
	
	public override float getMaxCooldown () {
		return 0.5f * (1 - (mob.stats.cooldownReduction / 100));
	}
	
	public override float getManaCost () {
		return 0;
	}
	
	public override void skillLogic () {
		if (mob.gameObject.transform.FindChild("RighteousFire(Clone)") == null) {
			GameObject righteousFire = GameObject.Instantiate(Resources.Load<GameObject>("RighteousFire"));
			righteousFire.transform.position = mob.position;
			righteousFire.transform.SetParent(mob.gameObject.transform);
			righteousFire.GetComponent<RighteousFire>().mob = mob;
		}
	}
}
