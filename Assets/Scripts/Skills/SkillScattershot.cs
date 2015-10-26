using UnityEngine;
using System.Collections;

public class SkillScattershot : Skill
{
	public SkillScattershot(GameObject gameObject) : base(gameObject) {} 

	public override string getName ()
	{
		return "Scattershot";
	}

	public override float getMaxCooldown ()
	{
		return 2f;
	}

	public override void skillLogic ()
	{
		GameObject cube = new GameObject ();
		cube.transform.rotation = this.getGameObject ().transform.rotation;
		cube.transform.position = this.getGameObject ().transform.position;
		cube.transform.localScale = new Vector3 (10, 10, 10);
		cube.transform.Translate (Vector3.up);
		cube.AddComponent<ScattershotProjectile1>();
	}
}

class ScattershotProjectile1 : MonoBehaviour {

	void Start()
	{
		Destroy (this.gameObject.GetComponent<SphereCollider> ());
		this.gameObject.AddComponent<BoxCollider2D> ();
	}

	void Update()
	{
		//Destroy (gameObject);
	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log ("Collision");
		Destroy (collider.gameObject);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		Debug.Log ("Collision");
		if (collider.CompareTag ("Enemy"))
		{
			collider.gameObject.GetComponent<Health> ().hurt (100);
		}
	}
}
