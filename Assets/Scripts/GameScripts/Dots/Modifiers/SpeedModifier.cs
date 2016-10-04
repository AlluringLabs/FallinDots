using UnityEngine;
using System.Collections;

namespace FallinDots.Dots.Modifiers {

    public class SpeedModifier : IModifierInterface {

        public void ApplyModification(Spawner spawner) {
            // Raw, random values that will help us calculate the new timeChange.
            float timeChange = 5f / Random.Range(70, 100);
            float newTime = spawner.timeBetweenSpawn - timeChange;

            if (newTime > spawner.minTimeBetweenSpawn) {
                spawner.timeBetweenSpawn = spawner.timeBetweenSpawn - timeChange;
            }
            else {
                spawner.timeBetweenSpawn = spawner.minTimeBetweenSpawn;
            }
        }

    }

}
