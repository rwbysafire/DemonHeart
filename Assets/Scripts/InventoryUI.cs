using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

/*
 * Get the item list:
 * itemListDictionary[Item.Type.General] for general items,
 * ... Item.Type.Armor...,
 * ... Item.Type.Skill...,
 * ... Item.Type.Weapon...
 */

public class InventoryUI : MonoBehaviour {

	public Text ItemName;
	public Text ItemDescription;
	public GameObject ItemText;
	public GameObject itemTemplate;
	public GameObject inventory;
	public GameObject background;
	public ItemTemplate itemMoveHolder;
	public Mob playerScript;
	public List<Image> skillImages = new List<Image>();
	public Dictionary<Item.Type, List<Item>> itemListDictionary;
	public int itemSlotCount;

	private ItemTemplate[] itemHolder;
	private static readonly Dictionary<Item.Type, int> itemListLength =
		new Dictionary<Item.Type, int> {
		{ Item.Type.General, 15 },
		{ Item.Type.Armor, 5 },
		{ Item.Type.Skill, 13 }
	};

	private static readonly Dictionary<Item.Type, string> itemTags =
		new Dictionary<Item.Type, string> {
		{ Item.Type.General, "item" },
		{ Item.Type.Armor, "item_armor" },
		{ Item.Type.Skill, "item_skill" }
	};

	// Use this for initialization
	void Start () {
		RectTransform inventoryTransform = (RectTransform) inventory.transform;
		float width = inventoryTransform.rect.width;
		float height = inventoryTransform.rect.height;

		itemListDictionary = new Dictionary<Item.Type, List<Item>> ();
		foreach (Item.Type type in System.Enum.GetValues(typeof(Item.Type))) {
			itemListDictionary.Add (type, new List<Item> ());
		}
		itemHolder = new ItemTemplate[itemSlotCount];
		for (int i = 0; i < itemSlotCount; i++) {
			// clone the item template
			GameObject itemClone = GameObject.Instantiate (itemTemplate) as GameObject;
			itemClone.transform.SetParent (itemTemplate.transform.parent, false);
			itemHolder [i] = itemClone.GetComponent<ItemTemplate> ();
		}

		Destroy (itemTemplate);

		inventory.SetActive (false);
	}

	// setter for ItemText
	private void SetItemName (string s) {
		ItemName.text = s;
	}

	private void SetItemDescription (string s) {
		ItemDescription.text = s;
	}

	// get dictionary of item and itemtemplate
	// mainly for save and load
	public void setItemListDictionary(Dictionary<Item.Type, List<Item>> listDictionary) {
		bool isChanged = !inventory.activeSelf;
		if (isChanged) {
			inventory.SetActive (true);
		}
		foreach (KeyValuePair<Item.Type, string> pair in itemTags) {
			GameObject[] holders = GameObject.FindGameObjectsWithTag (pair.Value);
			List<Item> items = new List<Item> ();
			foreach (Item item in listDictionary [pair.Key]) {
				items.Add (item);
			}
			int listCount = 0;
			for (int i = 0; i < holders.Length; i++) {
				ItemTemplate holder = holders [i].GetComponent<ItemTemplate> ();
				if (holder != null) {
					holder.RemoveItem ();
					if (pair.Key == Item.Type.Skill) {
						// special case for skill gem
						for (int j = 0; j < items.Count; j++) {
							if (holder.index == items[j].itemIndex) {
								this.addSkillGem (holder.index, (Gem) items[j]);
								holder.SetItem (items[j]);
								items.RemoveAt (j);
								break;
							}
						}
					} else {
						// general case
						if (listDictionary [pair.Key].Count > listCount) {
							Item item = listDictionary [pair.Key] [listCount];
							holder.SetItem (item);
							listCount++;
						}
					}
				}
			}
//			Debug.Log (itemTags [i] + holders.Length.ToString ());
		}
		if (isChanged) {
			inventory.SetActive (false);
		}

		this.itemListDictionary = listDictionary;
		foreach (KeyValuePair<Item.Type, List<Item>> pair in itemListDictionary) {
			Debug.Log (pair.Key.ToString () + ":" + pair.Value.Count.ToString ());
		}
	}

	public void ShowText (string name, string description) {
		SetItemName (name);
		SetItemDescription (description);
		ItemText.SetActive (true);
	}

	public void HideText () {
		SetItemName ("");
		SetItemDescription ("");
		ItemText.SetActive (false);
	}

	// return true if the item is added
	// use for pick up
	public bool AddItem (Item item, Item.Type type) {
		bool isAdded = false;
//		Debug.Log (item.itemName + " : " + type.ToString ());
		if (itemListDictionary[type].Count < itemListLength[type]) {
			// find the next available slot to put
			for (int i = 0; i < itemHolder.Length; i++) {
				if (!itemHolder [i].HasItem ()) {
					itemHolder [i].SetItem (item);
					itemListDictionary[type].Add (item);
					isAdded = true;
					break;
				}
			}
		} else {
			// inventory is full
		}

//		foreach (KeyValuePair<Item.Type, List<Item>> pair in itemListDictionary) {
//			Debug.Log (pair.Key.ToString () + ":" + pair.Value.Count.ToString ());
//		}

		return isAdded;
	}

	// use for attach an item
	public bool AttachItem (Item item, Item.Type type) {
		bool isAdded = false;
		if (itemListDictionary[type].Count < itemListLength[type]) {
			itemListDictionary[type].Add (item);

			isAdded = true;
		} else {
			// inventory is full
		}

//		foreach (KeyValuePair<Item.Type, List<Item>> pair in itemListDictionary) {
//			Debug.Log (pair.Key.ToString () + ":" + pair.Value.Count.ToString ());
//		}

		return isAdded;
	}

	public bool AddItem (Item item) {
		return this.AddItem (item, Item.Type.General);
	}

	public void addSkillGem(int index, Gem gem) {
		playerScript.skills [index].addGem (gem);
	}

	public void removeSkillGem (int index, Gem gem) {
		playerScript.skills [index].removeGem (gem);
	}

	public void removeSkillGems (int index) {
		playerScript.skills [index].removeAllGem ();
	}

	public void AddArmorGem (ArmorGem gem) {
		playerScript.AddBuffToStats (gem.GetBuff ());
	}

	public void RemoveArmorGem (ArmorGem gem) {
		playerScript.RemoveBuffFromStats (gem.GetBuff ());
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.C)) {
			if (Pause.IsPaused () && inventory.activeSelf) {
				// resume the game
				Pause.ResumeGame ();

				inventory.SetActive (false);
				background.SetActive (false);
			} else if (!Pause.IsPaused () && !inventory.activeSelf) {
				// pause the game
				Pause.PauseGame ();

				updateInventorySkillImages ();

				// reset the text
				HideText ();

				inventory.SetActive (true);
				background.SetActive (true);
			}
		}
	}

	public void updateInventorySkillImages () {
		// update the images for the skills
		for (int i = 0; i < playerScript.skills.Length; i++) {
			if (playerScript.skills[i] != null) {
				skillImages [i].overrideSprite = playerScript.skills [i].getImage ();
			}
			
		}
	}

	public void OnBackgroundClick () {
		// delete item if there is any child
		itemMoveHolder.RemoveItem ();
	}
}
