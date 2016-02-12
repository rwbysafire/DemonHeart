using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour {

	public Text ItemName;
	public Text ItemDescription;
	public GameObject ItemText;
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
			
		}
	}
}
