using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace AstraNope.Data.Creatures
{
    [Serializable]
    [MovedFrom(true, sourceClassName: "RaySpeciesDefinition")]
    public sealed class CreatureSpeciesDefinition
    {
        [SerializeField] private string displayName = "Creature";
        [SerializeField] private int prefabId = 2100;
        [SerializeField] private GameObject model;
        [Min(0.01f), SerializeField] private float modelScale = 1f;
        [Min(0f), SerializeField] private float cruiseSpeed = 2f;
        [Min(0f), SerializeField] private float turnSpeedDegrees = 75f;
        [Min(0f), SerializeField] private float wanderRadius = 24f;
        [Min(0f), SerializeField] private float verticalRadius = 7f;
        [Min(0f), SerializeField] private float fleeDistance = 8f;
        [Range(0f, 45f), SerializeField] private float maximumBankDegrees = 18f;
        [Min(0f), SerializeField] private float bankResponsiveness = 4f;
        [SerializeField] private Vector3 spawnCenter;
        [SerializeField] private Vector3 spawnVolume = new Vector3(8f, 3f, 8f);
        [Min(0), SerializeField] private int maximumAlive = 6;
        [Min(1), SerializeField] private int spawnPerTick = 2;
        [Min(0.01f), SerializeField] private float spawnInterval = 8f;
        [Header("Taming")]
        [Tooltip("How much affinity this species needs before it can be tamed. Its taming threshold.")]
        [Min(1f), SerializeField] private float maximumAffinity = 100f;
        [Tooltip("Affinity gained per successful feed at matching food tier.")]
        [Min(0.1f), SerializeField] private float affinityPerFeed = 25f;
        [Tooltip("Guaranteed tame success on this attempt number.")]
        [Range(1, 8), SerializeField] private int guaranteedAfterAttempts = 4;
        [Min(0f), SerializeField] private float feedCooldownSeconds = 2f;

        public string DisplayName => displayName;
        public int PrefabId => prefabId;
        public GameObject Model => model;
        public float ModelScale => modelScale;
        public float CruiseSpeed => cruiseSpeed;
        public float TurnSpeedDegrees => turnSpeedDegrees;
        public float WanderRadius => wanderRadius;
        public float VerticalRadius => verticalRadius;
        public float FleeDistance => fleeDistance;
        public float MaximumBankDegrees => maximumBankDegrees;
        public float BankResponsiveness => bankResponsiveness;
        public Vector3 SpawnCenter => spawnCenter;
        public Vector3 SpawnVolume => spawnVolume;
        public int MaximumAlive => maximumAlive;
        public int SpawnPerTick => spawnPerTick;
        public float SpawnInterval => spawnInterval;
        public float MaximumAffinity => maximumAffinity;
        public float AffinityPerFeed => affinityPerFeed;
        public int GuaranteedAfterAttempts => guaranteedAfterAttempts;
        public float FeedCooldownSeconds => feedCooldownSeconds;

#if UNITY_EDITOR
        public void Configure(string name, int id, GameObject sourceModel, Vector3 center,
            float speed, float scale)
        {
            displayName = name;
            prefabId = id;
            model = sourceModel;
            spawnCenter = center;
            cruiseSpeed = Mathf.Max(0f, speed);
            modelScale = Mathf.Max(0.01f, scale);
        }

        public void ConfigureTaming(float maxAffinity, float perFeed, int guaranteedAttempts)
        {
            maximumAffinity = Mathf.Max(1f, maxAffinity);
            affinityPerFeed = Mathf.Max(0.1f, perFeed);
            guaranteedAfterAttempts = Mathf.Clamp(guaranteedAttempts, 1, 8);
        }
#endif
    }

    [MovedFrom(true, sourceClassName: "RaySpeciesCatalog")]
    [CreateAssetMenu(menuName = "Survival/Creatures/Creature Species Catalog", fileName = "CreatureSpeciesCatalog")]
    public sealed class CreatureSpeciesCatalog : ScriptableObject
    {
        [SerializeField] private List<CreatureSpeciesDefinition> species = new List<CreatureSpeciesDefinition>();

        public IReadOnlyList<CreatureSpeciesDefinition> Species => species;

#if UNITY_EDITOR
        public List<CreatureSpeciesDefinition> MutableSpecies => species;
#endif
    }
}
