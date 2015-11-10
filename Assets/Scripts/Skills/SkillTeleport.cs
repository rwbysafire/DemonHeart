using UnityEngine;
using System.Collections;

public class SkillTeleport : Skill
{
	private float maxDistance = 5;

	public SkillTeleport(Mob mob) : base(mob) { }

	public override string getName ()
	{
		return "Teleport";
	}

	public override float getMaxCooldown ()
	{
		return 2f * (1 - mob.stats.cooldownReduction / 100);
	}

	public override void skillLogic()
	{
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/teleport"), mob.position);
		mob.position = Vector3.MoveTowards(mob.position, mob.getTargetLocation(), maxDistance);
	}
}

