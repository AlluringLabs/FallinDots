using UnityEngine;
using System.Collections;
using FallinDots.Generic;

namespace FallinDots {
	
	public class ThemeManager : BaseBehaviour {

        public static ThemeManager Instance;

        public Theme[] themes;

        public static int current;

        void Awake() {
            if(!Instance) {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            } else {
                Destroy(gameObject);
            }
        }

        public void setCurrentTheme(int newTheme) {
            current = newTheme;
        }

        [System.Serializable]
        public class Theme {
            public string themeName;
            public Dot[] themeStyles;
        }

        public Dot GetRandom() {
            int maxRange = themes[current].themeStyles.Length;
            int random = Random.Range(0, maxRange);
            Dot randomStyle = themes[current].themeStyles[random];
            return randomStyle;
        }

    }

}