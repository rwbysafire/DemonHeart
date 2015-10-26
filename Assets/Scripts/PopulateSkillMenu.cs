using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PopulateSkillMenu : MonoBehaviour {

	public bool enable;

	void Start () {
		enable = false;
	}

	void Update () {
		if (gameObject.activeSelf == true) {
			if (!enable) {
				drawSkillItems();
				enable = true;
			}
		}
	}

	private void drawSkillItems() {
		GameObject skillFab1 = (GameObject) Instantiate(Resources.Load("test", typeof(GameObject)));
		skillFab1.transform.SetParent(this.transform, false);
		skillFab1.transform.position = new Vector3(760, 740, 1);

	}
}
