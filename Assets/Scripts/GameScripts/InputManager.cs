using UnityEngine;
using System.Collections;
using System;
using FallinDots.Generic;

namespace FallinDots {

    public class InputManager : BaseBehaviour {

        public delegate void DotTapped(Dots.Dot target, int byUser);
        public event DotTapped dotTapped;

        void Update() {
            handleMobileInput();
            handleEditorInput();
        }

        // Handles the input of mobile devices based on RuntimePlatform
        private void handleMobileInput() {
            if(Application.isMobilePlatform) {
                Debug.Log("yes");
            }
        }

        // Same as above... except for Testing purposes within Unity..
        private void handleEditorInput() {
            if(Application.isEditor) {
                Debug.Log("yes");
            }
        }

    }

}