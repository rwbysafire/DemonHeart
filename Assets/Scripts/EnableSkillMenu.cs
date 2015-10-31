using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnableSkillMenu : MonoBehaviour {

	/**
	 * SKILL CONTAINER !!IN PROGRESS!!
	 * Things that must be done are labeled with 'TODO'
	 * 
	 * -- Summary of SkillContainer's presentation --
	 * 
	 * Player presses a key, menu comes up.
	 * Menu is generated as such:
	 *  - GUI image is drawn in the center of the screen, scaled by resolution.
	 *  - On the image, several containers are drawn, representing items and gem slots. (Highlighting a container draws a tooltip after 1-3 seconds.)
	 * 
	 * -- Summary of Skill Container and Skill switching --
	 * 
	 * The menu contains a selection of weapons, armor and skills that the character is currently using, with empty boxes
	 * underneath each equipped item. The boxes is where picked-up gems are placed and removed to buff the player's abilities.
	 * 
	 * -----
	 * |   | <- Item (weapon, armor, skill, etc)
	 * |   |
	 * -----
	 * -- --
	 * || || <- "Gem slots"
	 * -- --
	 * 
	 * When you select a skill slot, the menu is replaced with another menu that allows you to switch your skills with others
	 * that you have unlocked via fighting and leveling. (Alternatively, skills can unlock after a certain amount of waves.)
	 * The player may use any variation of 3 skills that they have unlocked.
	 * 
	 * -- Gems (proposal) --
	 * 
	 * Selecting an empty box will bring up a different menu that contains the empty box, and underneath are gems that
	 * can fit in that box. For instance, "Lighting-Infused Shot" gems won't fit in your armor gem slots, but will in your weapons.
	 * (It should also be noted that weapon buffs also apply to your skills.)
	 * 
	 * Idea: There may also be gems that you pick up that contain multiple buffs, say "+5% armor and +10% damage," and either
	 * we allow the gem to use both buffs regardless of where it's placed, or make the player choose which buff they want
	 * from the gem via placing it in either an armor gem slot or weapon gem slot.
	 * 
	 * For now, let's make red gems represent weapons, yellow gems represent armor, and blue gems represent magic.
	 * Gems that contain a weapon (red) and magic (blue) buff will be purple.
	 * Gems that contain a weapon (red) and armor (yellow) buff will be orange.
	 * Gems that contain a magic (blue) and armor (yellow) buff will be green.
	 * (In our previous example, let's say a "Lightning-Infused Shot" gem also increased mana. That gem would be purple.)
	 * (Also, the armor gems will contain the health increase bonuses and not just, you know... armor stuff.)
	 * 
	 * These aren't ideas that are set in stone, but serve as a way that we could differentiate from the ARPG's we're
	 * taking influence from - mainly, Diablo and Path of Exile.
	 */

	//NOTE: "gameObject" is the parent of the script (aka the player), and "GameObject" refers to the actual object type.

	private bool isActive;
	private GameObject menu;
	private Image img;

	void Start() {
		isActive = false;

		// To get a focus effect on the skill menu, we'll overlay an opaque black fill behind the menu.
		img = (Image) GameObject.Find("SkillBGFade").gameObject.GetComponent(typeof(Image)); // Our reference to the background object.
		menu = GameObject.Find ("SkillMenu"); // We'll be using a SkillMenu variable for this too.

		// The skill menu is hidden by default (it's a pain in the scene view), so we make a throwaway variable for enabling it.
		var menuSprite = (Image) GameObject.Find("SkillMenu-Sprite").gameObject.GetComponent(typeof(Image));

		menuSprite.enabled = true; // Enables the skill menu.
		img.enabled = true; // Enables the background black image (it's also obtrusive in the scene view).
		showMenu(false); // showMenu is set to false. This hides the menu and background at startup. This must always be the last step.
	}

	void Update() {
		// Toggle skill menu logic
		if (Input.GetKeyDown(KeyCode.I)) {
			isActive = !isActive;
			showMenu(isActive);
		}

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

	public void showMenu(bool b) {
		foreach (Transform child in this.transform) {
			if (child.name == "SkillMenu") {
				child.gameObject.SetActive(b);
				if (b) {
					PopulateSkillMenu population = (PopulateSkillMenu) child.gameObject.GetComponentInChildren(typeof(PopulateSkillMenu));
					population.enable = false;
				} else {
					foreach (Transform child2 in child.transform) {
						if (child2.name == "SkillMenu_Sprite") {
							foreach (Transform child3 in child2.transform) {
								GameObject.Destroy (child3.gameObject);
							}
						}
					}
				}
			}
		}
	}
}
