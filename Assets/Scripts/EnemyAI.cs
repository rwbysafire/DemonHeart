using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public int speed, followDistance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindWithTag ("Player")) {
			var player = GameObject.FindWithTag ("Player");
			Vector3 playerPosition = new Vector3 (player.transform.position.x, player.transform.position.y, player.transform.position.z - 10);
			if (Mathf.Sqrt(Mathf.Pow(playerPosition.x - transform.position.x, 2) + Mathf.Pow(playerPosition.y - transform.position.y, 2)) <= followDistance) {
				Vector3 diff = player.transform.position - transform.position;
                diff.Normalize();
                float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

                transform.Translate (Vector3.up * speed * Time.deltaTime);
			}
		}
	}
}
