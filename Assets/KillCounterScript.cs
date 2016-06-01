using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class KillCounterScript : MonoBehaviour {

	public Player player;
	public Text counter;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		counter.text = player.stats.killCount.ToString ();
	}
}
