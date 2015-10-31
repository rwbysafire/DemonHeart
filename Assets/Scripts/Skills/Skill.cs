using UnityEngine;
using System.Collections;

public abstract class Skill  
{
	private GameObject gameObj;
	private GameObject player;
	private float cooldown;


	public Skill(GameObject gameObj) {
		player = GameObject.FindGameObjectWithTag("Player");
		this.gameObj = gameObj;
		cooldown = 0.0f; 
	}

	public GameObject getGameObject() {
		return gameObj; 
	}

	public GameObject getPlayer() {
		return player;
	}

	public bool useSkill() {
		if (cooldown <= Time.fixedTime) {
			skillLogic();
			cooldown = Time.fixedTime + getMaxCooldown(); 
			return true;
		}
		return false; 
	}

	public float remainingCooldown() {
		float tempTime = cooldown - Time.fixedTime;
		if (tempTime < 0)
			tempTime = 0;
		return tempTime;
	}

	public abstract string getName();
	public abstract float getMaxCooldown();
	public abstract void skillLogic(); 
}
