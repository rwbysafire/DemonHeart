using UnityEngine;
using System.Collections;

public class SkillStunArrow : Skill 
{
	public SkillStunArrow(GameObject gameObject) : base(gameObject) { }
	
	public override string getName ()
	{
		return "Stun Arrow";
	}
	
	public override float getMaxCooldown ()
	{
		return 3f;
	}
	
	public override void skillLogic()
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate(Resources.Load("Arrow_Placeholder")) as GameObject;
		basicArrow.GetComponent<basic_projectile>().speed = 5;
		basicArrow.GetComponent<basic_projectile>().damage = 10;
		basicArrow.GetComponent<basic_projectile>().stunTime = 2;
		basicArrow.GetComponent<basic_projectile>().duration = 5;
		basicArrow.GetComponent<basic_projectile>().pierceChance = 100;
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject().transform.position;
		basicArrow.transform.rotation = this.getGameObject().transform.rotation;
		basicArrow.transform.localScale *= 4; 
		basicArrow.transform.Translate(Vector3.up * 0.7f);
	}	
}
