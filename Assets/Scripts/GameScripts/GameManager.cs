using System;
using UnityEngine;
using TouchScript.Gestures;
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

		void Start() {
			started = true;
            paused = false;
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
            
            // For testing on Desktop and desktop controls
            if(Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXEditor) {

                // Shit for now
                if (Input.GetKeyDown(KeyCode.Escape)) {
                    TogglePause();
                }

                // Fun idea
                if(Input.GetKeyDown(KeyCode.A)) {
                    Time.timeScale = Time.timeScale + 1;
                }
            }

            // Just some basic Input code for Dots...
            // TODO: move this to somewhere else...
            if(Input.GetMouseButtonDown(0)) {
                Debug.Log(Input.mousePosition);
                Debug.Log(Vector2.zero);

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                if(hit.collider != null) {
                    if(hit.collider.gameObject.tag == "Dot") {
                        Destroy(hit.collider.gameObject);
                    }
                }
            }

            if(Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android) {
                // Implement
//                for (int i = 0; i < Input.touchCount; i++) {
//                    if(Input.GetTouch(i).phase == TouchPhase.Began) {
//                        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.GetTouch(i).position, Vector2.zero));
//                        if(hit.collider != null && hit.transform.gameObject.tag == "dot") {
//                            Destroy(hit.transform.gameObject);
//                        }
//                    }
//                }
            }
        }

		// Just toggles the pause state of the game. This should probably include something like
		// fading a menu in/out as well.
		private void TogglePause() {
            if(paused) {
                paused = false;
                Time.timeScale = 1;
            } else {
                paused = true;
                Time.timeScale = 0;
            }
		}

	}

}