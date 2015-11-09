using UnityEngine;
using System.Collections;

public class SkillTeleport : Skill
{
	private float maxDistance = 5;

	public SkillTeleport(GameObject gameObject, Stats stats) : base(gameObject, stats) { }

	public override string getName ()
	{
		return "Teleport";
	}

	public override float getMaxCooldown ()
	{
		return 2f * (1 - getStats().cooldown / 100);
	}

	public override void skillLogic()
	{
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/teleport"), getGameObject().transform.position);
		getGameObject().transform.position = Vector3.MoveTowards(this.getGameObject().transform.position, CharControl.getTargetLocation(), maxDistance);
	}
}

