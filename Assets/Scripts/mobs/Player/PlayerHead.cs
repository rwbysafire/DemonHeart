using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour {
	
	public Skill teleport;
	public Skill scattershot;
	public Skill powershot;
	public Skill basicAttack;
	public Skill volley;
	public Skill stunArrow;
	public Stats playerStats = new Stats();
	
	void Start()
	{
		teleport = new SkillTeleport (GameObject.Find ("Player"), playerStats);
		scattershot = new SkillScattershot (gameObject, playerStats);
		powershot = new SkillPowershot (gameObject, playerStats);
		basicAttack = new SkillBasicAttack (gameObject, playerStats);
		volley = new SkillVolley (gameObject, playerStats);
		stunArrow = new SkillStunArrow (gameObject, playerStats);
		playerStats.attackDamage = 5;
		playerStats.cooldown = 50;
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
