using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;

public class SpawnEnemies : MonoBehaviour {

	public CanvasGroup displayText;
    public int currentWave = 0;
	public TextAsset waveFile;

	private List<Wave> waveList;
	private bool isSpawning = false;

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