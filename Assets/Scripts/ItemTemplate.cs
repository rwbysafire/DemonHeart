using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemTemplate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject moveHolder;
	public InventoryUI inventory;

	private Item item;
	private int count;

	public void SetItem (Item item) {
		this.item = item;
		this.item.transform.SetParent (transform, false);
	}

	public void RemoveItem () {
		if (this.tag.Contains ("_")) {
			inventory.itemListDictionary [this.item.type].Remove (this.item);	
		} else {
			inventory.itemListDictionary [Item.Type.General].Remove (this.item);
		}
		this.item = null;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if (item != null) {
			inventory.ShowText (item.itemName, item.itemDescription);
		}
	}

	public void OnPointerExit (PointerEventData eventData) {
		inventory.HideText ();
	}

	public void OnClick () {
		if (transform.childCount != 0) {
			// has child
			if (moveHolder.transform.childCount != 0) {
//				Debug.Log ("Replace");
				// need to check the type of the gem
				if (!gameObject.tag.Contains("_") || gameObject.tag == moveHolder.transform.GetChild (0).gameObject.tag) {
					Transform slotItem = transform.GetChild (0);
					this.SetItem (moveHolder.transform.GetChild (0).GetComponent<Item> ());
					slotItem.SetParent (moveHolder.transform, false);
				} else {
					// reject the placing
				}
			} else {
//				Debug.Log ("Slot --> Holder");
				this.RemoveItem ();
				transform.GetChild (0).SetParent (moveHolder.transform, false);
				moveHolder.transform.position = Input.mousePosition;
				moveHolder.SetActive (true);
			}
		} else {
			// no child
			if (moveHolder.transform.childCount != 0) {
//				Debug.Log ("Holder --> Slot");
				// need to check the type of the gem
				if (!gameObject.tag.Contains("_") || gameObject.tag == moveHolder.transform.GetChild (0).gameObject.tag) {
//					Item item = moveHolder.transform.GetChild (0).GetComponent<Item> ();
					this.SetItem (moveHolder.transform.GetChild (0).GetComponent<Item> ());
					inventory.AttachItem (
						this.item,
						gameObject.tag.Contains ("_")? this.item.type : Item.Type.General);
					moveHolder.SetActive (false);
				} else {
					// reject the placing
				}
			} else {
				// empty click
			}
		}
	}
}
