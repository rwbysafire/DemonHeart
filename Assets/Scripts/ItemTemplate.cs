using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class ItemTemplate : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	public InventoryUI inventory;

	// only used in skill and weapon type yet
	public int index;

	private static ItemTemplate moveHolder;
	private Image itemImage;
	private Item item;
	private int count;

	void Awake () {
		initObject ();
	}

	void Start () {
		
	}

	public void initObject () {
		itemImage = transform.GetChild (0).GetComponentInChildren<Image> ();
		if (itemImage == null) {
			Debug.LogError ("image not found");
		}

		if (moveHolder == null) {
			GameObject gameObject = GameObject.FindGameObjectWithTag ("item_move_holder");
			moveHolder = gameObject.GetComponent<ItemTemplate> ();
		}
	}

	public bool HasItem() {
		return this.item != null;
	}

	public void SetItem (Item item) {
		this.item = item;
		itemImage.overrideSprite = Resources.Load<Sprite> (item.spritePath);
		itemImage.color = Color.white;
		itemImage.transform.localPosition = new Vector2 (0, 0);

		if (gameObject.tag == "item_skill") {
			item.itemIndex = this.index;
		} else {
			item.itemIndex = -1;
		}
	}

	public Item RemoveItem () {
		Item removedItem = null;
		if (this.HasItem ()) {
			if (this.tag.Contains ("_")) {
				inventory.itemListDictionary [this.item.type].Remove (this.item);	
			} else {
				inventory.itemListDictionary [Item.Type.General].Remove (this.item);
			}
			removedItem = this.item;
			this.item = null;
			itemImage.sprite = null;
			itemImage.color = Color.clear;
		}

		return removedItem;
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
		if (this.item != null) {
			// has item
			if (moveHolder.item != null) {
//				Debug.Log ("Replace");
				// need to check the type of the gem
				if (!gameObject.tag.Contains("_") || gameObject.tag == moveHolder.item.tag) {
					Item item = moveHolder.RemoveItem ();
					if (inventory.AttachItem (item,
						    gameObject.tag.Contains ("_") ? item.type : Item.Type.General)) {
						Item slotItem = this.RemoveItem ();
						this.SetItem (item);
						moveHolder.SetItem (slotItem);

						if (gameObject.tag == "item_skill") {
							inventory.removeSkillGem (this.index, (Gem) slotItem);
							inventory.addSkillGem (this.index, (Gem) this.item);
						} else if (gameObject.tag == "item_armor") {
							inventory.RemoveArmorGem ((ArmorGem)slotItem);
							inventory.AddArmorGem ((ArmorGem) this.item);
						}
					} else {
						moveHolder.SetItem (item);
					}	
				} else {
					// reject the placing
				}
			} else {
//				Debug.Log ("Slot --> Holder");
				if (gameObject.tag == "item_skill") {
					inventory.removeSkillGem (this.index, (Gem)this.item);
				} else if (gameObject.tag == "item_armor") {
					inventory.RemoveArmorGem ((ArmorGem) this.item);
				}
				moveHolder.SetItem (this.RemoveItem ());
				moveHolder.transform.position = Input.mousePosition;
				moveHolder.gameObject.SetActive (true);
			}
		} else {
			// no item
			if (moveHolder.item != null) {
//				Debug.Log ("Holder --> Slot");
				// need to check the type of the gem
				if (!gameObject.tag.Contains("_") || gameObject.tag == moveHolder.item.tag) {
					Item item = moveHolder.RemoveItem ();

					if (inventory.AttachItem (item,
						    gameObject.tag.Contains ("_") ? item.type : Item.Type.General)) {
						this.SetItem (item);
						if (gameObject.tag == "item_skill") {
							inventory.addSkillGem (this.index, (Gem)this.item);
						} else if (gameObject.tag == "item_armor") {
							inventory.AddArmorGem ((ArmorGem) this.item);
						}
					} else {
						moveHolder.SetItem (item);
					}
					moveHolder.gameObject.SetActive (false);
				} else {
					// reject the placing
				}
			} else {
				// empty click
			}
		}
	}
}
