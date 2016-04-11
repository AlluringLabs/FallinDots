using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int msBetweenSpawn = 100;
    public Dot dot;

    GameManager gameManager;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {

    }

    public void SpawnDot()
    {
        Vector3 pos = gameManager.GetScreenDimensions();

        Instantiate(dot, pos, Quaternion.identity);
    }

    public void SetNewSpawnTimer(int spawnTime)
    {

    }
}
