using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameUI : MonoBehaviour {
	
	public Text scoreText;
	public Text lostText;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void UpdateScore(int newScoreText) {
		scoreText.text += newScoreText;
	}
	
	public void UpdateLost() {
		
	}
	
}
