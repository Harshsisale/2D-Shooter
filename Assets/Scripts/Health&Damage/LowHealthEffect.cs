using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LowHealthEffect : MonoBehaviour
{
    public PlayerHealth playerHealth;
    private PostProcessVolume volume;
    private int currentLives;

    void Start()
    {
        // Get the Post Process Volume component
        volume = GetComponent<PostProcessVolume>();

        // Get the PlayerHealth component from the Player GameObject
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found!");
        }
    }

    void Update()
    {
        if (playerHealth != null)
        {
            currentLives = playerHealth.currentLives;

            // Explicitly check the player's lives
            switch (currentLives)
            {
                case 3:
                    volume.weight = 0;       // No effect at full lives
                    break;
                case 2:
                    volume.weight = 0.1f;   // Slight red tint
                    break;
                case 1:
                    volume.weight = 0.3f;   // Stronger red tint
                    break;
                case 0:
                    volume.weight = 1f;     // Maximum red tint (critical state)
                    break;
                default:
                    volume.weight = 0;       // In case of unexpected values
                    break;
            }
        }
    }
}
