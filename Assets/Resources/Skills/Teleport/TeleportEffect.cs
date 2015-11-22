using UnityEngine;
using System.Collections;

public class TeleportEffect : MonoBehaviour {

	float alpha = 0.5f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gameObject.GetComponent<LineRenderer>().SetColors(new Color(1,1,0,alpha-0.2f), new Color(1,1,0,alpha));
		alpha -= 0.02f;
		if (alpha <= 0)
			Destroy(gameObject);
	}
}
