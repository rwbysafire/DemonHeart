using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveObject {
	public Stats stats;
	public Dictionary<Item.Type, List<Item>> items;

	public SaveObject(Stats stats, Dictionary<Item.Type, List<Item>> items) {
		this.stats = stats;
		this.items = items;
	}
}

public class CharSaveLoadScript : MonoBehaviour {

	public Player player;
	public InventoryUI inventory;
	private BinaryFormatter bf;

	// Use this for initialization
	void Start () {
		bf = new BinaryFormatter ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.N)) {
			FileStream file = File.Create (Application.persistentDataPath + "/characters.data");
			SaveObject saveObject = new SaveObject (player.stats, inventory.itemListDictionary);
			bf.Serialize (file, saveObject);
			file.Close ();
			Debug.Log ("Player saved to " + Application.persistentDataPath);
		} else if (Input.GetKeyDown (KeyCode.M)) {
			FileStream file = File.Open (Application.persistentDataPath + "/characters.data", FileMode.Open);
			SaveObject loadObject = (SaveObject) bf.Deserialize (file);
			file.Close ();
			player.stats = loadObject.stats;
			inventory.setItemListDictionary (loadObject.items);
			Debug.Log ("Player status loaded");
		}
	}
}
