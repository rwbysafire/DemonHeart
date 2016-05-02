using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SpawnEnemies : MonoBehaviour {

    public int currentWave = 0;
	public TextAsset waveFile;

	private List<Wave> waveList;
	private bool isSpawning = false;
	private Vector3 ClosetSpawn;

	// Use this for initialization
	void Start () {
		// load the wave file
		waveList = new List<Wave>();
		JSONNode wavesData = JSON.Parse(waveFile.text);
		JSONArray wavesName = wavesData ["waves"].AsArray;
		for (int i = 0; i < wavesName.Count; i++) {
			JSONNode waveData = wavesData [wavesName [i]];
			int count = waveData ["count"].AsInt;
			JSONArray enemies = waveData ["enemies"].AsArray;
			GameObject[] enemiesGameObject = new GameObject[enemies.Count];
			for (int j = 0; j < enemiesGameObject.Length; j++) {
				enemiesGameObject [j] = Resources.Load<GameObject> (enemies[j]);
			}
			waveList.Add (new Wave(enemiesGameObject, count));
		}
		Debug.Log (waveList.Count.ToString () + " waves loaded");

		StartCoroutine("spawnEnemy", waveList[currentWave]);

    }
	
	// Update is called once per frame
	void Update () {
		// need to fix, this method is slow (?)
		GameObject[] enemies = GameObject.FindGameObjectsWithTag ("Enemy");
		if (enemies.Length == 0 && currentWave < waveList.Count - 1 && !isSpawning) {

            currentWave += 1;
            StartCoroutine("spawnEnemy", waveList[currentWave]);
			print ("Starting wave: " + currentWave.ToString ());
        }
	}
		
    IEnumerator spawnEnemy(Wave wave) {
		isSpawning = true;

		// wait some seconds before start
		WholeScreenTextScript.ShowText("Wave " + (currentWave + 1).ToString() + " is coming...");
		yield return new WaitForSeconds(2.5f);

		List<Vector3> SpawnSpots = new List<Vector3>();
		SpawnSpots.Add(new Vector3(0f,20f,0f));
		SpawnSpots.Add(new Vector3(0f,-20f,0f));
		SpawnSpots.Add(new Vector3(30f,0f,0f));
		SpawnSpots.Add(new Vector3(-30f,0f,0f));
		GameObject curPlayer = GameObject.Find ("Player");
		Vector3 PlayerPos = curPlayer.transform.position;
		ClosetSpawn = SpawnSpots [0];
		for (int i = 1; i < 4; i++) 
		{
			float temp = Vector3.Distance(PlayerPos, SpawnSpots[i]);
			if (temp < Vector3.Distance(PlayerPos, ClosetSpawn)) 
			{
				ClosetSpawn = SpawnSpots [i];
			}
		}

        while (wave.count > 0) 
		{
            GameObject enemy = Instantiate<GameObject>(wave.enemies[Random.Range(0, wave.enemies.Length)]);
            wave.count -= 1;
			enemy.transform.position = ClosetSpawn;
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