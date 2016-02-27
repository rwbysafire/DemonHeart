using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gem {
    public Dictionary<string, int> properties = new Dictionary<string, int>();

    public virtual void onHitEffect() { }
}


public class GemExtraProjectiles : Gem {
    public GemExtraProjectiles() {
        properties.Add("projectileCount", 4);
    }
}

public class GemExtraChains : Gem {
    public GemExtraChains() {
        properties.Add("chainCount", 5);
    }
}

public class chainLightningOnHitGem : Gem {

    private SkillChainLightning skillChainLightning;

    public chainLightningOnHitGem() {
        skillChainLightning = new SkillChainLightning(GameObject.Find("Player").GetComponent<Mob>());
        skillChainLightning.properties["manaCost"] = 0;
        properties.Add("chainCount", 1);
    }

    public override void onHitEffect() {
        Debug.Log("gem used");
        skillChainLightning.skillLogic();
    }
}