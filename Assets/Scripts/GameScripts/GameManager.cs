using System;
using UnityEngine;
using FallinDots.Generic;

namespace FallinDots {

	public class GameManager : BaseBehaviour
	{

        public bool paused = false;
        public bool started = false;
        public bool ended = false;

		// Camera Stuffs
		public CameraUtils camUtils;
		public CameraUtils.Bounds camBounds;

		public int currentTheme;

		void Start() {
			started = true;
		}

		void Update() {
			// Kind of shit for now, but eventually we'll need something like this for mobile
			// pause menu of sorts.
			if (Input.GetKeyDown(KeyCode.Escape))
			{
				TogglePause();
			}
        }

		// Just toggles the pause state of the game. This should probably include something like
		// fading a menu in/out as well.
		private void TogglePause() {
            // NOTE: Probably not the best way to handle pausing. Should have own "Time" class
            // that might handle this on it's own.

            // TODO: Make the Pause menu show up or something and do something "cool"
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