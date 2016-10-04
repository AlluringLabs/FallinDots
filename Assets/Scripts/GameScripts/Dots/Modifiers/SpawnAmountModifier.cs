using UnityEngine;
using System.Collections;

namespace FallinDots.Dots.Modifiers {

    public class SpawnAmountModifier : IModifierInterface {

        public void ApplyModification(Spawner spawner) {
            // Increases the amount of dots that are spawned during
            // each spawn sequence by 1.
            spawner.dotsToSpawn = spawner.dotsToSpawn + 1;
        }

    }

}
