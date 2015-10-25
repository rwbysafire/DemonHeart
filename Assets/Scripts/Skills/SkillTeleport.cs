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
		Vector3 mousePosition = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
		                                    Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
		                                    this.getGameObject().transform.position.z);
		this.getGameObject().transform.position = Vector3.MoveTowards(this.getGameObject().transform.position, mousePosition, 20);
	}
}

