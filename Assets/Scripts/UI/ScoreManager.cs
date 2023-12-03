using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace AlictusPlatform
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI killText;


        private int currentScore;
        private int currentKill;

        private void Awake()
        {
            UpdateScoreText();
        }

        private void OnEnable() => Collectible.OnAnyCollected += AddScore;
        private void OnDisable() => Collectible.OnAnyCollected -= AddScore;

        // add method for killing

        public void AddScore(int points)
        {
            currentScore += points;
            UpdateScoreText();
        }

        public void UpdateScoreText()
        {
            scoreText.text = $"{currentScore}";
            killText.text = $"{currentKill}";
        }
    }
}