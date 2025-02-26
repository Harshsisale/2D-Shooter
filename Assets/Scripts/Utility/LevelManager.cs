using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    // Name of the PlayerPrefs key for saving progress
    private string progressKey = "LevelProgress";

    // Method to check if a level is unlocked
    public bool IsLevelUnlocked(int level)
    {
        int highestLevel = PlayerPrefs.GetInt(progressKey, 1); // Default to 1 if no progress saved
        return level <= highestLevel;
    }

    // Call this when a level is completed to unlock the next one
    public void CompleteLevel(int level)
    {
        int highestLevel = PlayerPrefs.GetInt(progressKey, 1);

        // If the completed level is the highest so far, unlock the next level
        if (level == highestLevel)
        {
            PlayerPrefs.SetInt(progressKey, highestLevel + 1);
            PlayerPrefs.Save();
        }
    }

    // Use this to load a level if it is unlocked
    public void LoadLevel(int level)
    {
        if (IsLevelUnlocked(level))
        {
            SceneManager.LoadScene("Level" + level);
        }
        else
        {
            Debug.Log("Level " + level + " is locked!");
        }
    }
}
