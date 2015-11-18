using UnityEngine;
using System.Collections;

public class RighteousFire : MonoBehaviour {

	public Mob mob;

	private Sprite[] sprite;
	private int frame = 0;
	
	public string enemyTag;

	void Start () {
		sprite = Resources.LoadAll<Sprite>("Skills/RighteousFire/righteousFire");
		if (mob.gameObject.tag == "Player" || mob.gameObject.tag == "Ally")
			enemyTag = "Enemy"; 
		else
			enemyTag = "Player";
		StartCoroutine("playAnimation", 0.1f);
	}

	void Update () {
		if (mob.stats.health > 1) {
			mob.hurt(mob.stats.maxHealth * 0.1f * Time.deltaTime);
			if (mob.stats.health < 1)
				mob.stats.health = 1;
		}
		if (mob.stats.health <= 1)
			Destroy(gameObject);
	}

	IEnumerator playAnimation(float delay) {
		while(true) {
			if(frame >= sprite.Length) {
				frame = 0;
			}
			GetComponent<SpriteRenderer> ().sprite = sprite[frame];
			frame++;
			yield return new WaitForSeconds(delay);
		}
	}

	void OnTriggerStay2D(Collider2D collider) {
		if (collider.tag == enemyTag) {
			collider.GetComponent<Mob>().hurt(getDamage() * Time.deltaTime);
		}
	}

	public float getDamage() {
		return mob.stats.maxHealth * 1;
	}
}
