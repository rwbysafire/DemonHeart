using UnityEngine;
using System.Collections;

public class SkillTeleport : Skill
{
	private float maxDistance = 5;

	public SkillTeleport(Mob mob) : base(mob) { }

	public override string getName () {
		return "Teleport";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Sprite/flash");
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
		if (!Physics2D.OverlapPoint(teleportLocation))
			mob.position = teleportLocation;
		else {
			RaycastHit2D[] hit = Physics2D.LinecastAll(mob.position, teleportLocation);
			for (int x = hit.Length - 1; x >= 0; x--) {
				teleportLocation = hit[x].point;
				teleportLocation -= (mob.getTargetLocation() - mob.position).normalized * mob.gameObject.GetComponent<CircleCollider2D>().radius;
				if (Physics2D.OverlapPointAll(teleportLocation).Length == 0) {
					mob.position = hit[x].point;
					break;
				}
			}
		}
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Sound/teleport"), mob.position);
	}
}

