using UnityEngine;
using System.Collections;

public abstract class Mob : MonoBehaviour{
	
	public Stats stats = new Stats();
	private float stunTime = 0;

	public Skill[] skills = new Skill[6];

	public void replaceSkill(int skillNum, Skill skill) {
		skills[skillNum] = skill;
	}

	public virtual Quaternion rotation {
		get {
			return gameObject.transform.rotation;
		}
		set {
			gameObject.transform.rotation = value;
		}
	}

	public virtual Vector3 position {
		get {
			return gameObject.transform.position;
		}
		set {
			gameObject.transform.position = value;
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
		return stats.health <= 0;
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
			Destroy(gameObject);
		if(isStunned())
			return;
		OnUpdate();
		if (stats.health < stats.maxHealth)
			stats.health += stats.healthRegen * Time.deltaTime;
		else if (stats.health > stats.maxHealth)
			stats.health = stats.maxHealth;
	}
	void FixedUpdate() {
		if(isStunned())
			return;
		OnFixedUpdate();
	}
	public virtual void OnStart() {}
	public virtual void OnUpdate() {}
	public virtual void OnFixedUpdate() {}
}
