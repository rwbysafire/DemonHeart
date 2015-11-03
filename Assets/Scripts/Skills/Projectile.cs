using UnityEngine;
using System.Collections;

public abstract class Projectile {
	
	public GameObject gameObject, origin;
	public Stats stats;
	public float speed, damage, duration, pierceChance, stunTime, chainTimes, turnSpeed;
	public bool isHoming, isForking;
	public int lastHit;
	float timer;
	public string tag;
	public string enemyTag;
	
	
	public Projectile(GameObject gameObject, GameObject origin, Stats stats) {
		this.gameObject = gameObject;
		this.origin = origin;
		this.stats = stats;
		tag = origin.tag;
		if (tag == "Player" || tag == "Ally")
			enemyTag = "Enemy"; 
		else
			enemyTag = "Player";
		//speed = getSpeed ();
		//damage = getDamage ();
		duration = getDuration ();
		//pierceChance = getPierceChance ();
		//stunTime = getStunTime ();
		//turnSpeed = getTurnSpeed ();
		//isHoming = getHoming ();
		isForking = getForking ();
		chainTimes = getChaining ();
		timer = Time.fixedTime + duration;
	}

	public void setGameObject(GameObject gameObject) {
		this.gameObject = gameObject;
	}

	public void projectileLogic() {
		//Moves projectile forward
		gameObject.transform.Translate(Vector3.up * getSpeed() * Time.deltaTime);
		//checks if he projectile is homing
		if (getHoming())
			homing (getTurnSpeed());
		//Projectile disappears after a certain amount of seconds
		if (timer <= Time.fixedTime)
			Object.Destroy(gameObject);
	}

	public void collisionLogic(Collider2D collider) {
		if (collider.tag != tag && collider.tag != "Projectile" && collider.tag != "EditorOnly" && collider.gameObject.GetInstanceID() != lastHit) {
			lastHit = collider.gameObject.GetInstanceID();
			OnHit();
			collider.gameObject.GetComponent<Health>().hurt (getDamage());
			//check if projectile will stun
			if(getStunTime() > 0)
				collider.gameObject.GetComponent<EnemyAI>().addStunTime(getStunTime());
			//check if projectile will pierce
			if (getPierceChance() < Random.Range(1, 100)) {
				//check if projectile will fork
				if (isForking) {
					fork();
					Object.Destroy(gameObject);
				} else if (chainTimes > 0) {
					chainTimes -= 1;
					chain();
					timer = Time.fixedTime + duration;
				} else {
					OnExplode();
					Object.Destroy(gameObject);
				}
			}
		}
	}

	void chain() {
		GameObject enemy = FindClosestEnemy();
		if (enemy != null) {
			Vector3 diff = enemy.transform.position - gameObject.transform.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
			gameObject.transform.rotation = Quaternion.Euler(0f, 0f, rot_z);
		}
	}

	void fork() {
		cloneProjectile (45);
		cloneProjectile (-45);
	}

	void homing(float turnSpeed) {
		GameObject enemy = FindClosestEnemy();
		if (enemy != null) {
			Vector3 diff = enemy.transform.position - gameObject.transform.position;
			diff.Normalize();
			float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg - 90;
			var angle = Quaternion.Angle(Quaternion.Euler(0, 0, rot_z), gameObject.transform.rotation);
			gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, Quaternion.Euler(0, 0, rot_z), turnSpeed * Time.deltaTime / angle);
		}
	}

	void cloneProjectile(float rotation = 0) {
		GameObject clonedProjectile = MonoBehaviour.Instantiate(gameObject) as GameObject;
		Projectile projectile = this.MemberwiseClone() as Projectile;
		projectile.isForking = false;
		projectile.timer = Time.fixedTime + duration;
		clonedProjectile.GetComponent<basic_projectile> ().setProjectile (projectile);
		clonedProjectile.transform.RotateAround (clonedProjectile.transform.position, Vector3.forward, rotation);
		projectile.setGameObject (clonedProjectile);
	}

	GameObject FindClosestEnemy() {
		GameObject[] gos = GameObject.FindGameObjectsWithTag(enemyTag);
		GameObject closest = null;
		float distance = 8;
		Vector3 position = gameObject.transform.position;
		foreach (GameObject go in gos) {
			float curDistance = Vector3.Distance(go.transform.position, position);
			if (curDistance < distance && go.GetInstanceID() != lastHit) {
				closest = go;
				distance = curDistance;
			}
		}
		return closest;
	}

	public abstract float getSpeed();
	public abstract float getDamage();
	public virtual float getDuration() {
		return 1;
	}
	public virtual float getPierceChance() {
		return 0;
	}
	public virtual float getStunTime() {
		return 0;
	}
	public virtual int getChaining() {
		return 0;
	}
	public virtual float getTurnSpeed() {
		return 100;
	}
	public virtual bool getHoming() {
		return false;
	}
	public virtual bool getForking() {
		return false;
	}
	public virtual void OnHit(){}
	public virtual void OnExplode(){}
}
