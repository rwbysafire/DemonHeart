using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour{

    public float speed;
	public Skill teleport;
	public Skill scattershot;
	public Skill powershot;
	public Skill basicAttack;
	public Skill stunArrow;
	public Stats playerStats;

    void Start()
    {
		teleport = new SkillTeleport (GameObject.FindWithTag ("Player"), playerStats);
		scattershot = new SkillScattershot (GameObject.FindWithTag ("Player"), playerStats);
		powershot = new SkillPowershot (GameObject.FindWithTag ("Player"), playerStats);
		basicAttack = new SkillBasicAttack (GameObject.FindWithTag ("Player"), playerStats);
		stunArrow = new SkillStunArrow (GameObject.FindWithTag ("Player"), playerStats);
		playerStats.attackDamage = 5;
		playerStats.cooldown = 50;
    }

    void Update()
    {   //Rotate player based on mouse position
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        

        //WASD Movement in relation to the world
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed, Space.World);
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.World);

		if (Input.GetKey (KeyCode.E)) 
		{
			teleport.useSkill();
		}
		if (Input.GetKey (KeyCode.Q)) 
			scattershot.useSkill ();
		if (Input.GetKey (KeyCode.R))
			powershot.useSkill (); 
		if (Input.GetKey (KeyCode.Mouse0))
			basicAttack.useSkill ();
		if (Input.GetKey (KeyCode.X))
			stunArrow.useSkill ();
    }

	public static Vector3 getTargetLocation()
	{
		return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
	}
	
}
