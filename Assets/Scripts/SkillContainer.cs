using UnityEngine;
using System.Collections;

public class SkillContainer : MonoBehaviour {

	/**
	 * SKILL CONTAINER !!IN PROGRESS!!
	 * Things that must be done are labeled with 'TODO'
	 * 
	 * Temporary skillstuffs:
	 * TODO DMG resistance buff for a short period
	 * TODO Speed buff for a short period
	 * TODO Rate of fire increase for a short period
	 */

	//NOTE: "gameObject" is the parent of the script (aka the player), and "GameObject" refers to the actual object type.
	public bool enableSkill_Speed;
	public bool enableSkill_DamageResist;
	public bool enableSkill_FireRate;

	private Movement playerMove;

	void Start() {
		playerMove = (Movement) gameObject.GetComponent(typeof(Movement));
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			StartCoroutine(triggerSkill("Skill 1"));
		}
	}
	private IEnumerator triggerSkill(string skillName) {
		float origSpd;
		if (skillName == "Skill 1" || skillName == "Skill 2" || skillName == "Skill 3") {
			origSpd = playerMove.speed;
			playerMove.speed = playerMove.speed * 2;
			yield return new WaitForSeconds(5);
			playerMove.speed = origSpd;
		}
	}
}
