using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	Vector2 playerPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag ("Player")) {
			var player = GameObject.FindWithTag ("Player");
			playerPosition = new Vector2 (player.transform.position.x, player.transform.position.y);
		}
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Vector3 camPosision = new Vector3 ((playerPosition.x * 6 + mousePosition.x) / 7, (playerPosition.y * 6 + mousePosition.y) / 7, transform.position.z);
		transform.position = Vector3.MoveTowards(transform.position, camPosision, Mathf.Pow(Mathf.Pow(camPosision.x - transform.position.x, 2) + Mathf.Pow(camPosision.y - transform.position.y, 2), 0.75f) * Time.deltaTime * 5);
	}
}
