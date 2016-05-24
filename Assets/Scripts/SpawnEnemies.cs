using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SpawnEnemies : MonoBehaviour {

    public int currentWave = 0;
	public TextAsset waveFile;
	public GameObject mustKillEnemies, normalEnemies;

	private List<Wave> waveList;
	private int isSpawnFinished = 0;
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

				SpawnSet spawnSet = new SpawnSet (
					                    Resources.Load<GameObject> (enemyData ["enemy"]),
					                    enemyData ["count"].AsInt,
					                    enemyData ["mustBeKilled"].AsBool,
					                    enemyData ["interval"].AsFloat);

				// look for buff
				if (enemyData ["buff"].Count > 0) {
					JSONClass buffJson = enemyData ["buff"].AsObject;
					Buff buff = new Buff ();
					buff.strengthAddon = buffJson ["str"].AsInt;
					buff.dexterityAddon = buffJson ["dex"].AsInt;
					buff.intelligenceAddon = buffJson ["int"].AsInt;
					buff.baseHealth = buffJson ["health"].AsInt;
					buff.baseMana = buffJson ["mana"].AsInt;
					buff.level = j + 1;
					spawnSet.SetBuff (buff);
				}

				w.AddEnemy (spawnSet);
			}
			waveList.Add (w);
		}
		Debug.Log (waveList.Count.ToString () + " waves loaded");
    }
	
	// Update is called once per frame
	void Update () {
        if (mustKillEnemies.transform.childCount == 0 && currentWave <= waveList.Count - 1 && isSpawnFinished <= 0) {
            StopAllCoroutines();
			isSpawnFinished = 1;
            StartCoroutine("spawnEnemy", waveList[currentWave]);
            print ("Starting wave: " + currentWave.ToString ());
            currentWave += 1;
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

		// wait some seconds before start
		WholeScreenTextScript.ShowText("Wave " + (currentWave + 1).ToString() + " is coming...");
		yield return new WaitForSeconds(2.5f);
		foreach (SpawnSet spawnSet in wave.enemies) {
            if (spawnSet.mustBeKilled)
                isSpawnFinished += 1;
            StartCoroutine (SpawnEnemy(spawnSet));
			yield return new WaitForSeconds(1f);
		}
			
		isSpawnFinished -= 1;
    }

	IEnumerator SpawnEnemy(SpawnSet spawnSet) {
		int count = spawnSet.count;
		while (count > 0) {
			if (count >= 3) {
				for (int i = 0; i < 3; i++) {
					GameObject enemy = Instantiate<GameObject> (spawnSet.obj);
					enemy.transform.SetParent (spawnSet.mustBeKilled ? mustKillEnemies.transform : normalEnemies.transform);
					enemy.transform.rotation = Quaternion.Euler (0f, 0f, Random.Range (0, 360));
					zPosition += 0.000001f; //Makes sure that monsters always spawn on diffrent layers so there is no z-fighting

					enemy.GetComponent<Mob> ().AddBuffToStats (spawnSet.buff);

					enemy.transform.position = new Vector3 (ClosestSpawn [i].x, ClosestSpawn [i].y, zPosition);
					yield return new WaitForSeconds(spawnSet.interval);
					count--;
				}
			} else {
				GameObject enemy = Instantiate<GameObject> (spawnSet.obj);
				enemy.transform.SetParent (spawnSet.mustBeKilled ? mustKillEnemies.transform : normalEnemies.transform);
				enemy.transform.rotation = Quaternion.Euler (0f, 0f, Random.Range (0, 360));
				zPosition += 0.000001f; //Makes sure that monsters always spawn on diffrent layers so there is no z-fighting

				enemy.GetComponent<Mob> ().AddBuffToStats (spawnSet.buff);

				enemy.transform.position = new Vector3 (ClosestSpawn [mark].x, ClosestSpawn [mark].y, zPosition);
				yield return new WaitForSeconds(spawnSet.interval);
				count--;
			}

		}
        isSpawnFinished -= 1;
    }
}

public class Wave
{
	public List<SpawnSet> enemies;
    
    public Wave() {
		enemies = new List<SpawnSet> ();
    }

	public void AddEnemy (SpawnSet spawnSet) {
		enemies.Add (spawnSet);
	}

}

public class SpawnSet {
	public GameObject obj;
	public int count;
	public bool mustBeKilled;
	public float interval;
	public Buff buff;

	public SpawnSet (GameObject obj, int count, bool mustBeKilled, float interval) {
		this.obj = obj;
		this.count = count;
		this.mustBeKilled = mustBeKilled;
		this.interval = interval;
	}

	public void SetBuff (Buff buff) {
		this.buff = buff;
	}
}