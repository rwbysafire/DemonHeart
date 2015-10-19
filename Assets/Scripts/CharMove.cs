using UnityEngine;
using System.Collections;

public class CharMove : MonoBehaviour {

	public int speed, speedV, maxSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Quaternion rotation = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);
		transform.eulerAngles = new Vector3(0,0, rotation.eulerAngles.z);

		if (Input.GetKeyDown (KeyCode.LeftShift))
			maxSpeed = maxSpeed * 2;
		else if (Input.GetKeyUp (KeyCode.LeftShift))
			maxSpeed = maxSpeed / 2;

		if (Input.GetKey (KeyCode.D)) {
			speed += 1;
			if (speed > maxSpeed)
				speed = maxSpeed;
		} else if (Input.GetKey (KeyCode.A)) {
			speed -= 1;
			if (speed < -maxSpeed)
				speed = -maxSpeed;
		} else {
			if (speed > 0)
				speed -= 1;
			else if (speed < 0)
				speed += 1;
		}

		if (Input.GetKey (KeyCode.W)) {
			speedV += 1;
			if (speedV > maxSpeed)
				speedV = maxSpeed;
		} else if (Input.GetKey (KeyCode.S)) {
			speedV -= 1;
			if (speedV < -maxSpeed)
				speedV = -maxSpeed;
		} else {
			if (speedV > 0)
				speedV -= 1;
			else if (speedV < 0)
				speedV += 1;
		}
		transform.Translate (Vector3.right * speed * Time.deltaTime, Space.World);
		transform.Translate (Vector3.up * speedV * Time.deltaTime, Space.World);
	}
}
