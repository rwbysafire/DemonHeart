using UnityEngine;
using System.Collections;

public class SkillRighteousFire : Skill {

	public SkillRighteousFire(Mob mob) : base(mob) { }
	
	public override string getName ()
	{
		return "Righteous Fire";
	}
	
	public override float getMaxCooldown ()
	{
		return 0.5f * (1 - (mob.stats.cooldownReduction / 100));
	}
	
	public override void skillLogic ()
	{

	}
}
