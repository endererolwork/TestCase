// Health.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlictusPlatform;

public class Health : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] FloatEventChannel playerHealthChannel;

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

        // Destroy the object after the death animation or actions
        Destroy(gameObject);
    }
}