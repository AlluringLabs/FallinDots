using UnityEngine;
using System;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots.Dots {
	
    public class Dot : BaseBehaviour {

        //public delegate void DotDestroyed();
        //public event DotDestroyed dotDestroyed;
//        public event Action DotDestroyed(bool byUser);

		// TODO: Add modifiers for dots like powerups, movement variations, etc...

		// How fast the dot should be moving
		public float speed = 3f;

		// How big the dot is
		public float width = 1.5f;

//        public bool isDisabled = false;

		void Update() {
            // if the game manager is not paused
            if(!GameManager.Instance.paused) {
                Move();
            }
		}

        public void destroy() {
//            if(!isDisabled) {
//                isDisabled = true;
                GameManager.Instance.updateScore();
                Destroy(gameObject);
//            }
        }

		private void Move() {
            float moveDistance = speed * Time.deltaTime;
            Vector3 currentPosition = transform.position;
            Vector3 nextPosition = currentPosition + (Vector3.down * moveDistance);

            if(nextPosition.y < CamUtils().bounds.minY - this.gameObject.transform.localScale.x) {
                Destroy(gameObject);
            } else {
                transform.Translate(Vector3.down * moveDistance);
            }
		}

	}

}