using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;

    // UI Image reference for the heart display
    public Image heartDisplay;

    // Sprites for different heart states
    public Sprite threeFull;
    public Sprite twoFullOneCracked;
    public Sprite oneFullTwoCracked;
    public Sprite allCracked;

    // Store the previous lives value to track changes
    private int previousLives = 3;

    // Page index for Game Over and Victory screens
    public int gameOverPageIndex = 0;
    public int victoryPageIndex = 1;

    void Start()
    {
        // Get the PlayerHealth component from the Player
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            playerHealth.OnLivesChanged += UpdateHeartDisplay;
        }
        else
        {
            Debug.LogError("Player GameObject with tag 'Player' not found!");
        }

        // Initial Update to ensure correct display on scene load
        UpdateHeartDisplay(GameManager.instance.playerLives);
    }

    void OnDestroy()
    {
        // Unsubscribe from the event to avoid memory leaks
        if (playerHealth != null)
        {
            playerHealth.OnLivesChanged -= UpdateHeartDisplay;
        }
    }

    // Event handler to update the heart display
    void UpdateHeartDisplay(int lives)
    {
        // Only update if lives have changed
        if (lives != previousLives)
        {
            previousLives = lives;

            switch (lives)
            {
                case 3:
                    heartDisplay.sprite = threeFull;
                    break;
                case 2:
                    heartDisplay.sprite = twoFullOneCracked;
                    break;
                case 1:
                    heartDisplay.sprite = oneFullTwoCracked;
                    break;
                case 0:
                    heartDisplay.sprite = allCracked;
                    break;
                default:
                    Debug.LogWarning("Unexpected life value: " + lives);
                    break;
            }

            // Force the UI to update immediately
            Canvas.ForceUpdateCanvases();
        }
    }

    void LateUpdate()
    {
        // Check the current lives value from GameManager
        int currentLives = GameManager.instance.playerLives;

        // If the current lives are 1, check the screen
        if (currentLives == 1)
        {
            // Check the current screen from UIManager
            if (UIManager.instance != null)
            {
                int currentPageIndex = UIManager.instance.GetCurrentPageIndex();

                // If it's the Game Over screen, change to 0 hearts
                if (currentPageIndex == 1)
                {
                    heartDisplay.sprite = allCracked;
                    Canvas.ForceUpdateCanvases();
                }
                // If it's the Victory screen, do nothing
                else if (currentPageIndex == victoryPageIndex)
                {
                    // Do nothing, keep hearts as they are
                }
            }
        }
    }
}
