using UnityEngine;
using System.Collections;

public class basic_projectile : MonoBehaviour, Entity
{

	public Projectile projectile;

    public Transform headTransform {
        get {
            return transform;
        }
    }

    public Transform feetTransform {
        get {
            return transform;
        }
    }

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

    public Vector3 getTargetLocation() {
        return transform.forward;
    }

    public string getEnemyTag() {
        return projectile.enemyTag;
    }
}
