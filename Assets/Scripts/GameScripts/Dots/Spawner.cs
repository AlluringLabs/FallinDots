﻿using UnityEngine;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots.Dots {

	public class Spawner : BaseBehaviour {

		public float timeBetweenSpawn = 1f;
		public bool disabled = false;
        public int count = 0;

		float nextSpawnTime;
		
		// Update is called once per frame
		void Update () {
            
            // Has the game started? Is the game paused? Is this spawn point disabled?
            if(GameManager.Instance.started && !GameManager.Instance.paused && !disabled) {
                if(Time.time > nextSpawnTime) {
                    nextSpawnTime = Time.time + timeBetweenSpawn;
                    StartCoroutine(SpawnDot());
                }
            }
		}

        Vector3 RandomizePosition(Vector3 scale) {
            Vector3 randomPosition = CamUtils().GetScreenPosition(Random.value);
            float halfWidth = scale.y / 2;

            if(randomPosition.x < CamUtils().bounds.minX + halfWidth) {
                randomPosition += (Vector3.right * halfWidth);
            } else if(randomPosition.x > CamUtils().bounds.maxX - halfWidth) {
                randomPosition -= (Vector3.right * halfWidth);
            }
            
            // Make sure the dot ALWAYS spawns above the screen
            randomPosition += (Vector3.up * halfWidth);

            return randomPosition;
        }

        Vector3 RandomizeScale(float width, float maxWidth) {
            float randomSize = Random.Range(width, maxWidth);
            return new Vector3(randomSize, randomSize, 1);
        }

        public void SetSpawnTimer(float newTime) {
            timeBetweenSpawn = newTime;
        }

        IEnumerator SpawnDot() {
            count = count + 1;
            Dot newDot = ThemeManager.Instance.GetRandom();
            newDot.width = 0.5f;

            Vector3 randomScale = RandomizeScale(newDot.width, 3.0f);
            Vector3 newPosition = RandomizePosition(randomScale);

            Dot spawnedDot = (Dot) Instantiate(newDot, newPosition, Quaternion.identity);
            spawnedDot.transform.localScale = randomScale;
            spawnedDot.transform.parent = GameObject.Find("DynamicObjects").transform;
            spawnedDot.transform.tag = "Dot";
            spawnedDot.name = "DOT_" + count;

            yield return null;
        }
            
	}

}