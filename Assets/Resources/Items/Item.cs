using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class Item {

	public enum Type {
		Armor,
		General,
		Skill,
		Weapon
	};

	public string itemName { get; set; }
	public string itemDescription { get; set; }
	public string tag { get; set; }
	public Sprite sprite { get; set; }
	public Type type = Type.General;

	public Item () {
		this.sprite = defaultSprite ();
		this.type = defaultType ();
		this.itemDescription = defaultDescription ();
	}

	public abstract Sprite defaultSprite ();
	public abstract Type defaultType ();
	public abstract string defaultDescription ();
}