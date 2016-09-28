using UnityEngine;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots.Dots {

    public class Spawner : BaseBehaviour {

        public GameObject basePrefab;

        public float minTimeBetweenSpawn = 0.1f;
        public float maxTimeBetweenSpawn = 5f;

        public float timeBetweenSpawn = 1f;
        public bool disabled = false;
        public int count = 0;

        float nextSpawnTime;
        float lastSpawnTimeUpdate;

        GameManager gameManager;

        void Start() {
            gameManager = GameManager.Instance;
            lastSpawnTimeUpdate = Time.time;
        }

        // Update is called once per frame
        void Update () {
            bool isTimeToSpawnDot = Time.time > nextSpawnTime;

            if (gameManager.isGameRunning() && isTimeToSpawnDot && !disabled) {
                nextSpawnTime = Time.time + timeBetweenSpawn;
                StartCoroutine(SpawnDot());
            }
        }

        void FixedUpdate() {
            if (gameManager.isGameRunning() && !disabled && isTimeToUpdateSpawnRate()) {
                // Raw, random values that will help us calculate the new timeChange.
                float timeChange = 5f / Random.Range(70, 100);
                float newTime = timeBetweenSpawn - timeChange;

                if (newTime > minTimeBetweenSpawn) {
                    timeBetweenSpawn = timeBetweenSpawn - timeChange;
                }
                else {
                    timeBetweenSpawn = minTimeBetweenSpawn;
                }

                lastSpawnTimeUpdate = Time.time;
            }
        }

        bool isTimeToUpdateSpawnRate() {
            return (Time.time - lastSpawnTimeUpdate) >= 4;
        }

        Vector3 RandomizePosition(Vector3 scale) {
            Vector3 randomPosition = CamUtils().GetScreenPosition(Random.value);
            float halfWidth = scale.y / 2;

            if (randomPosition.x < CamUtils().bounds.minX + halfWidth) {
                randomPosition += (Vector3.right * halfWidth);
            } else if (randomPosition.x > CamUtils().bounds.maxX - halfWidth) {
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
            Vector3 randomScale = RandomizeScale(1.0f, 1.5f);
            Vector3 newPosition = RandomizePosition(randomScale);

            GameObject dot = (GameObject)Instantiate(basePrefab, newPosition, Quaternion.identity);

            // Add components to the dot
            dot.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
            dot.gameObject.GetComponent<Dot>().width = 0.5f;

            // Set scale and parent/tags
            dot.transform.localScale = randomScale;
            dot.transform.parent = GameObject.Find("DynamicObjects").transform;
            dot.transform.tag = "Dot";

            yield return new WaitForEndOfFrame();
        }

    }

}
