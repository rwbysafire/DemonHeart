using UnityEngine;
using System.Collections;

public class Stats {
	public int level;
	public float exp;
	public int STR, DEX, INT;
	public float hp, defence, attackDamage;
	public float evasion, criticalDamage, basicAttackDamage, attackSpeed, moveSpeed;
	public float mana, manaRegen, abilityPower, magicProtection, cooldown;

	public float getLevel() {
		return level;
	}
	public float getExp() {
		return exp;
	}
	public float getSTR() {
		return STR;
	}
	public float getDEX() {
		return DEX;
	}
	public float getINT() {
		return INT;
	}
	public float getHp() {
		return hp;
	}
	public float getDefence() {
		return defence;
	}
	public float getAttackDamage() {
		return attackDamage;
	}
	public float getEvasion() {
		return evasion;
	}
	public float getCriticalDamage() {
		return criticalDamage;
	}
	public float getBasicAttackDamage() {
		return basicAttackDamage;
	}
	public float getAttackSpeed() {
		return attackSpeed;
	}
	public float getMoveSpeed() {
		return moveSpeed;
	}
	public float getMana() {
		return mana;
	}
	public float getManaRegen() {
		return manaRegen;
	}
	public float getAbilityPower() {
		return abilityPower;
	}
	public float getMagicProtection() {
		return magicProtection;
	}
	public float getCoolDown() {
		return cooldown;
	}
}
