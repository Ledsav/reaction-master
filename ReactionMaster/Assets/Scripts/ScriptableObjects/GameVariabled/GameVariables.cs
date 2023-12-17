using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameVariables", menuName = "Config/GameVariables", order = 1)]
    public class GameVariables : ScriptableObject
    {
        private const int MaxLeaderboardEntries = 50;

        [Header("Button Spawn Settings")] [SerializeField]
        private int numberOfButtonsToSpawn = 1; // Number of buttons to spawn at once

        [SerializeField] private float spawnedButtons; // Time between each spawn
        [SerializeField] private float spawnInterval = 2f; // Time between each spawn
        [SerializeField] private Vector4 margins;
        [Range(0, 1)] [SerializeField] private float badButtonProbability = 0.2f;


        [Header("Points Settings")] [SerializeField]
        private int points;

        [SerializeField] private List<int> leaderboard = new();

        public int NumberOfButtonsToSpawn => numberOfButtonsToSpawn;

        public DateTime LastClickTimeStamp { get; set; }

        public DateTime SpawnTimeStamp { get; set; }

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

        public List<int> Leaderboard
        {
            get => leaderboard;
            set => leaderboard = value;
        }

        private List<float> ReactionTimes { get; } = new();

        private void OnEnable()
        {
            LoadLeaderboard();
        }

        public void ResetGame()
        {
            points = 0;
            spawnedButtons = 0;
            ReactionTimes.Clear();
        }

        public int GetPlaceInLeaderboard(int points)
        {
            var leaderboard = Leaderboard;
            var index = leaderboard.FindIndex(x => x < points);
            return index == -1 ? leaderboard.Count : index;
        }

        public void AddToLeaderboard(int newScore)
        {
            leaderboard.Add(newScore);
            leaderboard.Sort((a, b) => b.CompareTo(a));
            if (leaderboard.Count > MaxLeaderboardEntries)
                leaderboard.RemoveRange(MaxLeaderboardEntries, leaderboard.Count - MaxLeaderboardEntries);
            SaveLeaderboard();
        }

        private void SaveLeaderboard()
        {
            var json = JsonUtility.ToJson(new SerializableList<int> { items = leaderboard });
            PlayerPrefs.SetString("leaderboard", json);
            PlayerPrefs.Save();
        }

        private void LoadLeaderboard()
        {
            if (PlayerPrefs.HasKey("leaderboard"))
            {
                var json = PlayerPrefs.GetString("leaderboard");
                var loadedData = JsonUtility.FromJson<SerializableList<int>>(json);
                leaderboard = loadedData.items;
            }
        }

        public void UpdateLastClickTimeStamp()
        {
            LastClickTimeStamp = GetCurrentTimestamp();
        }

        public void UpdateSpawnTimeStamp()
        {
            SpawnTimeStamp = GetCurrentTimestamp();
        }

        private DateTime GetCurrentTimestamp()
        {
            return DateTime.Now;
        }

        public void AddReactionTime(float reactionTime)
        {
            ReactionTimes.Add(reactionTime);
        }

        public float getAverageReactionTime()
        {
            var sum = ReactionTimes.Sum();
            var average = sum / ReactionTimes.Count;
            return average;
        }

        [Serializable]
        private class SerializableList<T>
        {
            public List<T> items;
        }
    }
}