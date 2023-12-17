using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = PlayModeLogic.Button;

namespace Managers
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text timeLeftText;
        [SerializeField] private TMP_Text pointsText;
        [SerializeField] private TMP_Text leaderboardText;
        [SerializeField] private TMP_Text reactionTimeText;
        [SerializeField] private Image outerPanel;
        [SerializeField] private Image circlePanel;
        [SerializeField] private Color correctColor;
        [SerializeField] private Color wrongColor;

        private void OnEnable()
        {
            Button.Clicked += OnNewPoints;
        }

        private void OnDisable()
        {
            Button.Clicked -= OnNewPoints;
        }

        public void UpdateTimeLeftText(float timeLeft)
        {
            timeLeft = (int)timeLeft;
            timeLeftText.text = timeLeft.ToString();
        }

        public void UpdatePointsText(int points)
        {
            pointsText.text = points + "\nPOINTS";

            var placeInLeaderboard = GameManager.Instance.gameVariables.GetPlaceInLeaderboard(points);
            leaderboardText.text = placeInLeaderboard == -1
                ? "You are not in the leaderboard"
                : "You are the <b>#" + placeInLeaderboard + "</b> best player";

            var averageReactionTime = (int)(GameManager.Instance.gameVariables.getAverageReactionTime() * 100) / 100f;
            reactionTimeText.text =
                $"<size=50>Average reaction time:</size> <b>{averageReactionTime}</b>s ";
        }

        private void OnNewPoints(int? points)
        {
            StartCoroutine(points > 0 ? ChangeColorOfPanel(correctColor) : ChangeColorOfPanel(wrongColor));
        }

        private IEnumerator ChangeColorOfPanel(Color color)
        {
            var currentColor = outerPanel.color;
            outerPanel.color = color;
            circlePanel.color = color;
            yield return new WaitForSeconds(0.2f);
            outerPanel.color = currentColor;
            circlePanel.color = currentColor;
        }
    }
}