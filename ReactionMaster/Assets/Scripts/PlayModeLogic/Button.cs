using System;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace PlayModeLogic
{
    public class Button : MonoBehaviour
    {
        private Image _buttonImage;
        private int _buttonPoints;

        private void Awake()
        {
            _buttonImage = GetComponent<Image>();
        }

        public static event Action<int> Clicked;

        public void ClickButton()
        {
            Clicked?.Invoke(_buttonPoints);
            gameObject.SetActive(false);
        }

        public void SetButton(ButtonConfig buttonConfig)
        {
            _buttonImage.sprite = buttonConfig.buttonImage;
            _buttonImage.color = buttonConfig.buttonColor;
            _buttonPoints = buttonConfig.buttonPoints;
            gameObject.tag = buttonConfig.buttonTag;
        }
    }
}