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
		buff.strength = Random.Range (5, 11);
		buff.strengthAddon = 3;
		this.itemDescription = string.Format("STR +{0}(+{1}/lv)", buff.strength, buff.strengthAddon);
	}
}