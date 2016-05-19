using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public class SaveObject {
	public Stats stats;
	public Buff buff;
	public Dictionary<Item.Type, List<Item>> items;
	public string[] skillNames;

	public SaveObject(Stats stats, Buff buff, Dictionary<Item.Type, List<Item>> items, string[] skillNames) {
		this.stats = stats;
		this.items = items;
		this.skillNames = skillNames;
		this.buff = buff;
	}
}

public class CharSaveLoadScript : MonoBehaviour {

	public static string PREFS_LOAD_GAME = "LOAD_GAME";

	public Player player;
	public InventoryUI inventory;

	private bool hasTriedLoading = false;
	private static BinaryFormatter bf = new BinaryFormatter ();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasTriedLoading) {
			if (PlayerPrefs.GetInt (CharSaveLoadScript.PREFS_LOAD_GAME) == 1) {
				Debug.Log ("Autoload game");
				LoadGame ();
			}
			hasTriedLoading = true;
		}
	}

	public void SaveGame () {
		FileStream file = File.Create (Application.persistentDataPath + "/characters.data");

		string[] skillNames = new string[player.skills.Length];
		for (int i = 0; i < player.skills.Length; i++) {
			skillNames [i] = player.skills [i].getName ();
		}

		SaveObject saveObject = new SaveObject (player.stats, player.buff, inventory.itemListDictionary, skillNames);
		bf.Serialize (file, saveObject);
		file.Close ();
		WholeScreenTextScript.ShowText ("Game saved");
		Debug.Log ("Player saved to " + Application.persistentDataPath);
	}

	public void LoadGame () {
		FileStream file = File.Open (Application.persistentDataPath + "/characters.data", FileMode.Open);
		SaveObject loadObject = (SaveObject) bf.Deserialize (file);
		file.Close ();
		player.stats = loadObject.stats;
		player.buff = loadObject.buff;
		
		for (int i = 0; i < loadObject.skillNames.Length; i++) {
			for (int j = 0; j < player.listOfSkills.Length; j++) {
				if (loadObject.skillNames[i] == player.listOfSkills[j].getName ()) {
					player.replaceSkill (i, player.listOfSkills [j]);
					break;
				}
			}
		}
		inventory.updateInventorySkillImages ();

        inventory.setItemListDictionary(loadObject.items);
        WholeScreenTextScript.ShowText ("Game loaded");
		Debug.Log ("Player status loaded");
	}

	public static bool HasSavedData () {
		FileStream file = File.Open (Application.persistentDataPath + "/characters.data", FileMode.Open);
		SaveObject loadObject = (SaveObject) bf.Deserialize (file);
		file.Close ();
		return loadObject != null;
	}
}
