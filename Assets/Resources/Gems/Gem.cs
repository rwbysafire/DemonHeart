using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Gem : Item {
    public Dictionary<string, property> properties = new Dictionary<string, property>();

    public virtual void onHitEffect() { }

	public override string defaultSpritePath () {
		return "Sprite/gems/gem5";
	}

	public override Type defaultType() {
		return Type.Skill;
	}

	public override string defaultDescription () {
		return "No skill for this :)";
	}

    [System.Serializable]
    public class property {
        public float value;
        public string operation;
        public property(float value, string operation) {
            this.value = value;
            this.operation = operation;
        }
    }
}

[System.Serializable]
public class WeaponGem : Gem {
	public WeaponGem () {
		this.itemDescription = "Power up your weapon!";
		properties.Add("projectileCount", new property(2, "+"));
	}

	public override string defaultSpritePath () {
		return "Sprite/gems/gem4";
	}

	public override Type defaultType() {
		return Type.Weapon;
	}
}

[System.Serializable]
public class GemExtraProjectiles : Gem {
    public GemExtraProjectiles() {
        properties.Add("projectileCount", new property(2, "+"));
		this.itemDescription = "Extra projectiles";
    }
}

[System.Serializable]
public class GemExtraChains : Gem {
    public GemExtraChains() {
        properties.Add("chainCount", new property(2, "+"));
		this.itemDescription = "Extra chains";
    }
}

[System.Serializable]
public class GemAttackSpeed : Gem {
    public GemAttackSpeed() {
        properties.Add("attackSpeed", new property(0.5f, "*"));
        this.itemDescription = "Double attack speed";
    }
}

public class GemCooldownReduction : Gem {
    public GemCooldownReduction() {
        properties.Add("cooldown", new property(0.5f, "*"));
        this.itemDescription = "Reduce cooldown";
    }
}

// need to be fixed
// for serialization, it cannot contain the skill class
public class chainLightningOnHitGem : Gem {

    private SkillChainLightning skillChainLightning;

    public chainLightningOnHitGem() {
        skillChainLightning = new SkillChainLightning(GameObject.Find("Player").GetComponent<Mob>());
        skillChainLightning.properties["manaCost"] = 0;
        properties.Add("chainCount", new property(1, "+"));

		this.itemDescription = "Chain lightning";
    }

    public override void onHitEffect() {
//        Debug.Log("gem used");
        skillChainLightning.skillLogic();
    }
}