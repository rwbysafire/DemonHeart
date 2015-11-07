using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public int speed, followDistance;
	private float stunTime = 0;
	private bool isAttacking;
	public Skill basicAttack;
	public Skill scattershot;
	public Skill slash;
	public Stats stats = new Stats();
	private Vector3 playerPosition;
	
	public Sprite[] spriteAttack;

	// Use this for initialization
	void Start () {
		basicAttack = new SkillBasicAttack (gameObject, stats);
		scattershot = new SkillScattershot (gameObject, stats);
		slash = new SkillSlash (gameObject, stats);
		stats.attackDamage = 5;
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
		if(!isStunned() && !isAttacking) {
			if (GameObject.FindWithTag ("Player") && Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= 2) {
				StartCoroutine("playAttackAnimation", 0.1);
			}
		}
	}

	void FixedUpdate() {
		if (GameObject.FindWithTag ("Player")) {
			if(!isStunned() && !isAttacking) {
				GameObject player = GameObject.FindWithTag ("Player");
				playerPosition = player.transform.position;
				Vector3 diff = player.transform.position - transform.position;
				diff.Normalize();
				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
				transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
				if (Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= followDistance) {
					GetComponent<Rigidbody2D>().AddForce(transform.up * speed);
				} else
					GetComponent<Rigidbody2D>().AddForce(transform.up * 30);
			}
		}
	}

	IEnumerator playAttackAnimation(float delay) {
		int frame = 0;
		isAttacking = true;
		gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
		do {
			GetComponent<SpriteRenderer> ().sprite = spriteAttack[frame];
			frame++;
			if (frame == 6)
				slash.useSkill ();
			yield return new WaitForSeconds(delay);
		} while (frame < spriteAttack.Length);
		isAttacking = false;
		gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
	}
}