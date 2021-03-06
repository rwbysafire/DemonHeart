﻿using UnityEngine;
using System.Collections;

public abstract class Mob : MonoBehaviour , Entity{
	
	public Stats stats = new Stats();
	public Buff buff = new Buff();
	private float stunTime = 0;
	private float canMove = 0;

	private float lasthit;
	public float lastHit{get{return lasthit;}}
	public bool isAttacking = false;
	public Skill[] skills = new Skill[6];

    public GameObject body;

    public string[] dropTable = { "Gems/ArmorGem", "Gems/SkillGem" };
    private int dropRate = 100;
    private int[] armourDrops = { 0, 1, 2, 3, 4, 5, 6 };
    private int[] skillDrops = { 0, 1, 2, 3, 4, 5, 6 };

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
	public virtual Vector3 getTargetLocation() {
        GameObject player = GameObject.FindWithTag("Player");
        float radius = GetComponent<CircleCollider2D>().radius * transform.localScale.x * 1.1f;
        //Debug.DrawLine(body.transform.position, body.transform.position + (player.transform.position - body.transform.position).normalized * 2, Color.red);
        foreach (RaycastHit2D lineCast in Physics2D.LinecastAll(body.transform.position + (player.transform.position - body.transform.position).normalized * radius, body.transform.position + (player.transform.position - body.transform.position).normalized * 2)) {
            if (lineCast.collider.CompareTag("Wall") || lineCast.collider.CompareTag("Enemy")) {
                //Debug.DrawLine(body.transform.position, (body.transform.position + body.transform.up * 2), Color.blue);
                foreach (RaycastHit2D directionCast in Physics2D.LinecastAll(body.transform.position + body.transform.up * radius, (body.transform.position + body.transform.up * 2))) {
                    if (directionCast.collider.CompareTag("Wall") || directionCast.collider.CompareTag("Enemy")) {
                        //Debug.DrawLine(body.transform.position, Physics2D.Raycast(body.transform.position + body.transform.TransformDirection(new Vector3(0.5f, 0.5f, 0)).normalized * radius, body.transform.TransformDirection(new Vector3(0.5f, 0.5f, 0))).point, Color.green);
                        //Debug.DrawLine(body.transform.position, Physics2D.Raycast(body.transform.position + body.transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0)).normalized * radius, body.transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0))).point, Color.green);
                        if (Physics2D.Raycast(body.transform.position + body.transform.TransformDirection(new Vector3(0.5f, 0.5f, 0)).normalized * radius, body.transform.TransformDirection(new Vector3(0.5f, 0.5f, 0))).distance > Physics2D.Raycast(body.transform.position + body.transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0)).normalized * radius, body.transform.TransformDirection(new Vector3(-0.5f, 0.5f, 0))).distance)
                            return body.transform.position + body.transform.TransformDirection(new Vector3(0.1f, 1, 0).normalized);
                        else
                            return body.transform.position + body.transform.TransformDirection(new Vector3(-0.1f, 1, 0).normalized);
                    }
                }
                return body.transform.position + body.transform.up;
            }
        }
        return player.transform.position;
    }

	void Start() {
		stats.tag = tag;
        OnStart();
		stats.health = stats.maxHealth;
		stats.mana = stats.maxMana;
	}

	void Update() {
		if (stats.health <= 0)
		{
			if (gameObject.tag != "Player") {
				Mob player = GameObject.Find ("Player").GetComponent<Mob> ();
				player.stats.exp += stats.exp;
				player.stats.CurExp += stats.exp;
				player.stats.killCount++;
			}
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
		if (stats.CurExp >= stats.threshold) {
			stats.CurExp -= stats.threshold;
			stats.level++;
			stats.threshold = (stats.level+1) * 200;
			stats.pts += Stats.ExtraPointsPerLevel;
		}
	}

	public virtual void OnDeath() { }

    public void setDropRate(int dropRate) {
        this.dropRate = dropRate;
    }

    public void setArmourDrops(int[] armourDrops) {
        this.armourDrops = armourDrops;
    }

    public void setSkillDrops(int[] skillDrops) {
        this.skillDrops = skillDrops;
    }

    void DropItem() {
        if (Random.Range(1, 101) <= dropRate) {
			GameObject Drop = (GameObject) Instantiate(
				Resources.Load<GameObject>(dropTable[Random.Range(0, dropTable.Length)]),
				this.gameObject.transform.position + new Vector3 (Random.Range (2f, 5f), Random.Range (2f, 5f), 0),
				Quaternion.identity);
            Drop.transform.position = feetTransform.position;
            Drop.GetComponent<DropItemScript>().armourDrops = armourDrops;
            Drop.GetComponent<DropItemScript>().skillDrops = skillDrops;
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
		if (buff != null) {
			this.buff.AddBuff (buff);
			this.stats.AddBuff (buff);
		}
	}

	public void RemoveBuffFromStats (Buff buff) {
		if (buff != null) {
			this.stats.RemoveBuff (buff);
			this.buff.RemoveBuff (buff);
		}
	}

    public static string getEnemyTag(string tag) {
        if (tag == "Player" || tag == "Ally")
            return "Enemy";
        else
            return "Player";
    }
}
