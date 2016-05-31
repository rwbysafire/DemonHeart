using UnityEngine;
using System.Collections;

public class Kamikaze : Mob {

	private int speed = 105, followDistance = 10;
	private Vector3 playerPosition;
	
	public Sprite[] spriteWalk, spriteAttack;
	private int walkingFrame;
	private int idleFrame;
	
	public override string getName ()
	{
		return "Kamikaze";
	}
	
	void create (){
		body = new GameObject ("PlayerHead");
		body.transform.SetParent (transform);
		body.transform.position = transform.position;
        body.AddComponent<SpriteRenderer>();
        body.GetComponent<SpriteRenderer>().material = (Material)Resources.Load("MapMaterial");
        body.GetComponent<SpriteRenderer>().sortingOrder = 2;
		stats.baseStrength = -50;
		// K exp = 50
		stats.exp = 50;
	}
	
	// Use this for initialization
	public override void OnStart () {
        setDropRate(10);
        setArmourDrops(new int[] {0,1,2,3,4,5,6});
        setSkillDrops(new int[] {3,5,6});
        create ();
		transform.rotation = new Quaternion(0,0,0,0);
		spriteWalk = Resources.LoadAll<Sprite>("Sprite/demon");
        spriteAttack = Resources.LoadAll<Sprite>("Sprite/demonAttack");
        replaceSkill(0, new SkillSelfDestruct ());
	}
	
	// Update is called once per frame
	public override void OnUpdate () 
	{
		if (GameObject.FindWithTag ("Player") && Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= 1.5)
			skills[0].useSkill(this);
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
        float endTime = Time.fixedTime + attackTime;
        float remainingTime = endTime - Time.fixedTime;
        while (remainingTime > 0) {
            int currentFrame = (int)(((attackTime - remainingTime) / attackTime) * spriteAttack.Length);
            body.GetComponent<SpriteRenderer>().sprite = spriteAttack[currentFrame];
            yield return new WaitForSeconds(0);
            remainingTime = endTime - Time.fixedTime;
        }
        skill.skillLogic(this, stats);
    }
}
