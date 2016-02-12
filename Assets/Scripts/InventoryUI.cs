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
	public GameObject itemMoveHolder;
	public Mob playerScript;
	public List<Image> skillImages = new List<Image>();
	public Dictionary<Item.Type, List<Item>> itemListDictionary;
	public int itemSlotCount;

	private ItemTemplate[] itemHolder;
	private static readonly Dictionary<Item.Type, int> itemListLength =
		new Dictionary<Item.Type, int> {
		{ Item.Type.General, 12 },
		{ Item.Type.Armor, 5 },
		{ Item.Type.Skill, 12 },
		{ Item.Type.Weapon, 3 }
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

		itemTemplate.SetActive (false);
	}

	// setter for ItemText
	private void SetItemName (string s) {
		ItemName.text = s;
	}

	private void SetItemDescription (string s) {
		ItemDescription.text = s;
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
//		Debug.Log (item.itemName + " : " + type.ToString ());
		if (itemListDictionary[type].Count < itemListLength[type]) {
			itemHolder [itemListDictionary[type].Count].SetItem (item);
			itemListDictionary[type].Add (item);
			return true;
		} else {
			// inventory is full
			return false;
		}
	}

	// use for attach an item
	public bool AttachItem (Item item, Item.Type type) {
		if (itemListDictionary[type].Count < itemListLength[type]) {
			itemListDictionary[type].Add (item);
			return true;
		} else {
			// inventory is full
			return false;
		}
	}

	public bool AddItem (GameObject gameObject) {
		Item item = Instantiate (Resources.Load<Item> ("Items/Item"));
		item.SetSprite (gameObject.GetComponent<SpriteRenderer> ().sprite);
		item.gameObject.tag = gameObject.tag;
		item.itemName = gameObject.name.Replace("(Clone)", "");
		item.itemDescription = "I am a gem.";

		switch (gameObject.tag) {
		case "item_armor":
			item.type = Item.Type.Armor;
			break;
		case "item_skill":
			item.type = Item.Type.Skill;
			break;
		case "item_weapon":
			item.type = Item.Type.Weapon;
			break;
		default:
			item.type = Item.Type.General;
			break;
		}

		return this.AddItem (item, Item.Type.General);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			// output item status
//			foreach (KeyValuePair<Item.Type, List<Item>> entry in itemListDictionary) {
//				Debug.Log (entry.Key.ToString () + ": " + entry.Value.Count.ToString ());
//			}

			// update the images for the skills
			for (int i = 0; i < playerScript.skills.Length; i++) {
				skillImages [i].overrideSprite = playerScript.skills [i].getImage ();
			}

			// reset the text
			HideText ();

			inventory.SetActive (true);
		} else if (Input.GetKeyUp (KeyCode.B)) {
			inventory.SetActive (false);
		} else if (inventory.activeSelf) {
			
		}
	}

	public void OnBackgroundClick () {
		// delete item if there is any child
		if (itemMoveHolder.transform.childCount > 0) {
			Destroy (itemMoveHolder.transform.GetChild (0).gameObject);
		}
	}
}
