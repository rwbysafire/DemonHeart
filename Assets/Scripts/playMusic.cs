using UnityEngine;
using System.Collections;

public class playMusic : MonoBehaviour {

	void Update () {
		if (!gameObject.GetComponent<AudioSource>().isPlaying) {
			gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Battle Theme " + Random.Range(1,3).ToString());
			gameObject.GetComponent<AudioSource>().Play();
		}
	}
}
