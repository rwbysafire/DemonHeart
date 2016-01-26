using UnityEngine;
using System.Collections;

public class InventoryUI : MonoBehaviour {

	public GameObject itemTemplate;
	public GameObject inventory;
	public int itemSlotCount;

	private ItemTemplate[] itemHolder;
	private Item[] items;
	private int itemCount = 0;
	private RectTransform itemTemplateRectTransform;

	// Use this for initialization
	void Start () {
		RectTransform inventoryTransform = (RectTransform) inventory.transform;
		float width = inventoryTransform.rect.width;
		float height = inventoryTransform.rect.height;
		Debug.Log ("width: " + width.ToString ());
		Debug.Log ("height: " + height.ToString ());

		itemTemplateRectTransform = itemTemplate.GetComponent<RectTransform> ();

		// calculate the padding --> replaced by the grid layout
//		float columnPadding = (width - itemTemplateRectTransform.rect.width * columnCount) / (columnCount - 1);
//		float rowPadding = (height - itemTemplateRectTransform.rect.height * rowCount) / (rowCount - 1);
//		int itemSlotCount = rowCount * columnCount;

		Debug.Log ("count: " + itemSlotCount.ToString ());
		items = new Item[itemSlotCount];
		itemHolder = new ItemTemplate[itemSlotCount];
		for (int i = 0; i < itemSlotCount; i++) {
			// clone the item template and assign the new position
			GameObject itemClone = GameObject.Instantiate (itemTemplate) as GameObject;
			itemClone.transform.SetParent (itemTemplate.transform.parent, false);
//			itemClone.transform.localPosition = itemTemplate.transform.localPosition + new Vector3 (
//				(itemTemplateRectTransform.rect.width + columnPadding) * (i % columnCount),
//				(itemTemplateRectTransform.rect.height + rowPadding) * (int) (i / columnCount) * -1,
//				0
//			);
			itemHolder [i] = itemClone.GetComponent<ItemTemplate> ();
		}

		itemTemplate.SetActive (false);
	}

	public void AddItem (Item item) {
		if (itemCount < items.Length) {
			items [itemCount] = new Item ("", "I am an item.", "Sprite/gems/gem1");
			itemHolder [itemCount].SetItem (items [itemCount]);
			itemCount++;
		} else {
			Debug.Log ("Inventory full");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.B)) {
			inventory.SetActive (true);
		} else if (Input.GetKeyUp (KeyCode.B)) {
			inventory.SetActive (false);
			AddItem (null);
		} else if (inventory.activeSelf) {
			Debug.Log ("inventory shown");
		}
	}
}
