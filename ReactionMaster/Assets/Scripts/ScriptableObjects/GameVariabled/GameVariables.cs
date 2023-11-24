using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameVariables", menuName = "Config/GameVariables", order = 1)]
    public class GameVariables : ScriptableObject
    {
        [Header("Button Spawn Settings")] [SerializeField]
        private int numberOfButtonsToSpawn = 1; // Number of buttons to spawn at once

        [SerializeField] private float spawnedButtons; // Time between each spawn
        [SerializeField] private float spawnInterval = 2f; // Time between each spawn
        [SerializeField] private Vector4 margins;
        [Range(0, 1)] [SerializeField] private float badButtonProbability = 0.2f;

        [Header("Points Settings")] [SerializeField]
        private int points;

        public int NumberOfButtonsToSpawn => numberOfButtonsToSpawn;

        public float SpawnedButtons
        {
            get => spawnedButtons;
            set => spawnedButtons = value;
        }

        public float SpawnInterval => spawnInterval;
        public Vector4 Margins => margins;
        public float BadButtonProbability => badButtonProbability;

        public int Points
        {
            get => points;
            set => points = value;
        }

        public void ResetGame()
        {
            points = 0;
            spawnedButtons = 0;
        }
    }
}