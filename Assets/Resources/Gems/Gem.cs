using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gem {
    public Dictionary<string, int> properties = new Dictionary<string, int>();
}


public class extraProjectilesGem : Gem {
    public extraProjectilesGem() {
        properties.Add("projectileCount", 4);
    }
}

public class extraChainsGem : Gem {
    public extraChainsGem() {
        properties.Add("chainCount", 5);
    }
}