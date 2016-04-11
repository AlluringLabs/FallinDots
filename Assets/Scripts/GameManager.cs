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

    public float GetScreenMinX()
    {
        return GetScreenPosition(0.0f).x;
    }

    public float GetScreenMaxX()
    {
        return GetScreenPosition(1.0f).x;
    }

    public float GetScreenMinY()
    {
        return GetScreenPosition(0.0f, 0.0f).y;
    }

    public float GetScreenMaxY()
    {
        return GetScreenPosition(0.0f, 1.0f).y;
    }

    public Vector3 GetScreenPosition(float x)
    {
        return GetScreenPosition(x, 1.0f);
    }

    public Vector3 GetScreenPosition(float x, float y)
    {
        return mainCamera.ViewportToWorldPoint(new Vector3(x, y, mainCamera.nearClipPlane));
    }

    [System.Serializable]
    public class Theme
    {
        public Dot[] dotStyles;
    }
}
