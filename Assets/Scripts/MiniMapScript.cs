using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MiniMapScript : MonoBehaviour {

	public Player player;
	public RectTransform background;
	public Image enemyDot;
	public Image outScreenEnemyDot;

	private List<Image> enemyList;
	private List<Image> enemyOutScreenList;
	private float width, height;

	// Use this for initialization
	void Start () {
		enemyList = new List<Image> ();
		enemyOutScreenList = new List<Image> ();
		width = background.rect.width;
		height = background.rect.height;
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

				Image arrow = (Image) GameObject.Instantiate (outScreenEnemyDot, new Vector3 (0, 0, 0), Quaternion.identity);
				arrow.transform.SetParent (background.transform, false);
				arrow.gameObject.SetActive (false);
				enemyOutScreenList.Add (arrow);
			}
		} else if (difference < 0) {
			// remove the dots
			for (int i = 0; i < Mathf.Abs (difference); i++) {
				Image dot = enemyList [enemyList.Count - 1];
				enemyList.Remove (dot);
				GameObject.Destroy (dot.gameObject);

				Image arrow = enemyOutScreenList [enemyOutScreenList.Count - 1];
				enemyOutScreenList.Remove (arrow);
				GameObject.Destroy (arrow.gameObject);
			}
		}

		// update the position to the dots
		for (int i = 0; i < enemies.Length; i++) {
			Vector3 positionOffset = enemies[i].transform.position - player.transform.position;
			enemyList[i].transform.localPosition = positionOffset * 15;

			// show arrows for enemies out of the map
			if (enemyList [i].transform.localPosition.x > width / 2) {
				enemyList [i].gameObject.SetActive (false);
				enemyOutScreenList [i].gameObject.SetActive (true);

				enemyOutScreenList [i].transform.localRotation = Quaternion.Euler (0, 0, 90);
				enemyOutScreenList [i].transform.localPosition = new Vector3 (
					width / 2 - 10,
					Mathf.Clamp(
						enemyList [i].transform.localPosition.y,
						height / -2 + 15,
						height / 2 - 15
					),
					0);
			} else if (enemyList [i].transform.localPosition.y > height / 2) {
				enemyList [i].gameObject.SetActive (false);
				enemyOutScreenList [i].gameObject.SetActive (true);

				enemyOutScreenList [i].transform.localRotation = Quaternion.Euler (0, 0, 180);
				enemyOutScreenList [i].transform.localPosition = new Vector3 (
					Mathf.Clamp(
						enemyList [i].transform.localPosition.x,
						width / -2 + 15,
						width / 2 - 15
					),
					height / 2 - 10,
					0);
			} else if (enemyList [i].transform.localPosition.x < width / -2) {
				enemyList [i].gameObject.SetActive (false);
				enemyOutScreenList [i].gameObject.SetActive (true);

				enemyOutScreenList [i].transform.localRotation = Quaternion.Euler (0, 0, 270);
				enemyOutScreenList [i].transform.localPosition = new Vector3 (
					width / -2 + 10,
					Mathf.Clamp(
						enemyList [i].transform.localPosition.y,
						height / -2 + 15,
						height / 2 - 15
					),
					0);
			} else if (enemyList [i].transform.localPosition.y < height / -2) {
				enemyList [i].gameObject.SetActive (false);
				enemyOutScreenList [i].gameObject.SetActive (true);

				enemyOutScreenList [i].transform.localRotation = Quaternion.Euler (0, 0, 0);
				enemyOutScreenList [i].transform.localPosition = new Vector3 (
					Mathf.Clamp(
						enemyList [i].transform.localPosition.x,
						width / -2 + 15,
						width / 2 - 15
					),
					height / -2 + 10,
					0);
			} else {
				enemyList [i].gameObject.SetActive (true);
				enemyOutScreenList [i].gameObject.SetActive (false);
			}
		}
	}

	private static float GetDistanceOf (Vector3 p1, Vector3 p2) {
		return Mathf.Sqrt (Mathf.Pow (p1.x - p2.x, 2) + Mathf.Pow (p1.y - p2.y, 2));
	}
}
