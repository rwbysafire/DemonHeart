using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour{

	public Image itemImage;

	public string itemName { get; set; }
	public string itemDescription { get; set; }

	public Item (string name, string description) : base() {
		this.itemName = name;
		this.itemDescription = description;
	}
}