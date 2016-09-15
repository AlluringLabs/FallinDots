using UnityEngine;
using System.Collections;

namespace FallinDots.Generic
{
	public class BaseBehaviour : MonoBehaviour {

        public static CameraUtils CamUtils() {
            return Camera.main.GetComponent<CameraUtils>();
        }

	}
}