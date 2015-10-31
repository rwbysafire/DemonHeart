using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 7;
	
	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (direction != Vector2.zero) {
			transform.Translate (new Vector2(Mathf.Abs(direction.normalized.x) * direction.x, Mathf.Abs(direction.normalized.y) * direction.y) * speed * Time.deltaTime);
		}
	}
}
