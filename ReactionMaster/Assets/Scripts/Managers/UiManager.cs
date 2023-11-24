using TMPro;
using UnityEngine;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeLeftText;
        [SerializeField] private TMP_Text pointsText;

        public void UpdateTimeLeftText(float timeLeft)
        {
            timeLeft = (int)timeLeft;
            timeLeftText.text = timeLeft.ToString();
        }

        public void UpdatePointsText(int points)
        {
            pointsText.text = points + "\nPOINTS";
            // "You are the <b>#" + points + "</b> best player";
        }
    }
}