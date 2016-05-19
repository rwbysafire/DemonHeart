using UnityEngine;
using System.Collections;

public class ChainLightning : MonoBehaviour {
	
	public Stats stats;
	float alpha = 1f;
	ArrayList lastHit = new ArrayList();
	public int chainTimes;

	public GameObject enemy;
	string enemyTag;
	public Vector3 target;
	public int maxDistance;

	// Use this for initialization
	void Start () {
		if (stats.tag == "Player" || stats.tag == "Ally")
			enemyTag = "Enemy"; 
		else
			enemyTag = "Player";
        LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
		lineRenderer.material = Resources.Load<Material>("Skills/ChainLightning/lightningMaterial");
		lineRenderer.sortingOrder = 4;
		lineRenderer.SetPosition(0, transform.position);
		lineRenderer.SetPosition(1, target);
		displayFlash(target);
		if (enemy != null) {
			enemy.GetComponent<Mob>().hurt(20 + (1 * stats.abilityPower));
			if (chainTimes > 0) {
				lastHit.Insert(0, enemy.GetInstanceID());
				if (lastHit.Count > 2)
					lastHit.RemoveAt(2);
				StartCoroutine("castChainLightning");
			}
		}
		AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("Skills/ChainLightning/spark_" + Random.Range(0,2).ToString()), gameObject.transform.position);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		gameObject.GetComponent<LineRenderer>().SetColors(new Color(1,1,1,alpha), new Color(1,1,1,alpha));
		gameObject.GetComponent<LineRenderer>().SetWidth(alpha/1.5f+0.25f, alpha/1.5f+0.25f);
		alpha -= 0.15f;
		if (alpha <= 0)
			Destroy(gameObject);
	
	}

	IEnumerator castChainLightning(){
		yield return new WaitForSeconds(0.05f);
		GameObject nextEnemy = FindRandomEnemy(target, enemyTag, lastHit);
		if (nextEnemy != null) {
			GameObject chainLightning = GameObject.Instantiate(Resources.Load<GameObject>("Skills/ChainLightning/Chainlightning"));
			chainLightning.transform.position = target;
			chainLightning.GetComponent<ChainLightning>().stats = stats;
			chainLightning.GetComponent<ChainLightning>().chainTimes = chainTimes - 1;
			chainLightning.GetComponent<ChainLightning>().maxDistance = maxDistance;
			chainLightning.GetComponent<ChainLightning>().lastHit = lastHit;
			chainLightning.GetComponent<ChainLightning>().enemy = nextEnemy;
			chainLightning.GetComponent<ChainLightning>().target = nextEnemy.GetComponent<Mob>().feetTransform.position;
		}
	}

	GameObject FindRandomEnemy(Vector3 position, string tag, ArrayList ignoreID) {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
		ArrayList inRange = new ArrayList();
		foreach(GameObject go in gos) {
			if (Vector3.Distance(go.transform.position, position) < maxDistance && !ignoreID.Contains(go.GetInstanceID())) {
				bool add = true;
				foreach (RaycastHit2D lineCast in Physics2D.LinecastAll(position, go.transform.position)) {
					if (lineCast.collider.CompareTag("Wall")) {
						add = false;
						break;
					}
				}
				if (add)
					inRange.Add(go);
			}
		}
		if (inRange.Count > 0)
			return (GameObject)inRange.ToArray()[Random.Range(0, inRange.Count)];
		else
			return null;
	}

	
	void displayFlash(Vector3 position)
	{
		GameObject flash = new GameObject ();
		flash.transform.position = position;
		flash.AddComponent<SpriteRenderer> ();
		flash.GetComponent<SpriteRenderer> ().sprite = Resources.Load<Sprite> ("Skills/Teleport/flash");
		flash.GetComponent<SpriteRenderer> ().color = new Color(0.25f,0.25f,1,0.8f);
		flash.transform.localScale = new Vector3(0.5f, 0.5f, 1);
		flash.GetComponent<SpriteRenderer> ().sortingOrder = 4;
		GameObject.Destroy (flash, 0.1f);
	}
}
