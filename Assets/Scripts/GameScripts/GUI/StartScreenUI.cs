using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots.GUI {
	
	public class StartScreenUI : BaseBehaviour {

		public void GameSceneStart()
		{
			SceneManager.LoadScene("_GAME_");
		}

	}

}