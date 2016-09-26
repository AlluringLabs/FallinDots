using System;
using System.Collections;
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

        public GameObject pauseMenu;
        public GameObject overlayCanvas;
        public GameObject scoreOverlay;

        public InputManager input;

        public Text score;

        void Awake() {
            if (!Instance) {
                Instance = this;
            } else {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            this.gameObject.AddComponent<InputManager>();

            started = true;
            paused = false;

            pauseMenu.gameObject.SetActive(false);
            //pauseMenu.GetComponent<CanvasGroup>().alpha = 0;
            GetComponent<InputManager>().pauseToggleEvent += TogglePause;
        }

        void Update() {

        }

        public void updateScore() {
            playerScore = playerScore + 1;
            //score.text = playerScore.ToString();
        }

        public bool isGameRunning() {
            return started && !paused;
        }

        private void OnDotDestroyed() {
            playerScore++;
            //score.text = playerScore.ToString();
        }

        // Just toggles the pause state of the game. This should probably include something like
        // fading a menu in/out as well.
        public void TogglePause() {
            paused = !paused;
            StartCoroutine(TogglePauseMenu());
        }

        IEnumerator TogglePauseMenu() {
            Debug.Log("toggle pause");
            CanvasGroup image = pauseMenu.GetComponent<CanvasGroup>();

            if (paused) {
                Time.timeScale = 0;
                image.gameObject.SetActive(true);
            } else {
                Time.timeScale = 1;
                image.gameObject.SetActive(false);
            }

            yield return new WaitForEndOfFrame();

            //while(image.alpha <= 1f) {
            //    if (paused) {
            //        image.alpha = image.alpha + (Time.deltaTime / 0.5f);
            //    } else {
            //        image.alpha = image.alpha - (Time.deltaTime / 0.5f);
            //    }

            //    yield return new WaitForEndOfFrame();
            //}

            ////if(paused) {
            ////    image.gameObject.SetActive(true);
            ////    image.CrossFadeAlpha(0, 0, true);
            ////    image.CrossFadeAlpha(1, 0.4f, true);
            ////    Debug.Log("Fade in");
            ////} else {
            ////    image.CrossFadeAlpha(0, 0.3f, true);
            ////    Debug.Log("FAde out");
            ////    yield return new WaitForSeconds(0.3f);
            ////    Debug.Log("Deactivate");
            ////    image.gameObject.SetActive(false);
            ////}
        }

    }

}
