using UnityEngine;
using System.Collections;

public class basic_projectile : MonoBehaviour
{
    public float speed = 10, damage = 0, pierceChance = 0, duration = 1; 
	public float stunTime = 0;
	public bool homing = false;
    float timer;
	// Use this for initialization
	void Start ()
    {   // how long the projectile will last
		timer = Time.fixedTime + duration;
	}
	
	void Update ()
    {
        //Moves projectile forward
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        //Projectile disappears after a certain amount of seconds
		if (timer <= Time.fixedTime)
            Destroy(gameObject);

		if (homing) {
			GameObject enemy = FindClosestEnemy();
			if (enemy != null) {
				Vector3 diff = enemy.transform.position - transform.position;
				diff.Normalize();
				float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
				var angle = Quaternion.Angle(Quaternion.Euler(0, 0, rot_z), transform.rotation);
				transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, rot_z), 100 * Time.deltaTime / angle);
			}
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider)
    {
		if (collider.CompareTag ("Enemy")) {
			collider.gameObject.GetComponent<Health> ().hurt (damage);

			if(stunTime > 0)
				collider.gameObject.GetComponent<EnemyAI>().addStunTime(stunTime);

			//check if projectile will pierce
			if (pierceChance < Random.Range(1, 100))
				Destroy(gameObject);
		}
    }

	GameObject FindClosestEnemy() {
		GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = 8;
		Vector3 position = transform.position;
		foreach (GameObject go in gos) {
			float curDistance = Vector3.Distance(go.transform.position, position);
			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

}
