using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 7;
	private GameObject head, feet;
	public Sprite[] headSprite, feetSprite;
	public float headDirection, feetDirection;
	private int feetFrame = 0, headFrame = 0;
	private float feetTimer;

	public Skill teleport;
	public Skill scattershot;
	public Skill powershot;
	public Skill basicAttack;
	public Skill volley;
	public Skill stunArrow;
	public Stats playerStats = new Stats();

	void Start () {
		//Create head
		head = new GameObject ("PlayerHead");
		head.transform.SetParent(transform);
		head.tag = ("Player");
		head.AddComponent<SpriteRenderer> ();
		head.GetComponent<SpriteRenderer> ().sprite = headSprite[0];
		//Create feet
		feet = new GameObject ("PlayerFeet");
		feet.transform.SetParent(transform);
		feet.tag = ("Player");
		feet.AddComponent<SpriteRenderer> ();

		teleport = new SkillTeleport (gameObject, playerStats);
		scattershot = new SkillScattershot (head, playerStats);
		powershot = new SkillPowershot (head, playerStats);
		basicAttack = new SkillBasicAttack (head, playerStats);
		volley = new SkillVolley (head, playerStats);
		stunArrow = new SkillStunArrow (head, playerStats);
		playerStats.attackDamage = 5;
		playerStats.cooldown = 50;
	}

	void Update () {
		if (Input.GetKey (KeyCode.E)) {
			StartCoroutine("playFireAnimation");
			teleport.useSkill();
		}
		if (Input.GetKey (KeyCode.Q)) {
			StartCoroutine("playFireAnimation");
			scattershot.useSkill ();
		}
		if (Input.GetKey (KeyCode.R)) {
			StartCoroutine("playFireAnimation");
			powershot.useSkill (); 
		}
		if (Input.GetKey (KeyCode.Mouse0)) {
			StartCoroutine("playFireAnimation");
			basicAttack.useSkill ();
		}if (Input.GetKey (KeyCode.Mouse1)) {
			StartCoroutine("playFireAnimation");
			volley.useSkill ();
		}
		if (Input.GetKey (KeyCode.C)) {
			StartCoroutine("playFireAnimation");
			stunArrow.useSkill ();
		}
	}

	void FixedUpdate() {
		//Displays and modifies player rotations
		feetLogic();headLogic();
	}

	void feetLogic () {
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (direction != Vector2.zero) {
			//Movement
			Vector2 directionMagnitude = new Vector2(Mathf.Abs(direction.normalized.x) * direction.x, Mathf.Abs(direction.normalized.y) * direction.y);
			GetComponent<Rigidbody2D>().AddForce(directionMagnitude * 100);
			//Feet
			feetDirection = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;
			feet.transform.rotation = Quaternion.Euler (0f, 0f, feetDirection);
			if(feetFrame >= feetSprite.Length - 1)
				feetFrame = -1;
			if (feetTimer + (1.03 - Mathf.Pow(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude, 0.1f)) <= Time.fixedTime) {
				feet.GetComponent<SpriteRenderer> ().sprite = feetSprite[feetFrame += 1];
				feetTimer = Time.fixedTime;
			}
		} else {
			feet.GetComponent<SpriteRenderer> ().sprite = feetSprite[0];
			feetFrame = 0;
		}
	}

	void headLogic() {
		Vector3 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
		headDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
		head.transform.rotation = Quaternion.Euler(0f, 0f, headDirection);
	}

	IEnumerator playFireAnimation() {
		for(headFrame = 1; headFrame < headSprite.Length; headFrame++) {
			head.GetComponent<SpriteRenderer> ().sprite = headSprite[headFrame];
			yield return null;
		}
	}
}
