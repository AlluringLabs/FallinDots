using UnityEngine;
using System.Collections;
using FallinDots.Generic;
using FallinDots.Dots;

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
            public Sprite[] themeStyles;
        }

        public Sprite GetRandomSprite() {
            int maxRange = themes[current].themeStyles.Length;
            int random = Random.Range(0, maxRange);
            Sprite randomStyle = themes[current].themeStyles[random];
            return randomStyle;
        }

        public Dot GetRandom() {
            return new Dots.Dot();
        }

    }

}