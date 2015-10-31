using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public int maxEnemies = 30;
	Vector3 position;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (1, maxEnemies) > GameObject.FindGameObjectsWithTag ("Enemy").GetLength (0)) {
			GameObject enemy = Instantiate (Resources.Load ("Enemy")) as GameObject;
			do {
				position = new Vector3 (Random.Range (-10, 10), Random.Range (-10, 10), 0).normalized * Random.Range (10, 15);
			} while (position == Vector3.zero);
			enemy.transform.position = position + transform.position;
		}
	}
}
