using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public int maxEnemies = 50;
	public GameObject spawn;

	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (1, maxEnemies) > GameObject.FindGameObjectsWithTag ("Enemy").GetLength (0)) {
			GameObject enemy = Instantiate (spawn) as GameObject;
			enemy.transform.position = new Vector3 (Random.Range (-transform.localScale.x/2, transform.localScale.x/2), Random.Range (-transform.localScale.y/2, transform.localScale.y/2)) + transform.position;
			enemy.transform.rotation = Quaternion.Euler (0f, 0f, Random.Range(0, 360));
		}
	}
}
