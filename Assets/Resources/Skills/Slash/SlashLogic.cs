using UnityEngine;
using System.Collections;

public class SlashLogic : MonoBehaviour {

	public PolygonCollider2D[] hitboxes;
	private PolygonCollider2D hitbox;
	public Sprite[] sprite;
	private int frame = 0;
	public float damage;
    public float attackSpeed;
	
	public string enemyTag;

	void Start() {
		hitbox = gameObject.AddComponent<PolygonCollider2D>();
		hitbox.isTrigger = true;
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/Slash/slash_" + Random.Range(0,3).ToString()), gameObject.transform.position);
		StartCoroutine("playAnimation", attackSpeed);
	}
	
	IEnumerator playAnimation(float duration) {
        print(duration);
        float endTime = Time.fixedTime + duration;
        float remainingTime = endTime - Time.fixedTime;
        while (remainingTime > 0) {
            int currentFrame = (int)(((duration - remainingTime) / duration) * sprite.Length);
			GetComponent<SpriteRenderer> ().sprite = sprite[currentFrame];
			hitbox.points = hitboxes[currentFrame].points;
			yield return new WaitForSeconds(0f);
            remainingTime = endTime - Time.fixedTime;
        }
		Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.tag == enemyTag) {
			collider.GetComponent<Mob>().hurt(damage);
		}
	}
}
