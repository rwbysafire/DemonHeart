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
		return Resources.Load<Sprite>("Skills/Teleport/teleportIcon");
	}

	public override float getMaxCooldown () {
		return 2f * (1 - mob.stats.cooldownReduction / 100);
	}
	
	public override float getManaCost () {
		return 25;
	}

	public override void skillLogic() {
		GameObject teleport = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Teleport/Teleport"));
		LineRenderer lineRenderer = teleport.AddComponent<LineRenderer>();
		lineRenderer.material = Resources.Load<Material>("Skills/Teleport/blur");
		lineRenderer.SetColors(new Color(1,1,0,0.5f), new Color(1,1,0,1));
		lineRenderer.SetWidth(1, 1);
		lineRenderer.SetPosition(0, mob.position);
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/Teleport/teleport"), mob.position);
		displayFlash();
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
		lineRenderer.SetPosition(1, (mob.getTargetLocation() - mob.position).normalized*mob.gameObject.GetComponent<CircleCollider2D>().radius*2 + mob.position);
		displayFlash();
	}

	void displayFlash()
	{
		GameObject flash = new GameObject ();
		flash.transform.position = mob.position;
		flash.AddComponent<SpriteRenderer> ();
		flash.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Skills/Teleport/flash");
		flash.GetComponent<SpriteRenderer> ().color = Color.yellow;
		flash.GetComponent<SpriteRenderer> ().sortingOrder = 4;
		GameObject.Destroy (flash, 0.1f);
	}
}

