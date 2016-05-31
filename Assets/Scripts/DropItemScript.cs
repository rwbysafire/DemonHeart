using UnityEngine;
using System.Collections;

public class DropItemScript : MonoBehaviour {

	private static float TIMEOUT_SECOND = 10;
	public Item item;
    public int[] armourDrops, skillDrops;

	public void initItem () {
		switch (gameObject.tag) {
		case "item_armor":
			switch (armourDrops[Random.Range(0, armourDrops.Length)]) {
			case 0:
				item = new ArmorGemOfStrength ();
				break;
			case 1:
				item = new ArmorGemOfDexterity ();
				break;
			case 2:
				item = new ArmorGemOfIntelligence ();
				break;
			case 3:
				item = new ArmorGemOfPower ();
				break;
			case 4:
				item = new ArmorGemOfSpeed ();
				break;
			case 5:
				item = new ArmorGemOfEnergy ();
				break;
			case 6:
				item = new ArmorGemOfWisdom ();
				break;
			case 7:
				item = new ArmorGemOfAttributes ();
				break;
			default:
				item = new Gem ();
				break;
			}
			break;
		case "item_skill":
			switch (skillDrops[Random.Range(0, skillDrops.Length)]) {
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
			case 6:
				item = new GemIncreasedAoe();
				break;
			case 7:
				item = new GemSuperiorIncreasedAoe();
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
        GameObject dropSound = Instantiate<GameObject>(Resources.Load<GameObject>("Gems/DropSound"));
        dropSound.transform.position = transform.position;
		initItem ();
		Invoke ("Timeout", TIMEOUT_SECOND);
	}

	// Update is called once per frame
	void Update () {

	}
}
