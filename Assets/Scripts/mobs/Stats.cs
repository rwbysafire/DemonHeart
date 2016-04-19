using UnityEngine;
using System.Collections;

[System.Serializable]
public class Stats {
	public int level = 0;
	public float exp = 0;
	public int strength = 10;
	public int dexterity = 10;
	public int intelligence = 10;
	public float baseHealth = 600;
	public float baseMana = 200;
	public float threshold = 200;
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
			return 0 + (2 * intelligence);
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
		this.exp += buff.exp;
		this.strength += buff.strength;
		this.dexterity += buff.dexterity;
		this.intelligence += buff.intelligence;
		this.baseHealth += buff.baseHealth;
		this.baseMana += buff.baseMana;
		this.threshold += buff.threshold;
	}

	public void RemoveBuff (Buff buff) {
		this.level -= buff.level;
		this.exp -= buff.exp;
		this.strength -= buff.strength;
		this.dexterity -= buff.dexterity;
		this.intelligence -= buff.intelligence;
		this.baseHealth -= buff.baseHealth;
		this.baseMana -= buff.baseMana;
		this.threshold -= buff.threshold;
	}
}
