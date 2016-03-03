using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArmorItem : Item {

	public ArmorItem () {
	}

	public override Sprite defaultSprite () {
		return Resources.Load<Sprite> ("Sprite/gems/gem6");
	}

	public override Type defaultType() {
		return Type.Armor;
	}

	public override string defaultDescription () {
		return "Armor can protect you from hitting.";
	}
}