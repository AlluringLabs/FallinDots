using UnityEngine;
using System.Collections;

namespace FallinDots.Generic {
	
	public class CameraUtils : BaseBehaviour
	{

        private Camera cam;
	    public Bounds bounds;

	    // Use this for initialization
	    void Start() {
	        bounds = new Bounds();
            cam = GetComponent<Camera>();
	        UpdateScreenBounds();
	    }

	    // Updates the bounds of the screen and puts them in the ScreenBounds class.
	    public void UpdateScreenBounds() {
	        bounds.minX = GetScreenPosition(0.0f).x;
	        bounds.maxX = GetScreenPosition(1.0f).x;
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

	    public struct Bounds {
	        public float minX, maxX, minY, maxY;
	    }
	}

}