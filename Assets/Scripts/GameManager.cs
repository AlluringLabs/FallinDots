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

    void Start() {
        Camera mainCam = Camera.main;
		camUtils = (CameraUtils) mainCam.GetComponent<CameraUtils>();
		camBounds = (CameraUtils.Bounds) camUtils.bounds;
		hasStarted = true;
    }

    [System.Serializable]
    public class Theme {
        public Dot[] dotStyles;
    }
}
