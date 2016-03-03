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
		Item item = null;

		switch (gameObject.tag) {
		case "item_armor":
			item = new ArmorItem ();
			break;
		case "item_skill":
			switch (((int)Time.time) % 3) {
			case 0:
				item = new chainLightningOnHitGem ();
				break;
			case 1:
				item = new GemExtraProjectiles ();
				break;
			case 2:
				item = new GemExtraChains ();
				break;
			default:
				item = new Gem ();
				break;
			}
			break;
		case "item_weapon":
			item = new WeaponGem ();
			break;
		default:
			Debug.Log ("Error with tag: " + gameObject.tag);
			break;
		}
			
		item.tag = gameObject.tag;
		item.itemName = gameObject.name.Replace("(Clone)", "");

		return this.AddItem (item, Item.Type.General);
	}

	public void addSkillGem(int index, Gem gem) {
		playerScript.skills [index].addGem (gem);
	}

	public void removeSkillGem (int index, Gem gem) {
		playerScript.skills [index].removeGem (gem);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			if (Pause.IsPaused () && inventory.activeSelf) {
				// resume the game
				Pause.ResumeGame ();

				inventory.SetActive (false);
				background.SetActive (false);
			} else if (!Pause.IsPaused () && !inventory.activeSelf) {
				// pause the game
				Pause.PauseGame ();

				// update the images for the skills
				for (int i = 0; i < playerScript.skills.Length; i++) {
					skillImages [i].overrideSprite = playerScript.skills [i].getImage ();
				}

				// reset the text
				HideText ();

				inventory.SetActive (true);
				background.SetActive (true);
			}
		}
	}

	public void OnBackgroundClick () {
		// delete item if there is any child
		itemMoveHolder.RemoveItem ();
	}
}
