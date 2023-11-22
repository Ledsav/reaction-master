using TMPro;
using UnityEngine;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeLeftText;

        public void UpdateTimeLeftText(float timeLeft)
        {
            timeLeftText.text = timeLeft.ToString("F2");
        }
    }
}