using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public int speed, followDistance;
	private float stunTime = 0;
	public Skill basicAttack;
	public Skill scattershot;
	public Stats stats = new Stats();

	// Use this for initialization
	void Start () {
		basicAttack = new SkillBasicAttack (gameObject, stats);
		scattershot = new SkillScattershot (gameObject, stats);
		stats.attackDamage = 1;
		stats.cooldown = -100;
	}

	public bool isStunned()
	{
		return stunTime >= Time.fixedTime;
	}

	public void addStunTime(float t)
	{
		if (isStunned ()) 
			stunTime += t;
		else
			stunTime = Time.fixedTime + t; 
	}

	public void setStunTime(float t)
	{
		if(getStunRemaining() < t)
			stunTime = Time.fixedTime + t;
	}

	public float getStunRemaining()
	{
		float output = stunTime - Time.fixedTime;
		if (output < 0)
			output = 0;
		return output; 
	}

	// Update is called once per frame
	void Update () 
	{
		if (GameObject.FindWithTag ("Player")) 
		{
			if(!isStunned())
			{
				GameObject player = GameObject.FindWithTag ("Player");
				Vector3 playerPosition = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 10);
				if (Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= followDistance) 
				{
					Vector3 diff = player.transform.position - transform.position;
	                diff.Normalize();
	                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
	                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

					transform.Translate (Vector3.up * speed * Time.deltaTime);
					if (Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= 8)
						basicAttack.useSkill ();
					if (Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= 2)
						scattershot.useSkill ();
				}
			}
		}
	}
}
