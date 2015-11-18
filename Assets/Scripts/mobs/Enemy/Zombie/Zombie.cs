using UnityEngine;
using System.Collections;

public class Zombie : Mob {

	private int speed = 50, followDistance = 8;
	private bool isAttacking;
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
			StartCoroutine("playAttackAnimation", 0.1);
		}
	}

	public override Quaternion rotation {
		get {
			return body.transform.rotation;
		}
		set {
			body.transform.rotation = value;
		}
	}

	public override Transform feetTransform {
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
			Vector3 diff = (player.transform.position - position).normalized;
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			if (Mathf.Sqrt(Mathf.Pow(playerPosition.x - position.x, 2) + Mathf.Pow(playerPosition.y - position.y, 2)) <= followDistance) {
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
	
	IEnumerator playAttackAnimation(float delay) {
		int frame = 0;
		walkingFrame = 0;
		isAttacking = true;
		do {
			if(!isStunned()) {
				body.GetComponent<SpriteRenderer> ().sprite = spriteAttack[frame];
				if (frame == 6)
					skills[0].useSkill ();
					frame++;
			}
			yield return new WaitForSeconds(delay);
		} while (frame < spriteAttack.Length);
		isAttacking = false;
	}
}
