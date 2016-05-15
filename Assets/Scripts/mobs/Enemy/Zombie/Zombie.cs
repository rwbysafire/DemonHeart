using UnityEngine;
using System.Collections;

public class Zombie : Mob {

	private int speed = 50, followDistance = 8;
	private Vector3 playerPosition;
	
	public Sprite[] spriteAttack, spriteWalk, spriteIdle;
	private int walkingFrame;
	private int idleFrame;

	public override string getName ()
	{
		return "Zombie";
	}

	void create (){
		body = new GameObject ("PlayerHead");
		body.transform.SetParent (transform);
		body.transform.position = transform.position;
        body.AddComponent<SpriteRenderer>();
        body.GetComponent<SpriteRenderer>().material = (Material)Resources.Load("MapMaterial");
        body.GetComponent<SpriteRenderer>().sortingOrder = 2;
		stats.baseStrength = 20;
        stats.baseDexterity = -50;
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
		replaceSkill(0, new SkillSlash ());
		replaceSkill(1, new SkillCombatRoll ());
	}

	// Update is called once per frame
	public override void OnUpdate () 
	{
		if(isAttacking)
			return;
		if (GameObject.FindWithTag ("Player") && Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= 2) {
			skills[0].useSkill (this);
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
			Vector3 diff = (getTargetLocation() - feetTransform.position).normalized;
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
			headTransform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
			if (Mathf.Sqrt(Mathf.Pow(playerPosition.x - feetTransform.position.x, 2) + Mathf.Pow(playerPosition.y - feetTransform.position.y, 2)) <= followDistance) {
				GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed);
			} else
				GetComponent<Rigidbody2D>().AddForce(feetTransform.up * speed/2);
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
        walkingFrame = 0;
        bool hasntAttacked = true;
        float endTime = Time.fixedTime + attackTime;
        float remainingTime = endTime - Time.fixedTime;
        while (remainingTime > 0) {
            int currentFrame = (int)(((attackTime - remainingTime) / attackTime) * spriteAttack.Length);
            body.GetComponent<SpriteRenderer>().sprite = spriteAttack[currentFrame];
            if (hasntAttacked && currentFrame > 3) {
                hasntAttacked = false;
                skill.skillLogic(this, stats);
            }
            yield return new WaitForSeconds(0);
            remainingTime = endTime - Time.fixedTime;
        }
        body.GetComponent<SpriteRenderer>().sprite = spriteAttack[0];
        if (hasntAttacked)
            skill.skillLogic(this, stats);
        isAttacking = false;
    }
}
