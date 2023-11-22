using UnityEngine.Serialization;

namespace ScriptableObjects
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "GameVariables", menuName = "Config/GameVariables", order = 1)]
    public class GameVariables : ScriptableObject
    {
        [Header("Button Spawn Settings")]
        [SerializeField] private int numberOfButtonsToSpawn = 1; // Number of buttons to spawn at once
        [SerializeField] private float spawnedButtons = 0; // Time between each spawn
        [SerializeField] private float spawnInterval = 2f; // Time between each spawn
        [SerializeField] private Vector4 margins;
        [Range(0, 1)] [SerializeField] private float badButtonProbability = 0.2f; 
        
        public int NumberOfButtonsToSpawn => numberOfButtonsToSpawn;
        public float SpawnedButtons
        {
            get => spawnedButtons;
            set => spawnedButtons = value;
        }

        public float SpawnInterval => spawnInterval;
        public Vector4 Margins => margins;
        public float BadButtonProbability => badButtonProbability;

        // Add other public properties to expose the fields
    }

}