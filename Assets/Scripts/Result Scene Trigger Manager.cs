using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneTriggerManager : MonoBehaviour
{
    public GameObject AccessGranted;
    public GameObject AccessDeined;
    public GameObject HackingAborted;

    // Time to wait before switching scenes
    public float waitTime = 1.0f;

    private int currentLevelIndex;

    private void Start()
    {
        // Get the current level by subtracting the offset (assuming levels start at build index 7)
        currentLevelIndex = SceneManager.GetActiveScene().buildIndex - 6; // Subtract 6 to get the game level
    }
    private void Update()
    {
        
        // Example: Call this method when checking if the object is active
        if (AccessGranted.activeSelf)
        {
            PlayerPrefs.SetInt("LastLevelIndex", SceneManager.GetActiveScene().buildIndex);
            StartCoroutine(SwitchSceneAfterDelay("Win Scene"));
            OnWin();
            
        }
        else if (AccessDeined.activeSelf)
        {
            PlayerPrefs.SetInt("LastLevelIndex", SceneManager.GetActiveScene().buildIndex);
            StartCoroutine(SwitchSceneAfterDelay("Lose Scene"));
        }
        else if (HackingAborted.activeSelf)
        {
            PlayerPrefs.SetInt("LastLevelIndex", SceneManager.GetActiveScene().buildIndex);
            StartCoroutine(SwitchSceneAfterDelay("Abort Scene"));
        }
    }

    private IEnumerator SwitchSceneAfterDelay(string sceneName)
    {
        // Wait for the specified time
        yield return new WaitForSeconds(waitTime);

        // Switch to the specified scene
        SceneManager.LoadScene(sceneName);
    }
    public void OnWin()
    {
        LevelProgression progression = FindObjectOfType<LevelProgression>();
        progression.UnlockNextLevel(currentLevelIndex + 1);
    }

   
}




   
   
