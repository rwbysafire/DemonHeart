using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stats {
    public string tag;

	public static int ExtraPointsPerLevel = 3;
	public static int InitPoints = 10;

	public Stats () {
	}

    public Stats (string tag) {
        this.tag = tag;
    }

	public int level = 0;
	public float exp = 0;
	public float CurExp = 0;
	public int pts = 0;

	private int _strength = 10;
	public int baseStrength {
		get {return this._strength;}
		set {this._strength = value;}
	}
	public int strengthAddon = 0;
	public int strengthActualAddon {
		get {return this.strengthAddon * level;}
	}
	public int strength {
		get {return this._strength + strengthActualAddon;}

	}

	private int _dexterity = 10;
	public int baseDexterity {
		get {return this._dexterity;}
		set {this._dexterity = value;}
	}
	public int dexterityAddon = 0;
	public int dexterityActualAddon {
		get {return this.dexterityAddon * level;}
	}
	public int dexterity {
		get {return this._dexterity + dexterityActualAddon;}
	}

	private int _intelligence = 10;
	public int baseIntelligence {
		get {return this._intelligence;}
		set {this._intelligence = value;}
	}
	public int intelligenceAddon = 0;
	public int intelligenceActualAddon {
		get {return this.intelligenceAddon * level;}
	}
	public int intelligence {
		get {return this._intelligence + intelligenceActualAddon;}
	}

	public float baseHealth;
	public float baseMana;
	public float threshold;
	public float maxHealth {
		get {
			return baseHealth + (10 * level) + (2 * strength);
		}
	}
	public float health;
	public float healthRegen {
		get {
			return 3 + (0.1f * strength);
		}
	}
	public float maxMana {
		get {
			return baseMana + (5 * level) + intelligence;
		}
	}
	public float mana;
	public float manaRegen {
		get {
			return 1 + (0.2f * intelligence);
		}
	}
	public float basicAttackDamage {
		get {
			return 60 + level + (3 * dexterity);
		}
	}
	public float attackDamage {
		get {
			return 40 + (2 * level) + (2 * strength);
		}
	}
	public float criticalDamage {
		get {
			return 200 + (0.2f * dexterity);
		}
	}
	public float abilityPower {
		get {
			return 0 + (1 * intelligence);
		}
	}
	public float attackSpeed {
		get {
			return 1f + (0.01f * dexterity);
		}
	}
	public float defence {
		get {
			return 20 + level + strength;
		}
	}
	public float cooldownReduction {
		get {
			return 0.1f * intelligence;
		}
	}
	public float moveSpeed {
		get {
			return 100 + (0.3f * dexterity);
		}
	}

	public void AddBuff (Buff buff) {
		this.level += buff.level;
		this.baseStrength += buff.baseStrength;
		this.baseDexterity += buff.baseDexterity;
		this.baseIntelligence += buff.baseIntelligence;

		this.baseHealth += buff.baseHealth;
		this.baseMana += buff.baseMana;
		this.threshold += buff.threshold;

		this.strengthAddon += buff.strengthAddon;
		this.dexterityAddon += buff.dexterityAddon;
		this.intelligenceAddon += buff.intelligenceAddon;
	}

	public void RemoveBuff (Buff buff) {
		this.level -= buff.level;
		this.baseHealth -= buff.baseHealth;
		this.baseMana -= buff.baseMana;
		this.threshold -= buff.threshold;

		this.strengthAddon -= buff.strengthAddon;
		this.dexterityAddon -= buff.dexterityAddon;
		this.intelligenceAddon -= buff.intelligenceAddon;

		this.baseStrength -= buff.baseStrength;
		this.baseDexterity -= buff.baseDexterity;
		this.baseIntelligence -= buff.baseIntelligence;
	}
}
