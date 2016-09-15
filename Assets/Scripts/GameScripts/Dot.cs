using UnityEngine;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots {
	
	public class Dot : BaseBehaviour {

		// TODO: Add modifiers for dots like powerups, movement variations, etc...

		// How fast the dot should be moving
		public float speed = 3f;

		// How big the dot is
		public float width = 2.5f;

		void Update() {
            if(!GameManager.Instance.paused) {
                Move();
            }
		}

		private void Move() {
            float moveDistance = speed * Time.deltaTime;
            Vector3 currentPosition = transform.position;
            Vector3 nextPosition = currentPosition + (Vector3.down * moveDistance);

            if(nextPosition.y < CamUtils().bounds.minY) {
                Destroy(gameObject);
            } else {
                transform.Translate(Vector3.down * moveDistance);
            }
		}

	}

}