using UnityEngine;
using System.Collections;

public class FireBoltExplosion : MonoBehaviour {

    private Sprite[] sprite;
    private string fileName = "Skills/FireBolt/fireBoltExplosion";
    private int frame = 0;
    public float damage;

    public string enemyTag;

    void Start() {
        sprite = Resources.LoadAll<Sprite>(fileName);
        StartCoroutine("playAnimation", 0.05f);
    }

    IEnumerator playAnimation(float delay) {
        do {
            if (frame >= sprite.Length) {
                frame = 0;
            }
            GetComponent<SpriteRenderer>().sprite = sprite[frame];
            if (frame == 2)
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
