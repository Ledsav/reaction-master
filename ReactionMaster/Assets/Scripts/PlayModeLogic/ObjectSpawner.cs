using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PlayModeLogic
{
    public class ObjectSpawner : MonoBehaviour
    {
        [Header("Buttons Settings")] [SerializeField]
        private GameObject buttonPrefab; // The button prefab to spawn

        [SerializeField] private ButtonConfig[] buttonConfigs; // The button configurations to use

        [Header("Canvas Settings")] [SerializeField]
        private Canvas gameCanvas; // The canvas to spawn the buttons on

        [Header("Grid Settings")] [SerializeField]
        private int rows; // Number of rows in the grid

        [SerializeField] private int columns; // Number of columns in the grid
        [SerializeField] private Vector2 cellSpacing;

        private Button _activeButton;
        private Image _activeButtonImage;
        private RectTransform _activeButtonRectTransform;

        [Header("Spawn Settings")] private RectTransform _canvasRectTransform;
        private int _currentPositionIndex; // Current position index for button placement

        private Vector4 _margins;
        private float _spawnInterval;


        private void Awake()
        {
            _canvasRectTransform = gameCanvas.GetComponent<RectTransform>();
            PrepareButton();
        }

        private void Start()
        {
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            _margins = GameManager.Instance.gameVariables.Margins;
            SelectIndex();
        }

        private void SelectIndex()
        {
            _currentPositionIndex = Random.Range(0, rows * columns); // Reset position index
        }

        private void PrepareButton()
        {
            var newButton = Instantiate(buttonPrefab, gameCanvas.transform);
            _activeButton = newButton.GetComponent<Button>();
            _activeButtonRectTransform = newButton.GetComponent<RectTransform>();
            _activeButtonImage = newButton.GetComponent<Image>();
            HideButton();
        }


        public void RecycleButton()
        {
            // Deactivate the previous button if it's active
            if (_activeButtonImage.enabled) _activeButtonImage.enabled = false;

            // Calculate the available space considering margins and cell spacing
            var sizeDelta = _canvasRectTransform.sizeDelta;
            var availableWidth = sizeDelta.x - (_margins.x + _margins.z);
            var availableHeight = sizeDelta.y - (_margins.y + _margins.w);

            // Calculate grid cell size without considering cell spacing
            var cellWidth = availableWidth / columns;
            var cellHeight = availableHeight / rows;

            // Calculate new position based on the current index
            var row = _currentPositionIndex / columns;
            var col = _currentPositionIndex % columns;
            var newX = _margins.x + col * cellWidth + col * cellSpacing.x - sizeDelta.x / 2 + cellWidth / 2;
            var newY = _margins.y + row * cellHeight + row * cellSpacing.y - sizeDelta.y / 2 + cellHeight / 2;

            _activeButtonRectTransform.anchoredPosition = new Vector2(newX, -newY);

            // Set button configuration
            _activeButton.SetButton(Random.value < GameManager.Instance.gameVariables.BadButtonProbability
                ? buttonConfigs[1]
                : buttonConfigs[0]);
            _activeButtonImage.enabled = true;

            SelectIndex();

            GameManager.Instance.gameVariables.UpdateSpawnTimeStamp();
        }


        public void HideButton()
        {
            _activeButtonImage.enabled = false;
        }
    }
}