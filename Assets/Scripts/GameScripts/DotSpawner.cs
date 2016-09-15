using UnityEngine;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots {

	public class DotSpawner : BaseBehaviour {

		GameManager gameManager;

		public float timeBetweenSpawn = 1f;
		public bool disabled = false;
        public int count = 0;

		float nextSpawnTime;

		// Use this for initialization
		void Start () {
			gameManager = FindObjectOfType<GameManager>();
            Debug.Log("Started");
		}
		
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

        Vector3 RandomizePosition(float width) {
            Vector3 randomPosition = CamUtils().GetScreenPosition(Random.value);
            float halfWidth = width / 2;

            if(randomPosition.x < CamUtils().bounds.minX + halfWidth) {
                randomPosition += (Vector3.right * halfWidth);
            } else if(randomPosition.x > CamUtils().bounds.maxX - halfWidth) {
                randomPosition -= (Vector3.right * halfWidth);
            }

            return randomPosition;
        }

        Vector3 RandomizeScale() {
            float randomSize = Random.Range(2, 6);
            return new Vector3(randomSize, randomSize, 1);
        }

        public void SetSpawnTimer(float newTime) {
            timeBetweenSpawn = newTime;
        }

        IEnumerator SpawnDot() {
            count = count + 1;
            Dot newDot = ThemeManager.Instance.GetRandom();

            Vector3 newPosition = RandomizePosition(newDot.width);

            Dot spawnedDot = (Dot) Instantiate(newDot, newPosition, Quaternion.identity);
            spawnedDot.transform.localScale = RandomizeScale();
            spawnedDot.transform.parent = GameObject.Find("DynamicObjects").transform;
            spawnedDot.name = "DOT_" + count;

            yield return null;
        }
            
	}

}