using System;
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

        private void OnEnable()
        {
            Collectible.OnAnyCollected += AddScore;
            Health.OnAnyKilled += KillEnemy;
        }

        private void OnDisable()
        {
            Collectible.OnAnyCollected -= AddScore;
            Health.OnAnyKilled -= KillEnemy;
        }

        public void AddScore(int points)
        {
            currentScore += points;
            UpdateScoreText();
        }

        public void KillEnemy(int enemyPoints)
        {
            currentKill += enemyPoints;
            UpdateScoreText();
        }

        public void UpdateScoreText()
        {
            scoreText.text = $"{currentScore}";
            killText.text = $"{currentKill}";
        }
    }
}