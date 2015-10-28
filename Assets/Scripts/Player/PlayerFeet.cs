using UnityEngine;
using System.Collections;

public class PlayerFeet : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		Vector2 direction = new Vector2 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		if (direction != Vector2.zero) {
			float rot_z = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (0f, 0f, rot_z - 90);
		}
	}
}
