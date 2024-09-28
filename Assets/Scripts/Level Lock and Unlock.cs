using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelLockandUnlock : MonoBehaviour
{ 
    public Button[] buttons;

    private int highestLevel;
   
  
    // Start is called before the first frame update
    void Start()
    {
        // Get the highest unlocked level
        int highestUnlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 1);

        // Loop through all buttons and enable them based on the unlocked levels
        for (int i = 0; i < buttons.Length; i++)
        {
            // Enable the button if the level is unlocked, disable it otherwise
            buttons[i].interactable = (i + 1 <= highestUnlockedLevel);
        }
    }

   public void ResetProgress()
    {
        PlayerPrefs.DeleteAll(); // This will clear all saved PlayerPrefs
        PlayerPrefs.Save(); // Ensure the changes are saved immediately
        Debug.Log("PlayerPrefs has been reset.");
    }
}
