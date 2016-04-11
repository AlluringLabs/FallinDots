using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {
	
	public int currentLevel;
	public float levelDifficulty;
	
	void Start () {
		currentLevel = 0;
		levelDifficulty = 1;
	}
	
	public void Restart () {
		currentLevel = 0;
		levelDifficulty = 1;
	}
	
	public void UpdateLevelDifficulty(int level) {
		currentLevel = level;
		levelDifficulty++;
	}
	
}
