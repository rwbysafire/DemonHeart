using UnityEngine;
using System.Collections;

[System.Serializable]
public class Buff : Stats {
	public Buff (string tag = "") : base (tag){
		this.level = 0;
		this.exp = 0;
		this.baseStrength = 0;
		this.baseDexterity = 0;
		this.baseIntelligence = 0;
		this.baseHealth = 0;
		this.baseMana = 0;
		this.threshold = 0;
	}
}
