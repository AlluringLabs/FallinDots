using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public float timeBetweenSpawn = 2f;
    public Dot dot;
    public bool isDisabled;

    float nextSpawnTime;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        // Just a basic spawn timer to spawn dots at the specified interval.
        if (!isDisabled)
        {
            if (Time.time > nextSpawnTime)
            {
                nextSpawnTime = Time.time + timeBetweenSpawn;
                StartCoroutine(SpawnDot());
            }
        }
    }

    // Generates a random Vec3 position for a newly created dot.
    Vector3 GenerateRandomPosition(float dotWidth)
    {
        Vector3 randomPos = gameManager.GetScreenPosition(Random.value);
        float halfDotWidth = dotWidth/2;

        if (randomPos.x < gameManager.GetScreenMinX() + halfDotWidth) {
            randomPos += (Vector3.right * halfDotWidth);
        }
        else if (randomPos.x > gameManager.GetScreenMaxX() - halfDotWidth) {
            randomPos -= (Vector3.right * halfDotWidth);
        }

        return randomPos;
    }

    // Creates the position and Instantiates a new dot.
    IEnumerator SpawnDot()
    {
        Dot newDot = GetRandomDotStyle();
        Dot spawnedDot = (Dot)Instantiate(newDot, GenerateRandomPosition(newDot.width), Quaternion.identity);
        spawnedDot.transform.parent = this.transform;
        spawnedDot.name = "Dot";

        yield return null;
    }

    // Gets a random dot style from the current theme set in GameManager. Kind of shitty for now, but it works.
    public Dot GetRandomDotStyle()
    {
        int maxRange = gameManager.themes[gameManager.currentTheme].dotStyles.Length;
        int random = Random.Range(0, maxRange);
        Dot randomDot = gameManager.themes[gameManager.currentTheme].dotStyles[random]; 
        return randomDot;
    }

    // Sets a new spawn timer interval.
    public void SetNewSpawnTimer(float newTime)
    {
        timeBetweenSpawn = newTime;
    }
}
