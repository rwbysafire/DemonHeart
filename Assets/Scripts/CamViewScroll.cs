using UnityEngine;
using System.Collections;

public class CamViewScroll : MonoBehaviour {
/// <summary>
/// When attached to the main camera in the scene, enables the user to use the scroll wheel on the mouse to zoom in/out.
/// </summary>

	[Header("Recommended: ~4")]
	public int minimumZoom;
	[Header("Unity Default: 5")]
	public int defaultZoom;
	[Header("Recommended: ~8")]
	public int maximumZoom;

	private int currentZoom;

	private Camera cam;

	void Start () {
		currentZoom = defaultZoom;
		cam = Camera.main;
		cam.orthographicSize = currentZoom;
	}

	void Update () {
		if (Input.GetAxis("Mouse ScrollWheel") > 0f) {
			cam.orthographicSize -= 1;
			if (cam.orthographicSize < minimumZoom) {
				cam.orthographicSize = minimumZoom;
			}
		} else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
			cam.orthographicSize += 1;
			if (cam.orthographicSize > maximumZoom) {
				cam.orthographicSize = maximumZoom;
			}
		}
	}
}
