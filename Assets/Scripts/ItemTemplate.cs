using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemTemplate : MonoBehaviour {

	public GameObject moveHolder;

	private Item item;
	private int count;

	public void SetItem (Item item) {
		this.item = item;
	}

	public void OnClick () {
		if (transform.childCount != 0) {
			// has child
			if (moveHolder.transform.childCount != 0) {
				Debug.Log ("Replace");
				Transform slotItem = transform.GetChild (0);
				moveHolder.transform.GetChild (0).SetParent (transform, false);
				slotItem.SetParent (moveHolder.transform, false);
			} else {
				Debug.Log ("Slot --> Holder");
				transform.GetChild (0).SetParent (moveHolder.transform, false);
				moveHolder.SetActive (true);
			}
		} else {
			// no child
			if (moveHolder.transform.childCount != 0) {
				Debug.Log ("Holder --> Slot");
				moveHolder.transform.GetChild (0).SetParent (this.transform, false);
				moveHolder.SetActive (false);
			}
		}
	}
}
