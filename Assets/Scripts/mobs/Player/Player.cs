using UnityEngine;
using System.Collections;

public class Player : Mob {

	public float speed = 7;
	private GameObject head, feet, flashlight;
	private Sprite[] headSprite, feetSprite;
	private int feetFrame = 0, headFrame = 0;
	private float feetTimer;
	public Skill[] listOfSkills;
	public InventoryUI inventory;

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
		head.GetComponent<SpriteRenderer> ().material = (Material) Resources.Load("MapMaterial");
		head.GetComponent<SpriteRenderer> ().sprite = headSprite [0];
		head.GetComponent<SpriteRenderer> ().sortingOrder = 3;
		//Create feet
		feet = new GameObject ("PlayerFeet");
		feet.transform.SetParent (transform);
		feet.transform.position = transform.position;
		feet.AddComponent<SpriteRenderer> ();
		feet.GetComponent<SpriteRenderer> ().material = (Material) Resources.Load("MapMaterial");
		feet.GetComponent<SpriteRenderer> ().sortingOrder = 1;
		//Create flashlight
		flashlight = Instantiate(Resources.Load ("Flashlight")) as GameObject;
		flashlight.transform.position = new Vector3(transform.position.x, transform.position.y, -0.45f);
		flashlight.transform.SetParent(head.transform);
	}

	public override void OnStart ()
	{
		listOfSkills = new Skill[12]{new SkillBasicAttack(this),
			new SkillChainLightning(this),
			new SkillCombatRoll(this),
			new SkillExplosiveArrow(this),
			new SkillPowershot(this),
			new SkillRighteousFire(this),
			new SkillScattershot(this),
			new SkillSelfDestruct(this),
			new SkillSlash(this),
			new SkillStunArrow(this),
			new SkillTeleport(this),
			new SkillVolley(this)};
		createPlayer ();
		replaceSkill(0, listOfSkills[0]);
		replaceSkill(1, listOfSkills[1]);
		replaceSkill(2, listOfSkills[2]);
		replaceSkill(3, listOfSkills[4]);
		replaceSkill(4, listOfSkills[10]);
		replaceSkill(5, listOfSkills[3]);
		stats.strength = 20;
		stats.dexterity = 30;
		stats.intelligence = 10;
	}

	public override void OnUpdate ()
	{
		if (Time.timeScale != 0) {
			if (Input.GetKey (KeyCode.Mouse0)) {
				skills[0].useSkill();
			}
			if (Input.GetKey (KeyCode.Mouse1)) {
				skills[1].useSkill ();
			}
			if (Input.GetKey (KeyCode.Alpha1)) {
				skills[2].useSkill (); 
			}
			if (Input.GetKey (KeyCode.Alpha2)) {
				skills[3].useSkill ();
			}
			if (Input.GetKey (KeyCode.Alpha3)) {
				skills[4].useSkill ();
			}
			if (Input.GetKey (KeyCode.Alpha4)) {
				skills[5].useSkill ();
			}
            foreach (Skill skill in skills) {
                if (skill != null)
                    skill.skillPassive();
            }
		}

		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit();
		}
	}

	public override void OnFixedUpdate ()
	{
		//Displays and modifies player rotations
		headLogic();
        foreach (Skill skill in skills) {
            if (skill != null)
                skill.skillFixedUpdate();
        }
	}

	public override void movement() {
		feetLogic();
	}
	
	public override Transform headTransform {
		get {
			return head.transform;
		}
	}

	public override Transform feetTransform {
		get {
			return feet.transform;
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

    public override void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Drop")) {
            if (collider.name == "DexterityGem(Clone)")
            {
                stats.dexterity += 2;
            }
            else if (collider.name == "StrengthGem(Clone)")
            {
                stats.strength += 2;
            }
            else if (collider.name == "IntelGem(Clone)")
            {
                stats.intelligence += 2;
            }
            Destroy(collider.gameObject);
		}
    }

	void OnTriggerStay2D (Collider2D collider) {
		if (collider.tag.ToLower ().StartsWith ("item") && Input.GetKeyDown (KeyCode.E)) {
			if (inventory.AddItem (collider.gameObject)) {
				Destroy (collider.gameObject);
			} else {
				Debug.Log ("Item not picked due to full capacity");
			}
		}
	}

	public override IEnumerator playAttackAnimation(Skill skill, float attackTime) {
		for(headFrame = 1; headFrame < headSprite.Length; headFrame++) {
			head.GetComponent<SpriteRenderer> ().sprite = headSprite[headFrame];
			if (headFrame == 2)
				skill.skillLogic();
			yield return new WaitForSeconds(attackTime/headSprite.Length);
		}
		isAttacking = false;
	}
}
