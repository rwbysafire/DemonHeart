using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour{

	public Image itemImage;

	public string itemName { get; set; }
	public string itemDescription { get; set; }

	public void SetSprite (Sprite sprite) {
		itemImage.overrideSprite = sprite;
	}

	void Start () {

	}
}