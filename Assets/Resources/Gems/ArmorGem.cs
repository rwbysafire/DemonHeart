using UnityEngine;
using System.Collections;

[System.Serializable]
public class ArmorGem : ArmorItem {
	protected Buff buff = new Buff ();

	public ArmorGem () {
		InitBuff ();
	}

	// override this in the new class to give the buff
	protected virtual void InitBuff () {

		// random gen a gem
//		switch (Random.Range (0, 5)) {
//		case 0:
//			buff.baseHealth = Random.Range (100, 300);
//			this.itemDescription = "Health +" + buff.baseHealth.ToString ();
//			break;
//		case 1:
//			buff.baseMana = Random.Range (100, 300);
//			this.itemDescription = "Mana +" + buff.baseMana.ToString ();
//			break;
//		case 2:
//			buff.strength = Random.Range (10, 50);
//			this.itemDescription = "Strength +" + buff.strength.ToString ();
//			break;
//		case 3:
//			buff.intelligence = Random.Range (10, 50);
//			this.itemDescription = "Intelligence +" + buff.intelligence.ToString ();
//			break;
//		case 4:
//			buff.dexterity = Random.Range (10, 50);
//			this.itemDescription = "Dexterity +" + buff.dexterity.ToString ();
//			break;
//		}
	}

	public Buff GetBuff () {
		return buff;
	}
}

[System.Serializable]
public class ArmorGemOfStrength : ArmorGem {
	protected override void InitBuff() {
		buff.baseStrength = Random.Range (5, 11);
		buff.strengthAddon = 3;
		this.itemName = "Gem of Strength";
		this.itemDescription = string.Format("STR +{0}(+{1}/lv)", buff.strength, buff.strengthAddon);
	}
}

[System.Serializable]
public class ArmorGemOfDexterity : ArmorGem {
	protected override void InitBuff() {
		buff.baseDexterity = Random.Range (5, 11);
		buff.dexterityAddon = 3;
		this.itemName = "Gem of Dexterity";
		this.itemDescription = string.Format("DEX +{0}(+{1}/lv)", buff.dexterity, buff.dexterityAddon);
	}
}

[System.Serializable]
public class ArmorGemOfIntelligence : ArmorGem {
	protected override void InitBuff() {
		buff.baseIntelligence = Random.Range (5, 11);
		buff.intelligenceAddon = 3;
		this.itemName = "Gem of Intelligence";
		this.itemDescription = string.Format("INT +{0}(+{1}/lv)", buff.intelligence, buff.intelligenceAddon);
	}
}

[System.Serializable]
public class ArmorGemOfPower : ArmorGem {
	protected override void InitBuff() {
		buff.baseStrength = Random.Range (3, 7);
		buff.strengthAddon = 2;
		buff.baseDexterity = Random.Range (2, 5);
		buff.dexterityAddon = 1;
		this.itemName = "Gem of Power";
		this.itemDescription = string.Format("STR +{0}(+{1}/lv)\nDEX +{2}(+{3}/lv)",
			buff.strength, buff.strengthAddon, buff.dexterity, buff.dexterityAddon);
	}
}

[System.Serializable]
public class ArmorGemOfSpeed : ArmorGem {
	protected override void InitBuff() {
		buff.baseDexterity = Random.Range (3, 7);
		buff.dexterityAddon = 2;
		buff.baseIntelligence = Random.Range (2, 5);
		buff.strengthAddon = 1;
		this.itemName = "Gem of Speed";
		this.itemDescription = string.Format("DEX +{0}(+{1}/lv)\nSTR +{2}(+{3}/lv)",
			buff.dexterity, buff.dexterityAddon, buff.strength, buff.strengthAddon);
	}
}