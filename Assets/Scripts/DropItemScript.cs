using UnityEngine;
using System.Collections;

public class DropItemScript : MonoBehaviour {

	private static float TIMEOUT_SECOND = 30;
	public Item item;

	public void initItem () {
		switch (gameObject.tag) {
		case "item_armor":
			switch (((int)Time.time) % 5) {
			case 0:
				item = new ArmorGemOfDexterity ();
				break;
			case 1:
				item = new ArmorGemOfIntelligence ();
				break;
			case 2:
				item = new ArmorGemOfPower ();
				break;
			case 3:
				item = new ArmorGemOfSpeed ();
				break;
			case 4:
				item = new ArmorGemOfStrength ();
				break;
			default:
				item = new Gem ();
				break;
			}
			break;
		case "item_skill":
			switch (((int)Time.time) % 6) {
			case 0:
				item = new GemAttackSpeed ();
				break;
			case 1:
				item = new GemExtraProjectiles ();
				break;
			case 2:
				item = new GemExtraChains ();
				break;
			case 3:
				item = new GemCooldownReduction ();
				break;
			case 4:
				item = new GemChainLightningOnHit();
				break;
			case 5:
				item = new GemReducedDuration();
				break;
			default:
				item = new Gem ();
				break;
			}
			break;
		case "item_weapon":
			item = new WeaponGem ();
			break;
		default:
			Debug.Log ("Error with tag: " + gameObject.tag);
			break;
		}

		item.tag = gameObject.tag;
	}

	private void Timeout () {
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		initItem ();
		Invoke ("Timeout", TIMEOUT_SECOND);
	}

	// Update is called once per frame
	void Update () {

	}
}
