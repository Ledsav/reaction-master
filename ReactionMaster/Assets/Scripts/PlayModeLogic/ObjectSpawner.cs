using UnityEngine;
using System.Collections;
using Managers;

namespace PlayModeLogic
{
    public class ObjectSpawner : MonoBehaviour
    {
        [Header("Prefab Settings")]
        [SerializeField] private GameObject buttonPrefab; // The button prefab to spawn

        [Header("Canvas Settings")]
        [SerializeField] private Canvas gameCanvas; // The canvas to spawn the buttons on

        [Header("Spawn Settings")]
        [SerializeField] private float spawnInterval = 2f; // Time between each spawn
        [SerializeField] private Vector4 margins; // Margins as Left, Top, Right, Bottom

        private RectTransform _canvasRectTransform;
        private RectTransform _activeButtonRectTransform; // Track the currently active button

        private void Awake()
        {
            _canvasRectTransform = gameCanvas.GetComponent<RectTransform>();
            PrepareButton();
        }

        private void PrepareButton()
        {
            var newButton = Instantiate(buttonPrefab, gameCanvas.transform);
            _activeButtonRectTransform = newButton.GetComponent<RectTransform>();
            newButton.SetActive(false);
        }

        private void Start()
        {
            StartCoroutine(SpawnButtonRoutine());
        }

        private IEnumerator SpawnButtonRoutine()
        {
            while (true)
            {
                RecycleButton();
                yield return new WaitForSeconds(spawnInterval);
            }
        }

        private void RecycleButton()
        {
            // Deactivate the previous button if it's active
            if (_activeButtonRectTransform.gameObject.activeSelf)
            {
                _activeButtonRectTransform.gameObject.SetActive(false);
            }

            // Randomly position the new button within the available space
            var sizeDelta = _canvasRectTransform.sizeDelta;
            var availableWidth = sizeDelta.x - (margins.x + margins.z);
            var availableHeight = sizeDelta.y - (margins.y + margins.w);

            var newX = Random.Range(margins.x, margins.x + availableWidth) - (_canvasRectTransform.sizeDelta.x / 2);
            var newY = Random.Range(margins.w, margins.w + availableHeight) - (_canvasRectTransform.sizeDelta.y / 2);

            _activeButtonRectTransform.anchoredPosition = new Vector2(newX, newY);
            _activeButtonRectTransform.gameObject.SetActive(true); 
        }
    }
}
