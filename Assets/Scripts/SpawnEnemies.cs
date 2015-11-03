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
			enemy.transform.position = new Vector3 (Random.Range (-transform.localScale.x/4, transform.localScale.x/4), Random.Range (-transform.localScale.y/4, transform.localScale.y/4)) + transform.position;
		}
	}
}
