using UnityEngine;
using System.Collections;

public class SkillTeleport : Skill
{
	private float maxDistance = 5;

	public SkillTeleport() : base() { }

	public override string getName () {
		return "Teleport";
	}
	
	public override Sprite getImage () {
		return Resources.Load<Sprite>("Skills/Teleport/teleportIcon");
	}

	public override float getMaxCooldown () {
		return 2f;
	}
	
	public override float getManaCost () {
		return 25;
	}

	public override void skillLogic(Entity mob, Stats stats) {
		GameObject teleport = GameObject.Instantiate(Resources.Load<GameObject>("Skills/Teleport/Teleport"));
		LineRenderer lineRenderer = teleport.AddComponent<LineRenderer>();
		lineRenderer.material = Resources.Load<Material>("Skills/Teleport/blur");
		lineRenderer.SetColors(new Color(1,1,0,0.5f), new Color(1,1,0,1));
		lineRenderer.SetWidth(1, 1);
		lineRenderer.SetPosition(0, mob.feetTransform.position);
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/Teleport/teleport"), mob.feetTransform.position);
		displayFlash(mob);
		Vector3 teleportLocation;
		if (Vector3.Distance(mob.feetTransform.position, mob.getTargetLocation()) > maxDistance)
			teleportLocation = mob.feetTransform.position + ((mob.getTargetLocation() - mob.feetTransform.position).normalized * maxDistance);
		else
			teleportLocation = mob.getTargetLocation();
		if (!Physics2D.OverlapPoint(teleportLocation))
			mob.gameObject.transform.position = teleportLocation;
		else {
			RaycastHit2D[] hit = Physics2D.LinecastAll(mob.feetTransform.position, teleportLocation);
			for (int x = hit.Length - 1; x >= 0; x--) {
				teleportLocation = hit[x].point;
				teleportLocation -= (mob.getTargetLocation() - mob.feetTransform.position).normalized * mob.gameObject.GetComponent<CircleCollider2D>().radius;
				if (Physics2D.OverlapPointAll(teleportLocation).Length == 0) {
					mob.gameObject.transform.position = hit[x].point;
					break;
				}
			}
		}
		lineRenderer.SetPosition(1, (mob.getTargetLocation() - mob.feetTransform.position).normalized*mob.gameObject.GetComponent<CircleCollider2D>().radius*2 + mob.feetTransform.position);
		displayFlash(mob);
	}

	void displayFlash(Entity mob)
	{
		GameObject flash = new GameObject ();
		flash.transform.position = mob.feetTransform.position;
		flash.AddComponent<SpriteRenderer> ();
		flash.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Skills/Teleport/flash");
		flash.GetComponent<SpriteRenderer> ().color = Color.yellow;
		flash.GetComponent<SpriteRenderer> ().sortingOrder = 4;
		GameObject.Destroy (flash, 0.1f);
	}
}

