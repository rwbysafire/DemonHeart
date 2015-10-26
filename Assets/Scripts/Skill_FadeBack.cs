using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * This function changes the opacity of a huge-ass black UI image, depending on if the SkillMenu object is activated
 * or not. This creates a nice background darken effect while the skill menu is "open" (activated).
 */

public class Skill_FadeBack : MonoBehaviour {
	
	private Image img;
	private GameObject menu;

	void Start() {
		img = (Image) gameObject.GetComponent(typeof(Image)); // Pulling the Image component from the object.
		menu = (GameObject) GameObject.Find("SkillMenu"); // We'll be using a SkillMenu variable for this too.

		// To keep the huge-ass black box from intruding the scene view, no opacity or anything, it's disabled by default.
		// Only when the game is running is it re-activated. (You're welcome.)
		img.enabled = true;
	}
	
	void Update() {
		// Changes the opacity of SkillFade's image.
		if (!menu.gameObject.activeSelf) { // If the Skill Menu object is not active...
			var c = img.color;
			c.a = 0; // ...we set the opacity to 0 (clear as crystal).
			img.color = c;
		} else { // Otherwise...
			var c = img.color;
			c.a = 0.4f; // ...the opacity of the image is 40%. Our background darken effect is achieved.
			img.color = c;
		}
	}
}
