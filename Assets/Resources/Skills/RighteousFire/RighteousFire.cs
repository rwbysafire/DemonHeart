using UnityEngine;
using System.Collections;

public class RighteousFire : MonoBehaviour {

	public Mob mob;

	private Sprite[] sprite;
	private int frame = 0;
	
	public string enemyTag;
    public float manaCost;
    public GameObject groundFire;

    void Start () {
		sprite = Resources.LoadAll<Sprite>("Skills/RighteousFire/FireAura");
		if (mob.gameObject.tag == "Player" || mob.gameObject.tag == "Ally")
			enemyTag = "Enemy"; 
		else
			enemyTag = "Player";
		StartCoroutine("playAnimation", 0.1f);
	}

	void Update () {
        if (Time.timeScale != 0) {
            if (!mob.useMana(manaCost * Time.deltaTime))
                Destroy(gameObject);
            GameObject fire = Instantiate<GameObject>(groundFire);
            float radius = GetComponent<CircleCollider2D>().radius * 0.75f;
            fire.transform.position = transform.position + new Vector3(Random.Range(-radius, radius), Random.Range(-radius, radius));
            fire.GetComponent<GroundFire>().init(enemyTag, getDamage(), Random.Range(0.6f, 1.4f));
        }
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
