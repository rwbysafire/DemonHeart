using UnityEngine;
using System.Collections;

public class SkillScattershot : Skill
{
	public SkillScattershot(GameObject gameObject) : base(gameObject) {}

	private int arrowCount = 9;

	public override string getName ()
	{
		return "Scattershot";
	}

	public override float getMaxCooldown ()
	{
		return 5f;
	}

	public override void skillLogic ()
	{
		for (int i = 0; i < arrowCount; i++) 
		{
			fireArrow(-45 + (i * 90 / (arrowCount - 1)) + Random.Range(-5,5));
		}
	}

	void fireArrow(int rotate = 0)
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Arrow_Placeholder")) as GameObject;
		basicArrow.GetComponent<basic_projectile>().speed = 5;
		basicArrow.GetComponent<basic_projectile>().damage = 5;
		basicArrow.GetComponent<basic_projectile>().pierceChance = 100;
		basicArrow.GetComponent<basic_projectile> ().duration = 0.5f;

		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject ().transform.position;
		basicArrow.transform.rotation = this.getGameObject ().transform.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
	}
}
