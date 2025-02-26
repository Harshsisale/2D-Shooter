using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour
{
    private Health playerHealth;
    public int currentLives;

    // Event to notify listeners when lives change
    public event Action<int> OnLivesChanged;

    void Start()
    {
        // Get the Health component from the Player
        playerHealth = GetComponent<Health>();

        if (playerHealth == null)
        {
            Debug.LogError("Health component is missing on Player GameObject!");
        }
    }

    void Update()
    {
        // Update the currentLives value from Health.cs
        if (playerHealth != null)
        {
            int previousLives = currentLives;
            currentLives = playerHealth.currentLives;

            // Check if lives have changed
            if (currentLives != previousLives)
            {
                // Trigger the event when lives change
                OnLivesChanged?.Invoke(currentLives);

                // Store lives in GameManager before the Player is destroyed
                if (GameManager.instance != null)
                {
                    GameManager.instance.UpdatePlayerLives(currentLives);
                }
            }
        }
    }
}
