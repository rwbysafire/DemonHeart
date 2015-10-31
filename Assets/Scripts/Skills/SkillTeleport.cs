using UnityEngine;
using System.Collections;

public class SkillTeleport : Skill
{
	private float maxDistance = 5;

	public SkillTeleport(GameObject gameObject) : base(gameObject) { }

	public override string getName ()
	{
		return "Teleport";
	}

	public override float getMaxCooldown ()
	{
		return 2f;
	}

	public override void skillLogic()
	{
		this.getGameObject().transform.position = Vector3.MoveTowards(this.getGameObject().transform.position, CharControl.getTargetLocation(), maxDistance);
	}
}

