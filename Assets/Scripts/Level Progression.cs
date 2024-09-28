using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    // Function to unlock the next level
    public void UnlockNextLevel(int nextLevelIndex)
    {
        // Get the highest level unlocked so far
        int highestUnlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1); // Default to level 1

        // If the next level index is greater than the highest unlocked level, save the new level
        if (nextLevelIndex > highestUnlockedLevel)
        {
            PlayerPrefs.SetInt("HighestUnlockedLevel", nextLevelIndex);
        }
    }

    // Function to get the highest unlocked level
    public int GetHighestUnlockedLevel()
    {
        return PlayerPrefs.GetInt("HighestUnlockedLevel", 1); // Default to level 1
    }
}
