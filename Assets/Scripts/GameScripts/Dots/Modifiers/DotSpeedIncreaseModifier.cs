using UnityEngine;
using System.Collections;

namespace FallinDots.Dots.Modifiers {

    public class DotSpeedIncreaseModifier : IModifierInterface {

        public void ApplyModification(Spawner spawner) {
            float dotSpeedModification = 5f / Random.Range(50, 80);
            spawner.dotSpeed = spawner.dotSpeed + dotSpeedModification;
        }

    }

}
