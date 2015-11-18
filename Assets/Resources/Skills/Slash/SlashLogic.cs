using UnityEngine;
using System.Collections;

public class SlashLogic : MonoBehaviour {

	public PolygonCollider2D[] hitboxes;
	private PolygonCollider2D hitbox;
	public Sprite[] sprite;
	private int frame = 0;
	public float damage;
	
	public string enemyTag;

	void Start() {
		hitbox = gameObject.AddComponent<PolygonCollider2D>();
		hitbox.isTrigger = true;
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/Slash/slash_" + Random.Range(0,3).ToString()), gameObject.transform.position);
		StartCoroutine("playAnimation", 0.01f);
	}
	
	IEnumerator playAnimation(float delay) {
		do {
			if(frame >= sprite.Length) {
				frame = 0;
			}
			GetComponent<SpriteRenderer> ().sprite = sprite[frame];
			hitbox.points = hitboxes[frame].points;
			frame++;
			yield return new WaitForSeconds(delay);
		} while (frame < sprite.Length);
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == enemyTag) {
			collider.GetComponent<Mob>().hurt(damage);
		}
	}
}
