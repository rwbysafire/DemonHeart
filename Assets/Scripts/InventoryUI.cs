using UnityEngine;
using System.Collections;

public class InventoryUI : MonoBehaviour {

	public GameObject itemTemplate;
	public GameObject inventory;
	public GameObject itemMoveHolder;
	public int itemSlotCount;

	private ItemTemplate[] itemHolder;
	private Item[] items;
	private int itemCount = 0;

	// Use this for initialization
	void Start () {
		RectTransform inventoryTransform = (RectTransform) inventory.transform;
		float width = inventoryTransform.rect.width;
		float height = inventoryTransform.rect.height;
		Debug.Log ("width: " + width.ToString ());
		Debug.Log ("height: " + height.ToString ());

		Debug.Log ("count: " + itemSlotCount.ToString ());
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

	public void AddItem (Item item) {
		if (itemCount < items.Length) {
			itemHolder [itemCount].SetItem (item);
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
		} else if (inventory.activeSelf) {
			
		}
	}
}
