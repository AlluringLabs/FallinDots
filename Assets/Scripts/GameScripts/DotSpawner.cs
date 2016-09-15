﻿using UnityEngine;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots {

	public class DotSpawner : BaseBehaviour {

		GameManager gameManager;

		public float timeBetweenSpawn = 2;
		public bool disabled = false;

		float nextSpawn;

		// Use this for initialization
		void Start () {
			gameManager = FindObjectOfType<GameManager> ();
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}

}