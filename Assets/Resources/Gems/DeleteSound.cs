using UnityEngine;
using System.Collections;

public class DeleteSound : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Destroy(gameObject, GetComponent<AudioSource>().clip.length);
	}
}
