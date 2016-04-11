using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Theme[] themes;
    public int currentTheme;
	
	// Set the default level in the editor so we can test different levels without playing the game.
	public float currentLevel = 1;

    Camera mainCamera;
    public Spawner mainSpawnPoint;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {

    }

    public float GetScreenMaxX()
    {
        return GetScreenPosition(1.0f).x;
    }

    public float GetScreenMinX()
    {
        return GetScreenPosition(0.0f).x;
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
