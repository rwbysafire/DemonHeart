using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {

	private Light light;
	private float originalIntensity;
	public float minSecondsBetweenFlick = 0f;
	public float maxSecondsBetweenFlick = 1f;
	public float minIntensity;

	void Start () {
		light = gameObject.GetComponent<Light>();
		originalIntensity = light.intensity;
		StartCoroutine("flick");
	}

	private IEnumerator flick() {
		light.intensity = Random.Range (0f, originalIntensity);
		yield return new WaitForSeconds(Random.Range(minSecondsBetweenFlick, maxSecondsBetweenFlick));
		StartCoroutine("flick");
	}
}
