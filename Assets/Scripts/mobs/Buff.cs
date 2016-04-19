using UnityEngine;
using System.Collections;

[System.Serializable]
public class Buff : Stats {
	public Buff () {
		this.level = 0;
		this.exp = 0;
		this.strength = 0;
		this.dexterity = 0;
		this.intelligence = 0;
		this.baseHealth = 0;
		this.baseMana = 0;
		this.threshold = 0;
	}
}
