using System;
using System.Net.Mime;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace PlayModeLogic
{
    public class Button: MonoBehaviour
    {
        public static event Action<ButtonType> Clicked;
        
        private Image _buttonImage;
        
        private void Awake()
        {
            _buttonImage = GetComponent<Image>();
        }
        public void ClickButton()
        {
            var gameObjectTag = gameObject.tag;
            switch (gameObjectTag)
            {
                case "GoodButton":
                    Clicked?.Invoke(ButtonType.Good);
                    break;
                case "BadButton":
                    Clicked?.Invoke(ButtonType.Bad);
                    break;
            }
            
            gameObject.SetActive(false);   
            
        }
        
        public void SetButton(ButtonConfig buttonConfig)
        {
            _buttonImage.sprite = buttonConfig.buttonImage;
            _buttonImage.color = buttonConfig.buttonColor;
            gameObject.tag = buttonConfig.buttonTag;
           
        }
    }
}