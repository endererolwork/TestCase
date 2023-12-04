using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlictusPlatform;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] FloatEventChannel playerHealthChannel;
    [SerializeField] private int enemyPoints = 1;
    public static event Action<int> OnAnyKilled;
    
    [SerializeField]private GameObject panel;

    int currentHealth;

    public bool IsDead => currentHealth <= 0;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    void Start()
    {
        PublishHealthPercentage();
    }

    public void TakeDamage(int damage)
    {
        if (!IsDead) // Only take damage if not already dead
        {
            currentHealth -= damage;
            PublishHealthPercentage();

            if (IsDead)
            {
                // Perform death actions, e.g., play death animation, destroy the object
                StartCoroutine(Die());
            }
        }

        // Check if player health is 0
        if (gameObject.CompareTag("Player") && currentHealth <= 0)
        {
            ReloadScene();
        }
    }

    void PublishHealthPercentage()
    {
        if (playerHealthChannel != null)
            playerHealthChannel.Invoke(currentHealth / (float)maxHealth);
    }

    IEnumerator Die()
    {
        // Play death animation or perform other death-related actions
        yield return new WaitForSeconds(1.0f); // Adjust as needed

        // Notify that the enemy has been killed
        OnAnyKilled?.Invoke(enemyPoints); // Replace EnemyPoints with the actual points you want to assign

        // Check if the object is the player
        if (gameObject.CompareTag("Player"))
        {
            ReloadScene();
        }
        else
        {
            // Destroy the object after the death animation or actions
            Destroy(gameObject);
        }
    }

    void ReloadScene()
    {
            panel.gameObject.SetActive(true);
        
    }
}
