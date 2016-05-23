using UnityEngine;
using System.Collections;

public class Fade : MonoBehaviour {

    public float lightSpeed = 0.1f;
    public float spriteSpeed = 0.03f;
    float alpha;
    Color c;

    // Use this for initialization
    void Start () {
        c = gameObject.GetComponent<SpriteRenderer>().color;
        alpha = c.a;
    }

    void Update() {
        c.a = alpha;
        gameObject.GetComponent<SpriteRenderer>().color = c;
        foreach (Transform child in transform) {
            child.GetComponent<Light>().intensity -= lightSpeed;
        }
        alpha -= spriteSpeed;
        if (alpha <= 0)
            Destroy(gameObject);
    }
}
