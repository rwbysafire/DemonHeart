using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SpawnEnemies : MonoBehaviour {

    public int currentWave = 0;
	public TextAsset waveFile;
	public GameObject mustKillEnemies, normalEnemies;

	private List<Wave> waveList;
	private bool isSpawning = false;
	private List<Vector3> ClosestSpawn;
	private int mark = 0;
    private float zPosition = 0;

    // Use this for initialization
    void Start () {
        // load the wave file
        zPosition = 0;
        waveList = new List<Wave>();
		JSONNode wavesData = JSON.Parse(waveFile.text);
		JSONArray wavesName = wavesData ["waves"].AsArray;
		for (int i = 0; i < wavesName.Count; i++) {
			JSONNode waveData = wavesData [wavesName [i]];
			int count = waveData ["count"].AsInt;
			JSONArray enemies = waveData ["enemies"].AsArray;
//			GameObject[] enemiesGameObject = new GameObject[enemies.Count];
			Wave w = new Wave();
			for (int j = 0; j < enemies.Count; j++) {
				JSONNode enemyData = enemies [j];
				w.AddEnemy (Resources.Load<GameObject>(enemyData ["enemy"]), enemyData ["count"].AsInt, enemyData ["mustBeKilled"].AsBool);
			}
			waveList.Add (w);
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
		CalClosestPos ();
        if (zPosition > 0.1f)
            zPosition = 0;
    }

	// Calculat the closest 3 spawn positions
	void CalClosestPos()
	{
		List<Vector3> SpawnSpots = new List<Vector3>();
		SpawnSpots.Add(new Vector3(0f,-20f,0f));
		SpawnSpots.Add(new Vector3(0f,20f,0f));
		SpawnSpots.Add(new Vector3(0f,30f,0f));
		SpawnSpots.Add(new Vector3(15f,0f,0f));
		SpawnSpots.Add(new Vector3(-15f,0f,0f));
		SpawnSpots.Add(new Vector3(30f,0f,0f));
		SpawnSpots.Add(new Vector3(-30f,0f,0f));
		SpawnSpots.Add(new Vector3(-15f,15f,0f));
		SpawnSpots.Add(new Vector3(-30f,15f,0f));
		SpawnSpots.Add(new Vector3(-15f,-15f,0f));
		SpawnSpots.Add(new Vector3(15f,-15f,0f));
		SpawnSpots.Add(new Vector3(20f,10f,0f));
		SpawnSpots.Add(new Vector3(10f,20f,0f));
		GameObject curPlayer = GameObject.Find ("Player");
		Vector3 PlayerPos = curPlayer.transform.position;

		ClosestSpawn = new List<Vector3>();
		ClosestSpawn.Add(new Vector3(0f, -20f, 0f));
		ClosestSpawn.Add(new Vector3(0f, 20f, 0f));
		ClosestSpawn.Add(new Vector3(0f, 30f, 0f));

		for (int i = 0; i < 13; i++) 
		{
			float temp = Vector3.Distance(PlayerPos, SpawnSpots[i]);
			int index = 0;
			bool f = false;
			for (int j = 0; j < 3; j++) 
			{
				if (Vector3.Distance(PlayerPos, ClosestSpawn [j]) > temp)
				{
					temp = Vector3.Distance (PlayerPos, ClosestSpawn [j]);
					index = j;
					f = true;
				}
			}
			if (f) 
			{
				ClosestSpawn [index] = SpawnSpots [i];
			}
		}

		for (int j = 1; j < 3; j++) 
		{
			if (Vector3.Distance (PlayerPos, ClosestSpawn [j]) < Vector3.Distance (PlayerPos, ClosestSpawn [mark]))
				mark = j;
		}
	}

    IEnumerator spawnEnemy(Wave wave) {
		isSpawning = true;

		// wait some seconds before start
		WholeScreenTextScript.ShowText("Wave " + (currentWave + 1).ToString() + " is coming...");
		yield return new WaitForSeconds(2.5f);
		foreach (KeyValuePair<GameObject, int> pair in wave.normalEnemies) {
			StartCoroutine (SpawnEnemy(pair.Key, pair.Value, false));
			yield return new WaitForSeconds(2f);
		}

		foreach (KeyValuePair<GameObject, int> pair in wave.mustKillEnemies) {
			StartCoroutine (SpawnEnemy(pair.Key, pair.Value, true));
			yield return new WaitForSeconds(2f);
		}
			
		isSpawning = false;
    }

	IEnumerator SpawnEnemy(GameObject obj, int count, bool mustBeKilled) {
		while (count > 0) {
			GameObject enemy = Instantiate<GameObject> (obj);
			enemy.transform.SetParent (mustBeKilled ? mustKillEnemies.transform : normalEnemies.transform);
			enemy.transform.rotation = Quaternion.Euler (0f, 0f, Random.Range (0, 360));
			zPosition += 0.000001f; //Makes sure that monsters always spawn on diffrent layers so there is no z-fighting
			if (count >= 3) {
				for (int i = 0; i < 3; i++) {
					enemy.transform.position = new Vector3 (ClosestSpawn [i].x, ClosestSpawn [i].y, zPosition);
				}
			} else {
				enemy.transform.position = new Vector3 (ClosestSpawn [mark].x, ClosestSpawn [mark].y, zPosition);
			}
			count--;
			yield return new WaitForSeconds(0.5f);
		}


	}
}

public class Wave
{
	public List<KeyValuePair<GameObject, int>> normalEnemies, mustKillEnemies;
    
    public Wave() {
		normalEnemies = new List<KeyValuePair<GameObject, int>> ();
		mustKillEnemies = new List<KeyValuePair<GameObject, int>> ();
    }

	public void AddEnemy (GameObject obj, int count, bool mustBeKilled) {
		KeyValuePair<GameObject, int> pair = new KeyValuePair<GameObject, int> (obj, count);
		if (mustBeKilled) {
			mustKillEnemies.Add (pair);
		} else {
			normalEnemies.Add (pair);
		}

	}

	public int GetTotalCount () {
		return normalEnemies.Count + mustKillEnemies.Count;
	}


}