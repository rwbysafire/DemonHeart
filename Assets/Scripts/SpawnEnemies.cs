using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

	public int maxEnemies = 50;
    public Wave[] waves;
    public int currentWave = 0;

	// Use this for initialization
	void Start () {
        waves = new Wave[] {
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") }, 10),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Kamikaze") }, 20),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") , Resources.Load<GameObject>("Kamikaze") }, 10),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") }, 40),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Kamikaze") }, 40),
            new Wave(new GameObject[] { Resources.Load<GameObject>("Zombie") }, 40),
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
        if (waves[currentWave].count == 0 && currentWave < waves.Length - 1) {
            currentWave += 1;
            StartCoroutine("spawnEnemy", waves[currentWave]);
            print("Starting next wave");
        }
	}

    IEnumerator spawnEnemy(Wave wave) {
        while (wave.count > 0) {
            GameObject enemy = Instantiate<GameObject>(wave.enemies[Random.Range(0, wave.enemies.Length)]);
            wave.count -= 1;
            enemy.transform.position = Vector3.zero;
            enemy.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
            yield return new WaitForSeconds(0.5f);
        }
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