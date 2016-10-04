using UnityEngine;
using System;
using System.Security.Cryptography;

namespace FallinDots.Generic.Utils {

    public static class RandomUtils {

        public static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        public static int randomSeed;

        public static uint getRandomScale() {
            uint scale = uint.MaxValue;

            while (scale == uint.MaxValue) {
                byte[] buffer = new byte[4];

                rng.GetBytes(buffer);
                scale = BitConverter.ToUInt32(buffer, 0);
            }

            return scale;
        }

        public static int RandomIntRange(int min, int max) {
            return (int)RandomFloatRange((float)min, (float)max);
        }

        public static float RandomFloat() {
            return RandomFloatRange(0f, 1f);
        }

        public static float RandomFloatRange(float min, float max) {
            uint scale = getRandomScale();
            return (float)(min + (max - min) * (scale / (double)uint.MaxValue));
        }

    }

}
