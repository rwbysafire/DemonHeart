using UnityEngine;
using System.Collections;

public class Cameras : MonoBehaviour {

	// Script will make the camera follow the player except for rotation
    // May need changes depending on how we decide the camera should work
	void Update ()
    {
        transform.position = new Vector3 (GameObject.FindWithTag("Player").transform.position.x, GameObject.FindWithTag("Player").transform.position.y,-10);
	}
}
