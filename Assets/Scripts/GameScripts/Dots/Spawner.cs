using UnityEngine;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots.Dots {

	public class Spawner : BaseBehaviour {

        public float minTimeBetweenSpawn = 0.0f;
        public float maxTimeBetweenSpawn = 5f;

		public float timeBetweenSpawn = 1f;
		public bool disabled = false;
        public int count = 0;

		float nextSpawnTime;
        float lastSpawnTimeUpdate;
		
		// Update is called once per frame
		void Update () {

            // Has the game started? Is the game paused? Is this spawn point disabled?
            if(GameManager.Instance.started && !GameManager.Instance.paused && !disabled) {
                if (Time.time > nextSpawnTime) {
                    nextSpawnTime = Time.time + timeBetweenSpawn;
                    StartCoroutine(SpawnDot());
                }
            }
		}

        void FixedUpdate()
        {
            if(GameManager.Instance.started && !GameManager.Instance.paused && !disabled)
            {
                // This doesn't work, but there needs to be a way to "gradually" increase the speed.
                // timeBetweenSpawn = Mathf.Lerp(minTimeBetweenSpawn, maxTimeBetweenSpawn, Time.fixedDeltaTime * 2);
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

            Sprite sprite = ThemeManager.Instance.GetRandomSprite();
            Vector3 randomScale = RandomizeScale(1.0f, 3.0f);
            Vector3 newPosition = RandomizePosition(randomScale);
            GameObject obj = new GameObject();
            obj.transform.parent = GameObject.Find("DynamicObjects").transform;

            GameObject dot = (GameObject)Instantiate(obj, newPosition, Quaternion.identity);

            // Add components to the dot
            dot.gameObject.AddComponent<SpriteRenderer>().sprite = sprite;
            dot.gameObject.AddComponent<CircleCollider2D>();
            dot.gameObject.AddComponent<Dot>().width = 0.5f;
            
            // Set scale and parent/tags
            dot.transform.localScale = randomScale;
            dot.transform.parent = GameObject.Find("DynamicObjects").transform;
            dot.transform.tag = "Dot";

            yield return new WaitForEndOfFrame();
        }

	}

}