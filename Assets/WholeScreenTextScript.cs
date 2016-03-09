using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WholeScreenTextScript : MonoBehaviour {

	public static Text displayText;
	private static CanvasGroup cg;
	private static bool isFading;
	private static float step = 0.012f;

	// Use this for initialization
	void Start () {
		if (cg == null) {
			Init ();
		}
	}

	void Init() {
		displayText = gameObject.GetComponentInChildren<Text> ();
		cg = gameObject.GetComponent<CanvasGroup> ();
	}

	// Update is called once per frame
	void Update () {
		if (isFading) {
			if (cg.alpha == 0) {
				isFading = false;
			}

			cg.alpha = Mathf.Clamp01 (cg.alpha - step);
		}
	}

	public static void ShowText (string text) {
		if (cg != null) {
			cg.alpha = 1f;
			isFading = true;
			displayText.text = text;	
		}
	}
}
