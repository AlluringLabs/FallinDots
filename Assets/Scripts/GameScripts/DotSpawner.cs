using UnityEngine;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots {

	public class DotSpawner : BaseBehaviour {

		GameManager gameManager;

		public float timeBetweenSpawn = 2;
		public bool disabled = false;

		float nextSpawnTime;

		// Use this for initialization
		void Start () {
			gameManager = FindObjectOfType<GameManager> ();
		}
		
		// Update is called once per frame
		void Update () {
            // Has the game started? Is the game paused? Is this spawn point disabled?

            if(gameManager.started && !gameManager.paused && !this.disabled) {
                if(Time.time > nextSpawnTime) {
                    nextSpawnTime = Time.time + timeBetweenSpawn;
                }
            }
		}

//        Vector3 RandomizePosition(float width) {
//            Vector3 randomPosition = CameraUtils.GetScreenPosition(Random.value);
//            float halfWidth = width / 2;
//
//            if(randomPosition.x < CameraUtils.Bounds.minX + halfWidth) {
//                
//            }
//        }

	}

}