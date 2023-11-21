using UnityEngine;
using System.Collections;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    // This will only happen if the GameManager is not found in the scene before any call is made
                    _instance = new GameObject("GameManager").AddComponent<GameManager>();
                }
                return _instance;
            }
        }

        private enum GameState
        {
            Menu,
            PlayMode,
            GameOver
        }

        private GameState _currentState;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                // Destroy any duplicate instance that might have been created
                Destroy(gameObject);
                return;
            }
            _instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the singleton persists across scenes
        }

        private void Start()
        {
            ChangeState(GameState.Menu);
        }

        private void ChangeState(GameState newState)
        {
            // Stop the current state coroutine if it's running
            StopAllCoroutines();

            // Start the new state coroutine
            _currentState = newState;
            switch (_currentState)
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
            }
        }

        private IEnumerator MenuState()
        {
            // Code for Menu state initialization
            // ...

            while (_currentState == GameState.Menu)
            {
                // Menu state execution code
                // ...

                yield return null;
            }

            // Code to clean up Menu state
            // ...
        }

        private IEnumerator PlayModeState()
        {
            // Code for PlayMode state initialization
            // ...

            while (_currentState == GameState.PlayMode)
            {
                // PlayMode state execution code
                // ...

                yield return null;
            }

            // Code to clean up PlayMode state
            // ...
        }

        private IEnumerator GameOverState()
        {
            // Code for GameOver state initialization
            // ...

            while (_currentState == GameState.GameOver)
            {
                // GameOver state execution code
                // ...

                yield return null;
            }

            // Code to clean up GameOver state
            // ...
        }

        // Example function to call when the game is over
        public void OnGameOver()
        {
            ChangeState(GameState.GameOver);
        }

        // Example function to call when restarting the game
        public void OnRestartGame()
        {
            ChangeState(GameState.PlayMode);
        }

        // Example function to go back to menu
        public void OnReturnToMenu()
        {
            ChangeState(GameState.Menu);
        }
    }
}
