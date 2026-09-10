using System;
using UnityEngine;
using WorldBuilder.Entities.Creatures;

namespace AstraNope.Data.Items
{
    [Serializable]
    public struct TameFoodDefinition
    {
        [Tooltip("Item id of the tame food.")]
        public int itemId;
        [Tooltip("Creature grade this food is tuned for. Feeding a creature above this tier still works, but barely.")]
        public CreatureGrade tier;
    }

    [CreateAssetMenu(menuName = "Survival/Creatures/Tame Food Catalog", fileName = "TameFoodCatalog")]
    public sealed class CreatureTameFoodCatalog : ScriptableObject
    {
        [SerializeField] private TameFoodDefinition[] tameFoods = Array.Empty<TameFoodDefinition>();

        public int TameFoodCount => tameFoods?.Length ?? 0;
        public TameFoodDefinition GetTameFood(int index) => tameFoods[index];

        public bool TryGetTameFood(int itemId, out TameFoodDefinition definition)
        {
            TameFoodDefinition[] entries = tameFoods ?? Array.Empty<TameFoodDefinition>();
            for (int i = 0; i < entries.Length; i++)
            {
                if (entries[i].itemId != itemId) continue;
                definition = entries[i];
                return true;
            }
            definition = default;
            return false;
        }

        public bool IsTameFood(int itemId) => TryGetTameFood(itemId, out _);
    }
}
