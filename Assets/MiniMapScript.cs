using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MiniMapScript : MonoBehaviour {

	public Player player;
	public GameObject background;
	public Image enemyDot;

	private List<Image> enemyList;

	// Use this for initialization
	void Start () {
		enemyList = new List<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");

		int difference = enemies.Length - enemyList.Count;
		if (difference > 0) {
			// add the dots
			for (int i = 0; i < difference; i++) {
				Image newDot = (Image) GameObject.Instantiate (enemyDot, new Vector3 (0, 0, 0), Quaternion.identity);
				newDot.transform.SetParent (background.transform, false);
				newDot.gameObject.SetActive (true);
				enemyList.Add (newDot);
			}
		} else if (difference < 0) {
			// remove the dots
			for (int i = 0; i < Mathf.Abs (difference); i++) {
				Image dot = enemyList [enemies.Length + i];
				enemyList.Remove (dot);
				GameObject.Destroy (dot.gameObject);
			}
		}

		// update the position to the dots
		for (int i = 0; i < enemies.Length; i++) {
			Vector3 positionOffset = enemies[i].transform.position - player.transform.position;
			enemyList[i].transform.localPosition = positionOffset * 15;
		}
	}

	private static float GetDistanceOf (Vector3 p1, Vector3 p2) {
		return Mathf.Sqrt (Mathf.Pow (p1.x - p2.x, 2) + Mathf.Pow (p1.y - p2.y, 2));
	}
}
