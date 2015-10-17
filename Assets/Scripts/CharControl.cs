using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour{
    public float speed;
    public Rigidbody2D move;
    void Start()
    {
        move = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()

    {   //Rotate player based on mouse position
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        transform.rotation = rot;
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);

        //WASD Movement in relation to the world
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed, Space.World);
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.World);

    }
	
}
