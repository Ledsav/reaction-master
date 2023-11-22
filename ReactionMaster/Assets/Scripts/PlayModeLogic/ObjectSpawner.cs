using UnityEngine;
using System.Collections;
using Managers;
using ScriptableObjects;

namespace PlayModeLogic
{
    public class ObjectSpawner : MonoBehaviour
    {
        [Header("Buttons Settings")]
        [SerializeField] private GameObject buttonPrefab; // The button prefab to spawn
        [SerializeField] private ButtonConfig[] buttonConfigs; // The button configurations to use
        
        [Header("Canvas Settings")]
        [SerializeField] private Canvas gameCanvas; // The canvas to spawn the buttons on

        [Header("Spawn Settings")]

        private RectTransform _canvasRectTransform;
        private RectTransform _activeButtonRectTransform;
        private PlayModeLogic.Button _activeButton;
        private Vector4 _margins;
        private float _spawnInterval;
        

        private void Awake()
        {
            _canvasRectTransform = gameCanvas.GetComponent<RectTransform>();
            PrepareButton();
        }
        
        private void InitializeVariables()
        {
            _margins = GameManager.Instance.gameVariables.Margins;
        }

        private void PrepareButton()
        {
            var newButton = Instantiate(buttonPrefab, gameCanvas.transform);
            _activeButtonRectTransform = newButton.GetComponent<RectTransform>();
            _activeButton = newButton.GetComponent<PlayModeLogic.Button>();
            newButton.SetActive(false);
        }

        private void Start()
        {
            InitializeVariables();
        }

 
        public void RecycleButton()
        {
            // Deactivate the previous button if it's active
            if (_activeButtonRectTransform.gameObject.activeSelf)
            {
                _activeButtonRectTransform.gameObject.SetActive(false);
            }

            // Randomly position the new button within the available space
            var sizeDelta = _canvasRectTransform.sizeDelta;
            var availableWidth = sizeDelta.x - (_margins.x + _margins.z);
            var availableHeight = sizeDelta.y - (_margins.y + _margins.w);

            var newX = Random.Range(_margins.x, _margins.x + availableWidth) - (_canvasRectTransform.sizeDelta.x / 2);
            var newY = Random.Range(_margins.w, _margins.w + availableHeight) - (_canvasRectTransform.sizeDelta.y / 2);

            _activeButtonRectTransform.anchoredPosition = new Vector2(newX, newY);
            
            _activeButton.SetButton(Random.value < GameManager.Instance.gameVariables.BadButtonProbability
                ? buttonConfigs[1]
                : buttonConfigs[0]);
            _activeButtonRectTransform.gameObject.SetActive(true); 
        }
    }
}