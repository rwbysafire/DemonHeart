using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveObject {
	public Stats stats;
}

public class CharSaveLoadScript : MonoBehaviour {

	public Player player;
	private BinaryFormatter bf;

	// Use this for initialization
	void Start () {
		bf = new BinaryFormatter ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) {
			FileStream file = File.Create (Application.persistentDataPath + "/characters.data");
			SaveObject saveObject = new SaveObject ();
			saveObject.stats = player.stats;
			bf.Serialize (file, saveObject);
			file.Close ();
			Debug.Log ("Player saved to " + Application.persistentDataPath);
		} else if (Input.GetKeyDown (KeyCode.M)) {
			FileStream file = File.Open (Application.persistentDataPath + "/characters.data", FileMode.Open);
			SaveObject loadObject = (SaveObject) bf.Deserialize (file);
			file.Close ();
			player.stats = loadObject.stats;
			Debug.Log ("Player status loaded");
		}
	}
}
