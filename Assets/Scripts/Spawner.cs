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
    Vector3 GenerateRandomPosition()
    {
        Vector3 maxPos = gameManager.GetScreenDimensions();
        Debug.Log("Max Position X: " + maxPos.x);
        return new Vector3(0, 0, 1);
        //return new Vector3(Random.Range(0, maxPos.x), 0, 1);
    }

    // Creates the position and Instantiates a new dot.
    IEnumerator SpawnDot()
    {
        Dot newDot = GetRandomDotStyle();
        Dot spawnedDot = (Dot)Instantiate(newDot, GenerateRandomPosition(), Quaternion.identity);
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
