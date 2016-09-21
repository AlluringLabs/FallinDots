using System;
using UnityEngine;
using UnityEngine.UI;
using FallinDots.Generic;

namespace FallinDots {

	public class GameManager : BaseBehaviour
	{

        public static GameManager Instance;

        public bool paused = false;
        public bool started = false;
        public bool ended = false;

        public int currentLevel = 0;

        public int playerScore = 0;
        public int playerLose = 0;

        public Canvas pauseMenu;
        public Canvas overlayCanvas;
        public Canvas scoreOverlay;

        public Text score;

		void Start() {
			started = true;
            paused = false;

            pauseMenu.gameObject.SetActive(false);
            scoreOverlay.gameObject.SetActive(true);
            FindObjectOfType<InputManager>().pauseToggleEvent += TogglePause;
		}

        void Awake() {
            if(!Instance) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

		void Update() {
            
        }

        public void updateScore() {
            playerScore = playerScore + 1;
            score.text = playerScore.ToString();
        }

        private void OnDotDestroyed() {
            playerScore++;
            score.text = playerScore.ToString();
        }

		// Just toggles the pause state of the game. This should probably include something like
		// fading a menu in/out as well.
		private void TogglePause() {
            if(paused) {
                paused = false;
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
            } else {
                paused = true;
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
		}

	}

}