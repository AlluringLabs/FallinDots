using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using FallinDots.Generic;
using FallinDots.Generic.Utils;
using FallinDots.Dots.Modifiers;

namespace FallinDots.Dots {

    public class Spawner : BaseBehaviour {

        public GameObject basePrefab;

        public float minTimeBetweenSpawn = 0.1f;
        public float maxTimeBetweenSpawn = 5f;

        public float timeBetweenSpawn = 1f;
        public bool disabled = false;
        public int count = 0;
        public int dotsToSpawn = 1;

        // Dot Specific, Adjustable Params
        public float dotSpeed = 3f;

        float nextSpawnTime;
        float lastSpawnTimeUpdate;

        GameManager gameManager;
        DifficultyModifier difficultyModifier;
        List<IModifierInterface> spawnerModifiers = new List<IModifierInterface> {
            new SpeedModifier(),
            new SpawnAmountModifier(),
            new DotSpeedIncreaseModifier()
        };

        void Start() {
            gameManager = GameManager.Instance;
            lastSpawnTimeUpdate = Time.time;

            difficultyModifier = new DifficultyModifier(this, spawnerModifiers);
        }

        void Update() {
            bool isTimeToSpawnDot = Time.time > nextSpawnTime;

            if (gameManager.isGameRunning() && isTimeToSpawnDot && !disabled) {
                nextSpawnTime = Time.time + timeBetweenSpawn;
                StartCoroutine(SpawnDot());
            }
        }

        void FixedUpdate() {
            if (gameManager.isGameRunning() && !disabled && isTimeToUpdateSpawnRate()) {
                difficultyModifier.ApplyRandomModifier();
                lastSpawnTimeUpdate = Time.time;
            }
        }

        bool isTimeToUpdateSpawnRate() {
            return (Time.time - lastSpawnTimeUpdate) >= 4;
        }

        Vector3 RandomizePosition(Vector3 scale) {
            float randomFloat = RandomUtils.RandomFloatRange(0f, 1f);
            Vector3 randomPosition = CamUtils().GetScreenPosition(randomFloat);
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
            float randomSize = RandomUtils.RandomFloatRange(width, maxWidth);
            return new Vector3(randomSize, randomSize, 1);
        }

        public void SetSpawnTimer(float newTime) {
            timeBetweenSpawn = newTime;
        }

        IEnumerator SpawnDot() {
            RandomUtils.RandomIntRange(0, 150);

            for (int i=1; i<=dotsToSpawn; i++) {
                count = count + 1;

                Sprite sprite = ThemeManager.Instance.GetRandomSprite();
                Vector3 randomScale = RandomizeScale(1.0f, 1.5f);
                Vector3 newPosition = RandomizePosition(randomScale);

                GameObject dot = (GameObject) Instantiate(basePrefab,
                                                          newPosition,
                                                          Quaternion.identity);

                // Add components to the dot
                dot.gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
                dot.gameObject.GetComponent<Dot>().width = 0.5f;
                dot.gameObject.GetComponent<Dot>().speed = dotSpeed;

                // Set scale and parent/tags
                dot.transform.localScale = randomScale;
                dot.transform.parent = GameObject.Find("DynamicObjects").transform;
                dot.transform.tag = "Dot";
            }

            yield return new WaitForEndOfFrame();
        }

    }

}
