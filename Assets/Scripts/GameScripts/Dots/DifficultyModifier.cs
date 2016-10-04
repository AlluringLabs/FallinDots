using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FallinDots.Dots.Modifiers;

namespace FallinDots.Dots {

    public class DifficultyModifier {

        public Spawner spawner;
        public Dictionary<string, IModifierInterface> modifiers;

        public DifficultyModifier(Spawner theSpawner, List<IModifierInterface> initialModifiers) {
            spawner = theSpawner;
            modifiers = new Dictionary<string, IModifierInterface>();

            foreach (IModifierInterface modifier in initialModifiers) {
                AddModifier(modifier);
            }
        }

        public void RemoveModifier(IModifierInterface modifier) {
            string modifierName = modifier.GetType().Name;

            if (modifiers.ContainsKey(modifierName)) {
                modifiers.Remove(modifierName);
            }
        }

        public void AddModifier(IModifierInterface modifier) {
            string modifierName = modifier.GetType().Name;

            // Only add the modifier to our dictionary of modifiers if it doesn't
            // already exist.
            if (!modifiers.ContainsKey(modifierName)) {
                modifiers.Add(modifierName, modifier);
            }
        }

        public void ApplyRandomModifier() {
            List<string> randomModifiers = Enumerable.ToList(modifiers.Keys);

            int randomModifierInd = (int) Mathf.Floor(Random.Range(0, randomModifiers.Count));
            string randomModifierName = randomModifiers[randomModifierInd];

            IModifierInterface modifier = modifiers[randomModifierName];

            modifier.ApplyModification(spawner);
        }

    }

}
