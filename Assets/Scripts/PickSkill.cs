using UnityEngine;
using System.Collections;

public class PickSkill : MonoBehaviour {

	int index;
	Skill[] skills;

	void Start() {
		skills = new Skill[12]{new SkillBasicAttack(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillChainLightning(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillCombatRoll(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillExplosiveArrow(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillPowershot(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillRighteousFire(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillScattershot(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillSelfDestruct(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillSlash(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillStunArrow(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillTeleport(GameObject.Find("Player").GetComponent<Mob>()),
			new SkillVolley(GameObject.Find("Player").GetComponent<Mob>())};
		
		gameObject.SetActive(false);
	}

	void Update () {
		if (Input.GetKeyUp(KeyCode.V))
			gameObject.SetActive(false);
	}

	public void setIndex(int i) {
		index = i;
	}

	public void replaceSkill(int i) {
		if (GameObject.Find("Player")) {
			GameObject.Find("Player").GetComponent<Mob>().replaceSkill(index, skills[i]);
		}
	}
}
