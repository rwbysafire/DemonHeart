using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour{

	public Image itemImage;

	public string itemName { get; set; }
	public string itemDescription { get; set; }
	private static string[] spritePaths = {"gem4", "gem5", "gem6"};

	public Item (string name, string description) : base() {
		this.itemName = name;
		this.itemDescription = description;
//		Debug.Log(spritePaths[0]);

	}

	void Start () {
		
		itemImage.overrideSprite = Resources.Load<Sprite> ("Sprite/gems/" + spritePaths[(int) Random.Range(0, 2.9f)]);
	}
}