using UnityEngine;
using System.Collections;

public class Item {
	private const string DEFAULT_IMAGE_PATH = "Sprite/Arrow_placeholder";

	public string name { get; set; }
	public string description { get; set; }
	public Sprite image { get; set; }

	public Item (string name, string description, string imagePath) {
		this.name = name;
		this.description = description;
		this.image = Resources.Load<Sprite>((imagePath == "" ? DEFAULT_IMAGE_PATH : imagePath));
	}
}