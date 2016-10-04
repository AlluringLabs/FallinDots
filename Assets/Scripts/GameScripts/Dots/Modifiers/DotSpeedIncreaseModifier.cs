using UnityEngine;
using System.Collections;
using FallinDots.Generic.Utils;

namespace FallinDots.Dots.Modifiers {

    public class DotSpeedIncreaseModifier : IModifierInterface {

        public void ApplyModification(Spawner spawner) {
            float dotSpeedModification = 5f / RandomUtils.RandomIntRange(50, 80);
            spawner.dotSpeed = spawner.dotSpeed + dotSpeedModification;
        }

    }

}
