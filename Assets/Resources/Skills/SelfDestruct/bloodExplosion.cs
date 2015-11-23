using UnityEngine;
using System.Collections;

public class bloodExplosion : MonoBehaviour {

	private Sprite[] sprite;
	private string fileName = "Skills/SelfDestruct/bloodExplosion";
	private int frame = 0;
	public float maxHealth;
	public string enemyTag;
	
	void Start () {
		sprite = Resources.LoadAll<Sprite>(fileName);
		StartCoroutine("playAnimation", 0.02f);
	}
	
	IEnumerator playAnimation(float delay) {
		do {
			if(frame >= sprite.Length)
				frame = 0;
			GetComponent<SpriteRenderer> ().sprite = sprite[frame];
			frame++;
			yield return new WaitForSeconds(delay);
		} while (frame < sprite.Length);
		Destroy(gameObject);
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == enemyTag) {
			collider.GetComponent<Mob>().hurt(0.2f * maxHealth);
		}
	}
}
