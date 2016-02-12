using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour{

	public enum Type {
		Armor,
		General,
		Skill,
		Weapon
	};

	public Image itemImage;

	public string itemName { get; set; }
	public string itemDescription { get; set; }
	public Type type = Type.General;

	public void SetSprite (Sprite sprite) {
		itemImage.overrideSprite = sprite;
	}

	void Start () {

	}
}