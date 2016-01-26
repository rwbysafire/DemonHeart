using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemTemplate : MonoBehaviour {

	[SerializeField]
	public Image image;
	[SerializeField]
	public Text nameText;

	private Item item;
	private int count;

	private void SetName (string name) {
		nameText.text = name;
	}

	private void SetImage (Sprite sprite) {
		image.sprite = sprite;
	}

	public void SetItem (Item item) {
		this.item = item;
		SetImage (item.image);
		SetName (item.name);
	}
		
}
