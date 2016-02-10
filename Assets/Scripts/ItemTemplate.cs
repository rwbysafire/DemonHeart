using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemTemplate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public GameObject moveHolder;
	public InventoryUI inventory;

	private static GameObject lastSlot;
	private Item item;
	private int count;

	public void SetItem (Item item) {
		this.item = item;
		this.item.transform.SetParent (transform, false);
	}

	public void RemoveItem () {
		this.item = null;
	}

	public void OnPointerEnter (PointerEventData eventData) {
		if (item != null) {
			inventory.SetItemName (item.itemName);
			inventory.SetItemDescription (item.itemDescription);
		}
	}

	public void OnPointerExit (PointerEventData eventData) {
		inventory.SetItemName ("");
		inventory.SetItemDescription ("");
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
					// reject the place and redo the pickup
					moveHolder.transform.GetChild (0).SetParent (lastSlot.transform, false);
					moveHolder.SetActive (false);
				}
			} else {
//				Debug.Log ("Slot --> Holder");
				this.RemoveItem ();
				transform.GetChild (0).SetParent (moveHolder.transform, false);
				lastSlot = this.gameObject;
				moveHolder.transform.position = Input.mousePosition;
				moveHolder.SetActive (true);
			}
		} else {
			// no child
			if (moveHolder.transform.childCount != 0) {
//				Debug.Log ("Holder --> Slot");
				// need to check the type of the gem
				if (!gameObject.tag.Contains("_") || gameObject.tag == moveHolder.transform.GetChild (0).gameObject.tag) {
					this.SetItem (moveHolder.transform.GetChild (0).GetComponent<Item> ());
					moveHolder.SetActive (false);
				} else {
					// reject the place and redo the pickup
					moveHolder.transform.GetChild (0).SetParent (lastSlot.transform, false);
					moveHolder.SetActive (false);
				}
			} else {
				Debug.Log ("Something goes wrong");
			}
		}
	}
}
