using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gem : Item {
    public Dictionary<string, int> properties = new Dictionary<string, int>();

    public virtual void onHitEffect() { }

	public override Sprite defaultSprite () {
		return Resources.Load<Sprite> ("Sprite/gems/gem5");
	}

	public override Type defaultType() {
		return Type.Skill;
	}

	public override string defaultDescription () {
		return "No skill for this :)";
	}
}

public class WeaponGem : Gem {
	public WeaponGem () {
		this.itemDescription = "Power up your weapon!";
		properties.Add("projectileCount", 4);
	}

	public override Sprite defaultSprite () {
		return Resources.Load<Sprite> ("Sprite/gems/gem4");
	}

	public override Type defaultType() {
		return Type.Weapon;
	}
}

public class GemExtraProjectiles : Gem {
    public GemExtraProjectiles() {
        properties.Add("projectileCount", 4);
		this.itemDescription = "Extra projectiles";
    }
}

public class GemExtraChains : Gem {
    public GemExtraChains() {
        properties.Add("chainCount", 5);
		this.itemDescription = "Extra chains";
    }
}

public class chainLightningOnHitGem : Gem {

    private SkillChainLightning skillChainLightning;

    public chainLightningOnHitGem() {
        skillChainLightning = new SkillChainLightning(GameObject.Find("Player").GetComponent<Mob>());
        skillChainLightning.properties["manaCost"] = 0;
        properties.Add("chainCount", 1);

		this.itemDescription = "Chain lightning";
    }

    public override void onHitEffect() {
//        Debug.Log("gem used");
        skillChainLightning.skillLogic();
    }
}