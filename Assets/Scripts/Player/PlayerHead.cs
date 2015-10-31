using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour {
	
	public Skill teleport;
	public Skill scattershot;
	public Skill powershot;
	public Skill basicAttack;
	public Skill volley;
	public Skill stunArrow;
	
	void Start()
	{
		teleport = new SkillTeleport (GameObject.Find ("Player"));
		scattershot = new SkillScattershot (gameObject);
		powershot = new SkillPowershot (gameObject);
		basicAttack = new SkillBasicAttack (gameObject);
		volley = new SkillVolley (gameObject);
		stunArrow = new SkillStunArrow (gameObject);
	}

	// Update is called once per frame
	void Update () {
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

		
		if (Input.GetKey (KeyCode.E)) 
			teleport.useSkill();
		if (Input.GetKey (KeyCode.Q)) 
			scattershot.useSkill ();
		if (Input.GetKey (KeyCode.R))
			powershot.useSkill (); 
		if (Input.GetKey (KeyCode.Mouse0))
			basicAttack.useSkill ();
		if (Input.GetKey (KeyCode.Mouse1))
			volley.useSkill ();
		if (Input.GetKey (KeyCode.C))
			stunArrow.useSkill ();
	}
}
