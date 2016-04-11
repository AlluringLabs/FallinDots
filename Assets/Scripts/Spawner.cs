using UnityEngine;

public class Spawner : MonoBehaviour
{

    public float timeBetweenSpawn = 2f;
    public Dot dot;
    public bool isDisabled;

    float nextSpawnTime;


    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        Debug.Log("update");
        //Debug.Log("next spawn time: " + nextSpawnTime);
        //Debug.Log("time: " + Time.time);

        // Just a basic spawn timer to spawn dots at the specified interval.
        if (!isDisabled)
        {
            if (Time.time > nextSpawnTime)
            {
                nextSpawnTime = Time.time + timeBetweenSpawn;
                SpawnDot();
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
    public void SpawnDot()
    {
        Instantiate(dot, GenerateRandomPosition(), Quaternion.identity);
    }

    // Sets a new spawn timer interval.
    public void SetNewSpawnTimer(float newTime)
    {
        timeBetweenSpawn = newTime;
    }
}
