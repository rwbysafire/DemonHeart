using UnityEngine;
using System.Collections;

public class SkillTeleport : Skill
{
	private float maxDistance = 5;

	public SkillTeleport(Mob mob) : base(mob) { }

	public override string getName () {
		return "Teleport";
	}

	public override float getMaxCooldown () {
		return 2f * (1 - mob.stats.cooldownReduction / 100);
	}
	
	public override float getManaCost () {
		return 25;
	}

	public override void skillLogic() {
		Vector3 teleportLocation;
		if (Vector3.Distance(mob.position, mob.getTargetLocation()) > maxDistance)
			teleportLocation = mob.position + ((mob.getTargetLocation() - mob.position).normalized * maxDistance);
		else
			teleportLocation = mob.getTargetLocation();
		Collider2D[] overlap = Physics2D.OverlapPointAll(teleportLocation);
		if (Physics2D.OverlapPointAll(teleportLocation).Length == 0)
			mob.position = teleportLocation;
		else {
			RaycastHit2D[] hit = Physics2D.LinecastAll(mob.position, teleportLocation);
			mob.position = hit[hit.Length - overlap.Length].point;
		}
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sounds/teleport"), mob.position);
	}
}

