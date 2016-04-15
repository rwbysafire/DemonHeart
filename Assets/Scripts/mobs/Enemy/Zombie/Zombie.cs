using UnityEngine;
using System.Collections;

public class Zombie : Mob {

	private int speed = 50, followDistance = 8;
	private Vector3 playerPosition;
	
	public Sprite[] spriteAttack, spriteWalk, spriteIdle;
	private int walkingFrame;
	private int idleFrame;

	private GameObject body;

	public override string getName ()
	{
		return "Zombie";
	}

	public override Vector3 getTargetLocation ()
	{
		return playerPosition;
	}

	void create (){
		body = new GameObject ("PlayerHead");
		body.transform.SetParent (transform);
		body.transform.position = transform.position;
		body.AddComponent<SpriteRenderer>();
		body.GetComponent<SpriteRenderer>().sortingOrder = 2;
		stats.strength = 20;
		// Z exp = 100
		stats.exp = 100;
	}

	// Use this for initialization
	public override void OnStart () {
		create ();
		transform.rotation = new Quaternion(0,0,0,0);
		spriteAttack = Resources.LoadAll<Sprite>("Sprite/zombieAttack");
		spriteWalk = Resources.LoadAll<Sprite>("Sprite/zombieWalk");
		spriteIdle = Resources.LoadAll<Sprite>("Sprite/zombieIdle");
		replaceSkill(0, new SkillSlash (this));
		replaceSkill(1, new SkillCombatRoll (this));
	}

	// Update is called once per frame
	public override void OnUpdate () 
	{
		if(isAttacking)
			return;
		if (GameObject.FindWithTag ("Player") && Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= 2) {
			skills[0].useSkill ();
		}
	}

	public override Transform headTransform {
		get {
			return body.transform;
		}
	}

	private float timer;
	public override void movement () {
		if (isAttacking)
			return;
		if (GameObject.FindWithTag ("Player")) {
			GameObject player = GameObject.FindWithTag ("Player");
			playerPosition = player.transform.position;
			Vector3 diff = (player.transform.position - feetTransform.position).normalized;
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			headTransform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			if (Mathf.Sqrt(Mathf.Pow(playerPosition.x - feetTransform.position.x, 2) + Mathf.Pow(playerPosition.y - feetTransform.position.y, 2)) <= followDistance) {
				GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed);
			} else
				GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed/4);
		}
		if (gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 0.05f) {
			if (timer + (1.20 - Mathf.Pow(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude, 0.1f)) <= Time.fixedTime) {
				if(walkingFrame >= spriteWalk.Length)
					walkingFrame = 0;
				body.GetComponent<SpriteRenderer> ().sprite = spriteWalk[walkingFrame];
				walkingFrame++;
				timer = Time.fixedTime;
			}
		}
	}
	
	public override IEnumerator playAttackAnimation(Skill skill, float attackTime) {
		int frame = 0;
		walkingFrame = 0;
		do {
			if(!isStunned()) {
				body.GetComponent<SpriteRenderer> ().sprite = spriteAttack[frame];
				if (frame == 6)
					skill.skillLogic ();
					frame++;
			}
			yield return new WaitForSeconds(attackTime/spriteAttack.Length);
		} while (frame < spriteAttack.Length);
		isAttacking = false;
	}
}
