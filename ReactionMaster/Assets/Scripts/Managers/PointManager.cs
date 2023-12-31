﻿using PlayModeLogic;
using UnityEngine;

namespace Managers
{
    public class PointManager : MonoBehaviour
    {
        private void OnEnable()
        {
            Button.Clicked += AddPoints;
        }

        private void OnDisable()
        {
            Button.Clicked -= AddPoints;
        }

        private void AddPoints(int? points)
        {
            GameManager.Instance.gameVariables.Points += points ?? 0;
        }
    }
}