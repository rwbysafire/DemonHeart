using UnityEngine;
using System.Collections;

public class SkillPowershot : Skill
{
	public SkillPowershot(GameObject gameObject) : base(gameObject) { }

	public override string getName ()
	{
		return "Powershot";
	}
	
	public override float getMaxCooldown ()
	{
		return 5f;
	}
	
	public override void skillLogic()
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		basicArrow.GetComponent<basic_projectile>().speed = 20;
		basicArrow.GetComponent<basic_projectile>().damage = 20;
		basicArrow.GetComponent<basic_projectile>().pierceChance = 100;
		//Initiates the projectile's position and rotation
		basicArrow.transform.localScale = basicArrow.transform.localScale * 1;
		basicArrow.transform.position = this.getGameObject().transform.position;
		basicArrow.transform.rotation = this.getGameObject().transform.rotation;
		basicArrow.transform.Translate(Vector3.up * 0.7f);
	}
}

