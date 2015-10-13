using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed;

	void FixedUpdate()
	{
		Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		Quaternion rotation = Quaternion.LookRotation(transform.position - mousePosition, 
		                                              Vector3.forward);
		transform.rotation = rotation;
		transform.eulerAngles = new Vector3(0,0, transform.eulerAngles.z);
		GetComponent<Rigidbody2D>().angularVelocity = 0;

		if (Input.GetMouseButton(0))
			GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed);
	}

}
