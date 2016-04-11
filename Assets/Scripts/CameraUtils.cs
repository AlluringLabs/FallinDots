using UnityEngine;
using System.Collections;

public class CameraUtils : MonoBehaviour {
	
	Camera camera;
	public ScreenBounds screenBounds;
	
	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera>();
	}
	
	public void UpdateScreenBounds() {
		
	}
	
	public Vector3 GetScreenPosition(float x) {
		return GetScreenPosition(x, 1.0f);
	}
	
	public Vector3 GetScreenPosition(float x, float y) {
		Vector3 viewport = new Vector3(x, y, camera.nearClipPlane);
		return camera.ViewportToWorldPoint(viewport);
	}
	
	public class ScreenBounds {
		public float minX;
		public float maxX;
		public float minY;
		public float MaxY;
	}
	
}
