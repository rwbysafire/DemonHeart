using UnityEngine;
using System.Collections;

public class PlayerHead : MonoBehaviour {
	
	public Skill teleport;
	public Skill scattershot;
	public Skill powershot;
	public Skill basicAttack;
	
	void Start()
	{
		teleport = new SkillTeleport (GameObject.FindWithTag ("Player"));
		scattershot = new SkillScattershot (GameObject.Find ("Head"));
		powershot = new SkillPowershot (GameObject.Find ("Head"));
		basicAttack = new SkillBasicAttack (GameObject.Find ("Head"));
	}

	// Update is called once per frame
	void Update () {
		transform.position = GameObject.Find ("Feet").transform.position;
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
		diff.Normalize();
		
		float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

		
		if (Input.GetKey (KeyCode.E)) 
		{
			teleport.useSkill();
		}
		if (Input.GetKey (KeyCode.Q)) {
		}
		//scattershot.useSkill ();
		if (Input.GetKey (KeyCode.R))
			powershot.useSkill (); 
		if (Input.GetKey (KeyCode.Mouse0))
			basicAttack.useSkill ();
	}
}
