using UnityEngine;

public class GameManager : MonoBehaviour
{

    Camera mainCamera;
    public Spawner mainSpawnPoint;
    public Transform testSprite;

    void Start()
    {
        mainCamera = Camera.main;
        Vector3 screenDimensions = GetScreenDimensions();
    }

    void Update()
    {
        // Absolutely shit, but it serves its purpose for the time being.
        //
        // Idea: Have either the GameManager, Level or Spawner contain some
        //       sort of value for how long, in time, the game should wait
        //       until it spawns another dot.  This way we could have far
        //       more control over the spawning pattern/time in-between
        //       dots.
        // if (Random.value > .95) {
        //     mainSpawnPoint.SpawnDot();
        // }
    }

    public Vector3 GetScreenDimensions()
    {
        Vector3 screenDimens = new Vector3(Screen.width, Screen.height, 1);
        Vector3 levelDimens = mainCamera.ScreenToWorldPoint(screenDimens);
        return levelDimens;
    }
}
