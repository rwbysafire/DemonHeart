using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill  
{
	public Mob mob;
	private float cooldown;
    public Dictionary<string, int> properties = new Dictionary<string, int>();
    public Gem[] gems = new Gem[2];


	public Skill(Mob mob) {
		this.mob = mob;
		cooldown = 0.0f;
        updateSkill();
        addGem(0, new extraProjectilesGem());
        addGem(1, new extraChainsGem());
    }

    private void updateSkill() {
        initProperties();
        foreach (Gem gem in gems) {
            if (gem != null) {
                foreach (string key in new List<string>(properties.Keys)) {
                    if (gem.properties.ContainsKey(key)) {
                        properties[key] += gem.properties[key];
                    }
                }
            }
        }
    }

    public virtual void initProperties() { }

    public bool useSkill() {
		if (cooldown <= Time.fixedTime && !mob.isAttacking && mob.useMana(getManaCost())) {
			mob.attack(this, getAttackSpeed()/mob.stats.attackSpeed);
			cooldown = Time.fixedTime + getMaxCooldown(); 
			return true;
		}
		return false; 
	}

	public float remainingCooldown() {
		float tempTime = cooldown - Time.fixedTime;
		if (tempTime < 0)
			tempTime = 0;
		return tempTime;
	}

    public void addGem(int slot, Gem gem) {
        gems[slot] = gem;
        updateSkill();
    }

    public void removeGem(int slot) {
        gems[slot] = null;
        updateSkill();
    }

	public abstract string getName();
	public abstract Sprite getImage();
	public virtual float getMaxCooldown() {return 0;}
	public virtual float getAttackSpeed() {return 0;}
	public abstract float getManaCost();
	public abstract void skillLogic(); 
	public virtual void skillPassive() {}
	public virtual void skillFixedUpdate() {}
}
