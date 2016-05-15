using UnityEngine;
using System.Collections;

public class GroundFire : MonoBehaviour {

    private string enemyTag;
    private float damage;

    private Sprite[] sprite;

    public void init (string enemyTag, float damage, float duration) {
        this.enemyTag = enemyTag;
        this.damage = damage;
        StartCoroutine("destroyTimer", duration);
    }

    void Start () {
        sprite = Resources.LoadAll<Sprite>("Skills/RighteousFire/GroundFire");
        StartCoroutine("playAnimation", 0.05f);
    }

    void OnTriggerStay2D(Collider2D collider) {
        if (collider.tag == enemyTag) {
            collider.GetComponent<Mob>().hurt(damage * Time.deltaTime);
        }
    }

    IEnumerator playAnimation(float delay) {
        int frame = Random.Range(0, sprite.Length - 1);
        while (true) {
            if (frame >= sprite.Length) {
                frame = 0;
            }
            GetComponent<SpriteRenderer>().sprite = sprite[frame];
            frame++;
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator destroyTimer(float duration) {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        yield return new WaitForSeconds(duration - 0.4f);
        float amount = spriteRenderer.color.a / 4;
        while (spriteRenderer.color.a > 0) {
            spriteRenderer.color = new Color(1,1,1, spriteRenderer.color.a - amount);
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
