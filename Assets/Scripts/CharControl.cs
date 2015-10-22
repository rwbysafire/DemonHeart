using UnityEngine;
using System.Collections;

public class CharControl : MonoBehaviour{

    public float speed;

    void Start()
    {

    }

    void Update()

    {   //Rotate player based on mouse position
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        

        //WASD Movement in relation to the world
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * Time.deltaTime * speed, Space.World);
        transform.Translate(Vector3.up * Input.GetAxis("Vertical") * Time.deltaTime * speed, Space.World);

    }
	
}
