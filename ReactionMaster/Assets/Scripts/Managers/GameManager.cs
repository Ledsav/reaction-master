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

        [SerializeField] public GameVariables gameVariables;
        [SerializeField] public UiManager uiManager;
        [SerializeField] public ObjectSpawner objectSpawner;

        [SerializeField] private GameState currentState;

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
            ChangeState(GameState.PlayMode);
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
            // Access GameVariables here if needed
            // ...

            while (currentState == GameState.Menu)
                // Menu state execution code
                // ...
                yield return null;
        }

        private IEnumerator PlayModeState()
        {
            var timer = Instance.gameVariables.NumberOfButtonsToSpawn *
                        Instance.gameVariables.SpawnInterval;

            while (currentState == GameState.PlayMode)
            {
                Instance.objectSpawner.RecycleButton();
                Instance.gameVariables.SpawnedButtons++;
                yield return new WaitForSeconds(Instance.gameVariables.SpawnInterval);
                timer -= Instance.gameVariables.SpawnInterval;
                Instance.uiManager.UpdateTimeLeftText(timer);
                if (Instance.gameVariables.SpawnedButtons >= Instance.gameVariables.NumberOfButtonsToSpawn)
                    ChangeState(GameState.GameOver);
            }
        }

        private IEnumerator GameOverState()
        {
            // Access GameVariables here if needed
            // ...

            while (currentState == GameState.GameOver)
                // GameOver state execution code
                // ...
                yield return null;
        }

        public void OnGameOver()
        {
            ChangeState(GameState.GameOver);
        }

        public void OnRestartGame()
        {
            ChangeState(GameState.PlayMode);
        }

        public void OnReturnToMenu()
        {
            ChangeState(GameState.Menu);
        }

        // Additional methods to access or modify game variables
        // Example: public void UpdateSomeGameVariable(Type newValue) { ... }
    }
}