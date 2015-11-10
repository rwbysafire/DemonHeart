using UnityEngine;
using System.Collections;

public class Player : Mob {

	public float speed = 7;
	private GameObject head, feet;
	private Sprite[] headSprite, feetSprite;
	private int feetFrame = 0, headFrame = 0;
	private float feetTimer;

	public override string getName ()
	{
		return "Player";
	}

	void createPlayer (){
		headSprite = Resources.LoadAll<Sprite> ("Sprite/head");
		feetSprite = Resources.LoadAll<Sprite> ("Sprite/feet");
		//Create head
		head = new GameObject ("PlayerHead");
		head.transform.SetParent (transform);
		head.transform.position = transform.position;
		head.AddComponent<SpriteRenderer> ();
		head.GetComponent<SpriteRenderer> ().sprite = headSprite [0];
		head.GetComponent<SpriteRenderer> ().sortingOrder = 3;
		//Create feet
		feet = new GameObject ("PlayerFeet");
		feet.transform.SetParent (transform);
		feet.transform.position = transform.position;
		feet.AddComponent<SpriteRenderer> ();
		feet.GetComponent<SpriteRenderer> ().sortingOrder = 1;
	}

	public override void OnStart ()
	{
		createPlayer ();
		replaceSkill(0, new SkillBasicAttack (this));
		replaceSkill(1, new SkillVolley (this));
		replaceSkill(2, new SkillScattershot (this));
		replaceSkill(3, new SkillStunArrow (this));
		replaceSkill(4, new SkillTeleport (this));
		replaceSkill(5, new SkillSlash (this));
		replaceSkill(6, new SkillPowershot (this));
		replaceSkill(7, new SkillExplosiveArrow (this));
		stats.strength = 20;
		stats.dexterity = 30;
		stats.intelligence = 10;
	}

	public override void OnUpdate ()
	{
		if (Time.timeScale != 0) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				StartCoroutine("playFireAnimation");
				skills[0].useSkill();
			}
			if (Input.GetKey (KeyCode.Mouse1)) {
				StartCoroutine("playFireAnimation");
				skills[1].useSkill ();
			}
			if (Input.GetKey (KeyCode.Alpha1)) {
				StartCoroutine("playFireAnimation");
				skills[2].useSkill (); 
			}
			if (Input.GetKey (KeyCode.Alpha2)) {
				StartCoroutine("playFireAnimation");
				skills[3].useSkill ();
			}
			if (Input.GetKey (KeyCode.Alpha3)) {
				StartCoroutine("playFireAnimation");
				skills[4].useSkill ();
			}
			if (Input.GetKey (KeyCode.Alpha4)) {
				StartCoroutine("playFireAnimation");
				skills[5].useSkill ();
			}
			if (Input.GetKey (KeyCode.Alpha5)) {
				StartCoroutine("playFireAnimation");
				skills[6].useSkill ();
			}
			if (Input.GetKey (KeyCode.Alpha6)) {
				StartCoroutine("playFireAnimation");
				skills[7].useSkill ();
			}
		}
	}

	public override void OnFixedUpdate ()
	{
		//Displays and modifies player rotations
		feetLogic();headLogic();
	}

	public override Quaternion rotation {
		get {
			return head.transform.rotation;
		}
		set {
			head.transform.rotation = value;
		}
	}

	public override Vector3 getTargetLocation () {
		return new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, gameObject.transform.position.z);
	}

	void feetLogic () {
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (direction != Vector2.zero) {
			//Movement
			Vector2 directionMagnitude = new Vector2(Mathf.Abs(direction.normalized.x) * direction.x, Mathf.Abs(direction.normalized.y) * direction.y);
			GetComponent<Rigidbody2D>().AddForce(directionMagnitude * 100);
			//Feet
			float feetDirection = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg - 90;
			feet.transform.rotation = Quaternion.Euler (0f, 0f, feetDirection);
			if(feetFrame >= feetSprite.Length - 1)
				feetFrame = -1;
			if (feetTimer + (1.28 - Mathf.Pow(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude, 0.1f)) <= Time.fixedTime) {
				feet.GetComponent<SpriteRenderer> ().sprite = feetSprite[feetFrame += 1];
				feetTimer = Time.fixedTime;
			}
		} else {
			feet.GetComponent<SpriteRenderer> ().sprite = feetSprite[0];
			feetFrame = 0;
		}
	}
	
	void headLogic() {
		Vector3 direction = (getTargetLocation() - transform.position).normalized;
		float headDirection = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
		head.transform.rotation = Quaternion.Euler(0f, 0f, headDirection);
	}
	
	IEnumerator playFireAnimation() {
		for(headFrame = 1; headFrame < headSprite.Length; headFrame++) {
			head.GetComponent<SpriteRenderer> ().sprite = headSprite[headFrame];
			yield return null;
		}
	}
}
