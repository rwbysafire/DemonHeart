using UnityEngine;
using System.Collections;

public class BuildingFade : MonoBehaviour {

	private GameObject player;
	private Sprite sprite;
	private float alpha;
	private SpriteRenderer spriteComponent;

	public bool reverseXTrigger = false;
	public bool reverseYTrigger = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		spriteComponent = (SpriteRenderer) gameObject.GetComponent(typeof(SpriteRenderer));
		sprite = spriteComponent.sprite;
		alpha = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		var xx = gameObject.transform.position.x;
		var yy = gameObject.transform.position.y;

		if (reverseXTrigger && !reverseYTrigger) {
			if (player.transform.position.x >= xx &&
			    player.transform.position.y >= yy) {
				if (alpha > 0f) {
					alpha -= 0.05f;
				} else {
					alpha = 0f;
				}
			} else {
				if (alpha < 1f) {
					alpha += 0.05f;
				} else {
					alpha = 1f;
				}
			}
		} else if (!reverseXTrigger && reverseYTrigger) {
			if (player.transform.position.x <= xx &&
			    player.transform.position.y <= yy) {
				if (alpha > 0f) {
					alpha -= 0.05f;
				} else {
					alpha = 0f;
				}
			} else {
				if (alpha < 1f) {
					alpha += 0.05f;
				} else {
					alpha = 1f;
				}
			}
		} else if (reverseXTrigger && reverseYTrigger) {
			if (player.transform.position.x >= xx &&
			    player.transform.position.y <= yy) {
				if (alpha > 0f) {
					alpha -= 0.05f;
				} else {
					alpha = 0f;
				}
			} else {
				if (alpha < 1f) {
					alpha += 0.05f;
				} else {
					alpha = 1f;
				}
			}
		} else {
			if (player.transform.position.x <= xx &&
			    player.transform.position.y >= yy) {
				if (alpha > 0f) {
					alpha -= 0.05f;
				} else {
					alpha = 0f;
				}
			} else {
				if (alpha < 1f) {
				alpha += 0.05f;
				} else {
					alpha = 1f;
				}
			}
		}
		spriteComponent.color = new Color(1f, 1f, 1f, alpha);
		//Debug.Log ("(" + player.transform.position.x.ToString() + " " + player.transform.position.y.ToString()
		//           + ") (" + gameObject.transform.position.x.ToString () + " " + gameObject.transform.position.y.ToString()
		//           + ")");
	}
}
