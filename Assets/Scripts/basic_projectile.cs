using UnityEngine;
using System.Collections;

public class basic_projectile : MonoBehaviour
{

	public Projectile projectile;

	void Start() {
		
	}
	
	void Update() {
		projectile.projectileLogic ();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		projectile.collisionLogic (collider);
    }

	public void setProjectile(Projectile projectile) {
		this.projectile = projectile;
	}

	public void StartChildCoroutine(IEnumerator coroutineMethod)
	{
		StartCoroutine(coroutineMethod);
	}
}
