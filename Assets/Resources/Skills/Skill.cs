using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Skill  
{
	public Mob mob;
	private float cooldown;
    public Dictionary<string, float> properties = new Dictionary<string, float>();
    public Gem[] gems = new Gem[2];
    private List<SkillType> skillTypes = new List<SkillType>();


	public Skill(Mob mob) {
		this.mob = mob;
		cooldown = 0.0f;
        updateSkill();
    }

    private void updateSkill() {
        properties = new Dictionary<string, float>();
        initBaseProperties();
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

    private void initBaseProperties() {
        properties.Add("manaCost", getManaCost());
        properties.Add("attackSpeed", getAttackSpeed());
        properties.Add("cooldown", getMaxCooldown());
        foreach (SkillType skillType in skillTypes) {
            foreach (KeyValuePair<string, float> property in skillType.properties) {
                properties.Add(property.Key, property.Value);
            }
        }
    }

    public virtual void initProperties() { }

    public void addSkillType(SkillType skillType) {
        skillTypes.Add(skillType);
        updateSkill();
    }

    public bool useSkill() {
		if (cooldown <= Time.fixedTime && !mob.isAttacking && mob.useMana(properties["manaCost"])) {
			mob.attack(this, properties["attackSpeed"] / mob.stats.attackSpeed);
			cooldown = Time.fixedTime + properties["cooldown"]; 
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
