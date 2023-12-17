using System;
using System.Collections;
using PlayModeLogic;
using ScriptableObjects;
using UnityEngine;
using Utils;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;

        [Header("References")] [SerializeField]
        public GameVariables gameVariables;

        [SerializeField] public UiManager uiManager;
        [SerializeField] public AnimationManager animationManager;
        [SerializeField] public ObjectSpawner objectSpawner;

        [SerializeField] private GameState currentState;

        private float _timer;

        public static GameManager Instance
        {
            get
            {
                if (_instance == null) _instance = new GameObject("GameManager").AddComponent<GameManager>();
                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            ChangeState(GameState.Menu);
        }

        private void InitializeVariables()
        {
            _timer = Instance.gameVariables.NumberOfButtonsToSpawn *
                     Instance.gameVariables.SpawnInterval;
            Instance.uiManager.UpdateTimeLeftText(_timer);
        }


        private void ChangeState(GameState newState)
        {
            StopAllCoroutines();

            currentState = newState;
            switch (currentState)
            {
                case GameState.Menu:
                    StartCoroutine(MenuState());
                    break;
                case GameState.PlayMode:
                    StartCoroutine(PlayModeState());
                    break;
                case GameState.GameOver:
                    StartCoroutine(GameOverState());
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // Example Game States
        private IEnumerator MenuState()
        {
            while (currentState == GameState.Menu)
                yield return null;
        }

        private IEnumerator PlayModeState()
        {
            animationManager.PlayEnterGameAnimation();
            yield return new WaitForSeconds(2f); // Wait for the animation to finish

            while (currentState == GameState.PlayMode)
            {
                Instance.objectSpawner.RecycleButton();
                Instance.gameVariables.SpawnedButtons++;
                yield return new WaitForSeconds(Instance.gameVariables.SpawnInterval);

                if (Instance.gameVariables.SpawnTimeStamp - Instance.gameVariables.LastClickTimeStamp <=
                    TimeSpan.FromSeconds(Instance.gameVariables.SpawnInterval))
                    Instance.gameVariables.AddReactionTime(
                        (float)(Instance.gameVariables.LastClickTimeStamp - Instance.gameVariables.SpawnTimeStamp)
                        .TotalSeconds);

                _timer -= Instance.gameVariables.SpawnInterval;
                Instance.uiManager.UpdateTimeLeftText(_timer);

                // checking end game condition
                if (Instance.gameVariables.SpawnedButtons < Instance.gameVariables.NumberOfButtonsToSpawn) continue;

                Instance.gameVariables.AddToLeaderboard(Instance.gameVariables.Points);

                // setting end game
                OnGameOver();
            }
        }

        private IEnumerator GameOverState()
        {
            animationManager.PlayEnterGameOverAnimation();
            Instance.uiManager.UpdatePointsText(Instance.gameVariables.Points);

            while (currentState == GameState.GameOver)
                yield return null;
        }

        private void OnGameOver()
        {
            Instance.animationManager.PlayExitGameAnimation();
            Instance.uiManager.UpdateTimeLeftText(0);
            Instance.objectSpawner.HideButton();
            ChangeState(GameState.GameOver);
        }

        public void OnStartGame()
        {
            InitializeVariables();
            gameVariables.ResetGame();
            ChangeState(GameState.PlayMode);
        }

        public void OnReturnToMenu()
        {
            ChangeState(GameState.Menu);
        }
    }
}