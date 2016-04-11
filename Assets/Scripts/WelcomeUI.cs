using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class WelcomeUI : MonoBehaviour {
	
	public void GameSceneStart() {
		SceneManager.LoadScene("_GAME_");
	}
	
}
