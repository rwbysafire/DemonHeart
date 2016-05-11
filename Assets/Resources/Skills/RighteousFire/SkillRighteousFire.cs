﻿using UnityEngine;
using System.Collections;

public class SkillRighteousFire : Skill {

	public SkillRighteousFire() : base() { }
	
	public override string getName () {
		return "Fire Aura";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/RighteousFire/righteousFireIcon");
	}
	
	public override float getMaxCooldown () {
		return 0.5f;
	}
	
	public override float getManaCost () {
		return 20;
	}
	
	public override void skillLogic (Entity mob, Stats stats) {
		if (mob.gameObject.transform.FindChild("RighteousFire(Clone)") == null) {
			GameObject righteousFire = GameObject.Instantiate(Resources.Load<GameObject>("Skills/RighteousFire/RighteousFire"));
			righteousFire.transform.position = mob.feetTransform.position;
			righteousFire.transform.SetParent(mob.gameObject.transform);
			righteousFire.GetComponent<RighteousFire>().mob = mob.gameObject.GetComponent<Mob>();
            righteousFire.GetComponent<RighteousFire>().manaCost = properties["manaCost"];
        }
	}
}
