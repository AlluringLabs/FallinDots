using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{

    public float timeBetweenSpawn = 2f;
    public bool isDisabled;

    float nextSpawnTime;
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (gameManager.hasStarted)
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
    }

    // Generates a random Vec3 position for a newly created dot.
    Vector3 GenerateRandomPosition(float dotWidth)
    {
        Vector3 randomPos = gameManager.camUtils.GetScreenPosition(Random.value);
        float halfDotWidth = dotWidth / 2;

        if (randomPos.x < gameManager.camBounds.minX + halfDotWidth)
        {
            randomPos += (Vector3.right * halfDotWidth);
        }
        else if (randomPos.x > gameManager.camBounds.maxX - halfDotWidth)
        {
            randomPos -= (Vector3.right * halfDotWidth);
        }

        // Adding -2 to the y-axis so dots look like they are sliding in instead
        // of just spawning right on screen.  More fluid.
        return randomPos + (Vector3.up * 2);
    }

    // Creates the position and Instantiates a new dot.
    IEnumerator SpawnDot()
    {
        Dot newDot = GetRandomDotStyle();
        Vector3 newDotPos = GenerateRandomPosition(newDot.width);
        Dot spawnedDot = (Dot)Instantiate(newDot, newDotPos, Quaternion.identity);
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
