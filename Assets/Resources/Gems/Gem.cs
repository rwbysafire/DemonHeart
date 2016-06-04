using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Gem : Item {
    public Dictionary<string, property> properties = new Dictionary<string, property>();

	public Gem () {
		this.itemName = "Skill Gem";
	}

    public virtual void onHitEffect(Entity entity, Stats stats) { }

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
		this.itemName = "Weapon Gem";
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
        this.itemName = "Spread Gem";
        properties.Add("projectileCount", new property(2, "+"));
		this.itemDescription = "Skill Gem\nSkill fires 2 extra projectiles";
    }
}

[System.Serializable]
public class GemExtraChains : Gem {
    public GemExtraChains() {
        this.itemName = "Chain Gem";
        properties.Add("chainCount", new property(2, "+"));
		this.itemDescription = "Skill Gem\nSkill chains 2 additional times";
    }
}

[System.Serializable]
public class GemAttackSpeed : Gem {
    public GemAttackSpeed() {
        this.itemName = "Gem of the Tiger";
        properties.Add("attackSpeed", new property(0.8f, "*"));
        this.itemDescription = "Skill Gem\nIncrease skill attack speed by 20%";
    }
}

[System.Serializable]
public class GemCooldownReduction : Gem {
    public GemCooldownReduction() {
        this.itemName = "Gem of cooldown";
        properties.Add("cooldown", new property(0.8f, "*"));
        this.itemDescription = "Skill Gem\nReduce skill cooldown by 20%";
    }
}

[System.Serializable]
public class GemReducedDuration : Gem {
    public GemReducedDuration() {
        this.itemName = "Less Duration";
        properties.Add("duration", new property(0.5f, "*"));
        this.itemDescription = "Skill Gem\nReduce skill duration by 50%";
    }
}

[System.Serializable]
public class GemIncreasedAoe : Gem {
    public GemIncreasedAoe() {
        this.itemName = "Increased Area of Effect";
        properties.Add("areaOfEffect", new property(1.5f, "*"));
        this.itemDescription = "Skill Gem\n50% Increased area of effect";
    }
}

[System.Serializable]
public class GemSuperiorIncreasedAoe : Gem {
    public GemSuperiorIncreasedAoe() {
        this.itemName = "Superior Increased Area of Effect";
        properties.Add("areaOfEffect", new property(2f, "*"));
        this.itemDescription = "Skill Gem\n100% Increased area of effect";
    }
}

[System.Serializable]
public class GemChainLightningOnHit : Gem {

    public GemChainLightningOnHit() {
        //properties.Add("chainCount", new property(1, "+"));

        this.itemName = "Shock Gem";
        this.itemDescription = "Skill Gem\nOnly effects Basic Attack\nCast Chain lightning on hit";
    }

    public override void onHitEffect(Entity entity, Stats stats) {
        //Debug.Log("gem used");
        SkillChainLightning skillChainLightning = new SkillChainLightning();
        skillChainLightning.properties["manaCost"] = 0;
        skillChainLightning.skillLogic(entity, stats);
    }
}