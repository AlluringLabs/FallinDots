﻿using UnityEngine;
using System.Collections;
using System;
using FallinDots.Generic;

namespace FallinDots {

    public class InputManager : BaseBehaviour {

        //public delegate void DotTapped(GameObject target);
        //public event DotTapped dotTapped;

        public delegate void PauseToggle();
        public event PauseToggle pauseToggleEvent;

        void Update() {
            handleMobileInput();
            handleEditorInput();
        }

        // Handles the input of mobile devices based on RuntimePlatform
        private void handleMobileInput() {
            if(Application.isMobilePlatform) {
                for (int i = 0; i < Input.touchCount; i++) {
                    if(Input.GetTouch(i).phase == TouchPhase.Began)
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        Debug.DrawRay(ray.origin, ray.direction, Color.red);
                        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                        handleRaycastHit(hit);
                    }
                }
            }
        }

        private void handleRaycastHit(RaycastHit2D hit) {
            // WE did hit something, now figure out what it was
            if (hit.collider != null)
            {

                // First, check if the object is a Dot
                if (hit.collider.gameObject.tag == "Dot")
                {
                    if (!GameManager.Instance.paused)
                    {
                        Dots.Dot hitDot = hit.collider.gameObject.GetComponent<Dots.Dot>();
                        hitDot.destroy();
                    }
                }

            }
        }

        // Same as above... except for Testing purposes within Unity..
        private void handleEditorInput() {
            if(Application.isEditor) {
                // Toggle the pause event
                if(Input.GetKeyDown(KeyCode.Escape)) {
                    pauseToggleEvent();
                }

                // Fun idea
                if(Input.GetKeyDown(KeyCode.A)) {
                    Time.timeScale = Time.timeScale + 1;
                }

                if(Input.GetKeyDown(KeyCode.W)) {
                    Time.timeScale = 1;
                }

                // We clicked something
                if(Input.GetMouseButtonDown(0)) {

                    // Create a Ray
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

                    handleRaycastHit(hit);
                }
            }
        }

        public void HandleTogglePause() {
            pauseToggleEvent();
        }

    }

}