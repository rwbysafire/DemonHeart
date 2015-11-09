using UnityEngine;
using System.Collections;

public class SkillRighteousFire : Skill {

	public SkillRighteousFire(GameObject gameObject, Stats stats) : base(gameObject, stats) { }
	
	public override string getName ()
	{
		return "Righteous Fire";
	}
	
	public override float getMaxCooldown ()
	{
		return 0.5f * (1 - (getStats().cooldown / 100));
	}
	
	public override void skillLogic ()
	{

	}
}
