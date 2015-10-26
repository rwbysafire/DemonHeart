using UnityEngine;
using System.Collections;

public class SkillTeleport : Skill
{
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
		this.getGameObject ().transform.Translate (Vector3.up * 10);
	}
}

