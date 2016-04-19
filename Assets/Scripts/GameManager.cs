using UnityEngine;

public class GameManager : MonoBehaviour
{

    // Camera Stuffs
    public CameraUtils camUtils;
    public CameraUtils.Bounds camBounds;

    public Theme[] themes;
    public int currentTheme;

    // Set the default level in the editor so we can test different levels without playing the game.
    public float currentLevel = 1;
    public Spawner mainSpawnPoint;

    public bool isPaused = false;
    public bool hasStarted = false;
    public bool hasEnded = false;

    void Start()
    {
        Camera mainCam = Camera.main;
        camUtils = (CameraUtils)mainCam.GetComponent<CameraUtils>();
        camBounds = (CameraUtils.Bounds)camUtils.bounds;
        hasStarted = true;
    }

    void Update()
    {
        // Kind of shit for now, but eventually we'll need something like this for mobile
        // pause menu of sorts.
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        // Just for testing and fun purposes...
        // this could even be a powerup
        if (Input.GetMouseButton(1))
        {
            Time.timeScale = 0.5f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Time.timeScale = 1;
        }
    }

    // Just toggles the pause state of the game. This should probably include something like
    // fading a menu in/out as well.
    private void TogglePause()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0;
        }
    }

    [System.Serializable]
    public class Theme
    {
        public Dot[] dotStyles;
    }
}
