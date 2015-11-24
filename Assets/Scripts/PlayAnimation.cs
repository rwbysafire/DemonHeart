using UnityEngine;
using System.Collections;

public class PlayAnimation : MonoBehaviour {

	public bool repeat = true, destroyOnFinish = false;
	public float delay = 0.05f;
	public string fileName;
	public Sprite[] sprite;
	private int frame = 0;

	void Start () {
		sprite = Resources.LoadAll<Sprite>(fileName);
		GetComponent<SpriteRenderer> ().sprite = sprite[frame];
		StartCoroutine("playAnimation", delay);
		PolygonCollider2D thing = gameObject.AddComponent<PolygonCollider2D>();
		thing.isTrigger = true;
	}

	IEnumerator playAnimation(float delay) {
		do {
			if(frame >= sprite.Length) {
				frame = 0;
			}
			GetComponent<SpriteRenderer> ().sprite = sprite[frame];
			frame++;
			yield return new WaitForSeconds(delay);
		} while (repeat || frame < sprite.Length);
		if (destroyOnFinish)
			Destroy(gameObject);
	}
}
