using UnityEngine;
using System.Collections;

public class SkillVolley : Skill {

	public SkillVolley(GameObject gameObject) : base(gameObject) { }
	
	public override string getName ()
	{
		return "Volley";
	}
	
	public override float getMaxCooldown ()
	{
		return 0.5f;
	}
	
	public override void skillLogic()
	{
		fireArrow(-15);fireArrow(-10);fireArrow(-5);fireArrow(0);fireArrow(5);fireArrow(10);fireArrow(15);
	}

	void fireArrow(int rotate = 0)
	{
		//Instantiates the projectile with some speed
		GameObject basicArrow = MonoBehaviour.Instantiate (Resources.Load ("Arrow_Placeholder")) as GameObject;
		basicArrow.GetComponent<basic_projectile>().speed = 20;
		basicArrow.GetComponent<basic_projectile>().damage = 3;
		basicArrow.GetComponent<basic_projectile>().pierceChance = 75;
		basicArrow.GetComponent<basic_projectile>().homing = true;
		//Initiates the projectile's position and rotation
		basicArrow.transform.position = this.getGameObject ().transform.position;
		basicArrow.transform.rotation = this.getGameObject ().transform.rotation;
		basicArrow.transform.Translate (Vector3.up * 0.7f);
		basicArrow.transform.RotateAround (basicArrow.transform.position, Vector3.forward, rotate);
	}
}
