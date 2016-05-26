using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AoeSkill : SkillType {

    private Dictionary<string, float> Properties = new Dictionary<string, float>();
    public Dictionary<string, float> properties {
        get {
            return Properties;
        }
    }

    public AoeSkill() {
        Properties.Add("areaOfEffect", 1);
    }
}
