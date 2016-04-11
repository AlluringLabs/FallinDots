using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Theme[] themes;
    public int currentTheme;

    Camera mainCamera;
    public Spawner mainSpawnPoint;

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
        // TODO: Move this method to be private and update the screenDimensions
        // only when it needs to be updated (i.e: screen resize event).
        // That way we don't need to worry about generating the screen size
        // every single time we need to access it.
        Vector3 screenDimens = new Vector3(Screen.width, Screen.height, 1);
        return mainCamera.ScreenToWorldPoint(screenDimens);
    }

    [System.Serializable]
    public class Theme
    {
        public Dot[] dotStyles;
    }
}
