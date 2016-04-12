using UnityEngine;
using System.Collections;

public class CameraUtils : MonoBehaviour {
	
	public Camera cam;
	public Bounds bounds;
	
	// Use this for initialization
	void Start () {
		cam = GetComponent<Camera>();
		bounds = new Bounds();
		UpdateScreenBounds();
	}
	
	// Updates the bounds of the screen and puts them in the ScreenBounds class.
	public void UpdateScreenBounds() {
		bounds.maxX = GetScreenPosition(0.0f).x;
		bounds.minX = GetScreenPosition(1.0f).x;
		bounds.minY = GetScreenPosition(0.0f, 0.0f).y;
		bounds.maxY = GetScreenPosition(0.0f, 1.0f).y;
	}
	
	public Vector3 GetScreenPosition(float x) {
		return GetScreenPosition(x, 1.0f);
	}
	
	public Vector3 GetScreenPosition(float x, float y) {
		Vector3 viewport = new Vector3(x, y, cam.nearClipPlane);
		return cam.ViewportToWorldPoint(viewport);
	}
	
	public class Bounds {
		public float minX;
		public float maxX;
		public float minY;
		public float maxY;
	}
	
}
