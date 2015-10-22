using UnityEngine;
using System.Collections;

public class basic_projectile : MonoBehaviour
{
    public float speed;
    public float timer;
	// Use this for initialization
	void Start ()
    {

        
	}
	
	void Update ()
    {
        //Moves projectile forward
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        timer += Time.deltaTime;
        //Projectile disappears after 2 seconds
        if (timer >= 1)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
            collider.gameObject.GetComponent<Health>().hurt(10);
        
    }
}
