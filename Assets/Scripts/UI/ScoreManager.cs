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

        private const string ScoreKey = "PlayerScore";

        private void Awake()
        {
            LoadScore(); // Load the score when the game starts
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

            SaveScore(); // Save the score when the game is closed or the script is disabled
        }

        private void AddScore(int points)
        {
            currentScore += points;
            UpdateScoreText();
        }

        private void KillEnemy(int enemyPoints)
        {
            currentKill += enemyPoints;
            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            scoreText.text = $"{currentScore}";
            killText.text = $"{currentKill}";
        }

        private void SaveScore()
        {
            PlayerPrefs.SetInt(ScoreKey, currentScore);
            PlayerPrefs.Save();
        }

        private void LoadScore()
        {
            currentScore = PlayerPrefs.GetInt(ScoreKey, 0);
        }
    }
}
