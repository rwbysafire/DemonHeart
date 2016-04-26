using UnityEngine;
using System.Collections;

public abstract class Mob : MonoBehaviour{
	
	public Stats stats = new Stats();
	public Buff buff = new Buff();
	private float stunTime = 0;
	private float canMove = 0;

	private float lasthit;
	public float lastHit{get{return lasthit;}}
	public bool isAttacking = false;
	public Skill[] skills = new Skill[6];

	public string[] dropTable = { "Gems/ArmorGem", "Gems/SkillGem" };/*"Gems/StrengthGem", "Gems/DexterityGem", "Gems/IntelGem",*/

	public void replaceSkill(int skillNum, Skill skill) {
		skills[skillNum] = skill;
	}

	public virtual Transform headTransform {
		get {
			return gameObject.transform;
		}
	}

	public virtual Transform feetTransform {
		get {
			return headTransform;
		}
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

	public bool hurt(float damage) {
		stats.health -= damage;
		lasthit = Time.fixedTime;
		return stats.health <= 0;
	}

	public bool useMana(float amount) {
		bool enoughMana = (Mathf.Ceil(stats.mana) >= amount);
		if (enoughMana)
			stats.mana -= amount;
		return enoughMana;
	}

	public abstract string getName();
	public abstract Vector3 getTargetLocation();
	void Start() {
		OnStart();
		stats.health = stats.maxHealth;
		stats.mana = stats.maxMana;
	}

	void Update() {
		if (stats.health <= 0)
		{
			GameObject.Find ("Player").GetComponent<Mob> ().stats.exp += stats.exp;
			DropItem();
			OnDeath();
			Destroy(gameObject);
		}
		if(isStunned())
			return;
		OnUpdate();
		if (stats.health < stats.maxHealth)
			stats.health += stats.healthRegen * Time.deltaTime;
		else if (stats.health > stats.maxHealth)
			stats.health = stats.maxHealth;
		if (stats.mana < stats.maxMana)
			stats.mana += stats.manaRegen * Time.deltaTime;
		else if (stats.mana > stats.maxMana)
			stats.mana = stats.maxMana;
		if (stats.exp >= stats.threshold) {
			stats.level++;
			stats.threshold = (stats.level + 1) * stats.threshold;
		}
	}

	public virtual void OnDeath() {}

    void DropItem() {
        if (Random.Range(1, 101) <= 100) {
			GameObject Drop = (GameObject) Instantiate(
				Resources.Load<GameObject>(dropTable[Random.Range(0, dropTable.Length)]),
				this.gameObject.transform.position + new Vector3 (Random.Range (2f, 5f), Random.Range (2f, 5f), 0),
				Quaternion.identity);
            Drop.transform.position = feetTransform.position;
        }

    }

	void FixedUpdate() {
		if(isStunned())
			return;
		if(canMove < Time.fixedTime)
			movement();
		OnFixedUpdate();
	}

	public void disableMovement(float time) {
		canMove = Time.fixedTime + time;
	}

	public float getCanMove() {
		return canMove;
	}

	public string getEnemyTag() {
		if (gameObject.tag == "Player" || gameObject.tag == "Ally")
			return "Enemy"; 
		else
			return "Player";
	}

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        
    }

	public abstract void movement();
	public virtual void attack(Skill skill, float attackTime) {
		isAttacking = true;
		StartCoroutine(playAttackAnimation(skill, attackTime));
	}
	public abstract IEnumerator playAttackAnimation(Skill skill, float attackTime);
	public virtual void OnStart() {}
	public virtual void OnUpdate() {}
	public virtual void OnFixedUpdate() {}

	public void AddBuffToStats (Buff buff) {
		this.buff.AddBuff (buff);
		this.stats.AddBuff (buff);
	}

	public void RemoveBuffFromStats (Buff buff) {
		this.stats.RemoveBuff (buff);
		this.buff.RemoveBuff (buff);
	}
}
