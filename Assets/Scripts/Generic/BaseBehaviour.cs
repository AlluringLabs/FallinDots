using UnityEngine;
using System.Collections;

namespace FallinDots.Generic
{
	public class BaseBehaviour : MonoBehaviour {

        public Camera mainCamera;
        public CameraUtils cameraUtils;
        public CameraUtils.Bounds cameraBounds;

		// Use this for initialization
		void Start () {
            mainCamera = Camera.main;
            cameraUtils = (CameraUtils) mainCamera.GetComponent<CameraUtils>();
            cameraBounds = (CameraUtils.Bounds) cameraUtils.bounds;
		}

        public static CameraUtils GetCameraUtils() {
            return this.cameraUtils;
        }

	}
}