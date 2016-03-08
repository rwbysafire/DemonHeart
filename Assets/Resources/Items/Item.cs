using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
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
	public string spritePath { get; set; }
	public Type type = Type.General;
	public int itemIndex { get; set; }

	public Item () {
		this.spritePath = defaultSpritePath ();
		this.type = defaultType ();
		this.itemDescription = defaultDescription ();
		this.itemIndex = -1;
	}

	public abstract string defaultSpritePath ();
	public abstract Type defaultType ();
	public abstract string defaultDescription ();
}