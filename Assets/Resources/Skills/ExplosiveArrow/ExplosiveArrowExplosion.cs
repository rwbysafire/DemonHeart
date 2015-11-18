using UnityEngine;
using System.Collections;

public class ExplosiveArrowExplosion : MonoBehaviour {
	
	private Sprite[] sprite;
	private string fileName = "Skills/ExplosiveArrow/fireExplosion";
	private int frame = 0;
	public float damage;
	
	public string enemyTag;

	void Start () {
		sprite = Resources.LoadAll<Sprite>(fileName);
		StartCoroutine("playAnimation", 0.02f);
	}
	
	IEnumerator playAnimation(float delay) {
		do {
			if(frame >= sprite.Length) {
				frame = 0;
			}
			GetComponent<SpriteRenderer> ().sprite = sprite[frame];
			if (frame == 11) {
				gameObject.GetComponent<CircleCollider2D>().enabled = true;
				AudioSource.PlayClipAtPoint(Resources.Load <AudioClip>("Skills/ExplosiveArrow/explosion"), gameObject.transform.position);
			} 
			else if (frame == 23)
				gameObject.GetComponent<CircleCollider2D>().enabled = false;
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
