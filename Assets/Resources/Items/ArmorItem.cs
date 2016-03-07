using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class ArmorItem : Item {

	public ArmorItem () {
	}

	public override string defaultSpritePath () {
		return "Sprite/gems/gem6";
	}

	public override Type defaultType() {
		return Type.Armor;
	}

	public override string defaultDescription () {
		return "Armor can protect you from hitting.";
	}
}