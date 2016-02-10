using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

	public Text ItemName;
	public Text ItemDescription;
	public GameObject itemTemplate;
	public GameObject inventory;
	public GameObject itemMoveHolder;
	public Mob playerScript;
	public List<Image> skillImages = new List<Image>();
	public int itemSlotCount;

	private ItemTemplate[] itemHolder;
	private Item[] items;
	private int itemCount = 0;

	// Use this for initialization
	void Start () {
		RectTransform inventoryTransform = (RectTransform) inventory.transform;
		float width = inventoryTransform.rect.width;
		float height = inventoryTransform.rect.height;
		items = new Item[itemSlotCount];
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
	public void SetItemName (string s) {
		ItemName.text = s;
	}

	public void SetItemDescription (string s) {
		ItemDescription.text = s;
	}

	// return true if the item is added
	public bool AddItem (Item item) {
		if (itemCount < items.Length) {
			itemHolder [itemCount].SetItem (item);
			itemCount++;
			return true;
		} else {
			// inventory is full
			return false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			// update the images for the skills
			for (int i = 0; i < playerScript.skills.Length; i++) {
				skillImages [i].overrideSprite = playerScript.skills [i].getImage ();
			}

			// reset the text
			ItemName.text = "";
			ItemDescription.text = "";

			inventory.SetActive (true);
		} else if (Input.GetKeyUp (KeyCode.B)) {
			inventory.SetActive (false);
		} else if (inventory.activeSelf) {
			
		}
	}
}
