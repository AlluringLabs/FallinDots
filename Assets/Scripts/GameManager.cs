using UnityEngine;
// using System.Collections;

public class GameManager : MonoBehaviour {
	
	Camera mainCamera;
	public Spawner mainSpawnPoint;
	public Transform testSprite;
	
	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		Vector3 screenDimensions = GetScreenDimensions();
		testSprite.position = screenDimensions;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Vector3 GetScreenDimensions () {
		Vector3 screenDimens = new Vector3(Screen.width, Screen.height, 0);
		Vector3 levelDimens = mainCamera.ScreenToWorldPoint(screenDimens);
		return levelDimens;
	}
}
