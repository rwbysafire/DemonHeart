using UnityEngine;
using System.Collections;

public class RighteousFire : MonoBehaviour {

	public Mob mob;

	private Sprite[] sprite;
	private int frame = 0;
	
	public string enemyTag;
    public float manaCost;

    void Start () {
		sprite = Resources.LoadAll<Sprite>("Skills/RighteousFire/FireAura");
		if (mob.gameObject.tag == "Player" || mob.gameObject.tag == "Ally")
			enemyTag = "Enemy"; 
		else
			enemyTag = "Player";
		StartCoroutine("playAnimation", 0.1f);
	}

	void Update () {
		if (!mob.useMana(manaCost * Time.deltaTime))
			Destroy(gameObject);
	}

	void FixedUpdate() {
		transform.RotateAround(transform.position, Vector3.forward, -1);
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
		return mob.stats.strength * 1;
	}
}
