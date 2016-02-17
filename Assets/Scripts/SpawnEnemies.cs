using UnityEngine;
using System.Collections;


public class SpawnEnemies : MonoBehaviour {

	public CanvasGroup displayText;
    public int currentWave = 0;

	private Wave[] waves;
	private bool isSpawning = false;

	// Use this for initialization
	void Start () {
        waves = new Wave[] {
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") }, 5),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Kamikaze") }, 5),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") , Resources.Load<GameObject>("Kamikaze") }, 5),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") }, 5),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Kamikaze") }, 5),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") }, 5),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Kamikaze") }, 40),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") }, 40),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Kamikaze") }, 40),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") }, 40),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Kamikaze") }, 40),
        };
        StartCoroutine("spawnEnemy", waves[currentWave]);
    }
	
	// Update is called once per frame
	void Update () {
		// need to fix, this method is slow (?)
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (enemies.Length == 0 && currentWave < waves.Length - 1 && !isSpawning) {
            currentWave += 1;
            StartCoroutine("spawnEnemy", waves[currentWave]);
			print ("Starting wave: " + currentWave.ToString ());
        }
	}
		
    IEnumerator spawnEnemy(Wave wave) {
		isSpawning = true;

		// wait some seconds before start
		displayText.alpha = 1f;
		float secondsToWait = 2f;
		float step = 0.025f;
		while (displayText.alpha > 0) {
			displayText.alpha = Mathf.Clamp01 (displayText.alpha - step);
			yield return new WaitForSeconds(secondsToWait * step);
		}
		yield return new WaitForSeconds(0.5f);

        while (wave.count > 0) {
            GameObject enemy = Instantiate<GameObject>(wave.enemies[Random.Range(0, wave.enemies.Length)]);
            wave.count -= 1;
            enemy.transform.position = Vector3.zero;
            enemy.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
            yield return new WaitForSeconds(0.5f);
        }
		isSpawning = false;
    }
}

public class Wave
{
    public GameObject[] enemies;
    public int count;
    public Wave(GameObject[] enemies, int count) {
        this.enemies = enemies;
        this.count = count;
    }
}