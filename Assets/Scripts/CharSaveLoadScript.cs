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

	public Player player;
	public InventoryUI inventory;
	private BinaryFormatter bf;

	// Use this for initialization
	void Start () {
		bf = new BinaryFormatter ();
	}
	
	// Update is called once per frame
	void Update () {
		
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
		inventory.setItemListDictionary (loadObject.items);
		for (int i = 0; i < loadObject.skillNames.Length; i++) {
			for (int j = 0; j < player.listOfSkills.Length; j++) {
				if (loadObject.skillNames[i] == player.listOfSkills[j].getName ()) {
					player.replaceSkill (i, player.listOfSkills [j]);
					break;
				}
			}
		}
		inventory.updateInventorySkillImages ();
		WholeScreenTextScript.ShowText ("Game loaded");
		Debug.Log ("Player status loaded");
	}
}
