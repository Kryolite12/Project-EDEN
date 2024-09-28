using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitcher : MonoBehaviour
{
    public MonoBehaviour cameraScript; // Reference to the script on the Main Camera

    // Name of your tutorial scene
    public string tutorialSceneName = "Tutorial Level";
    // Name of your level selector scene
    public string levelSelectorSceneName = "Level Selector";

    // Function to switch scene after activating the camera script
    public void SwitchNextSceneWithDelay()
    {
        StartCoroutine(ActivateScriptAndSwitchNextScene());
    }
    public void SwitchPreviousSceneWithDelay()
    {
        StartCoroutine(ActivateScriptAndSwitchPreviousScene());
    }

    public void SwitchSceneWithDelay(string sceneName)
    {
        StartCoroutine(ActivateScriptAndSwitchScene(sceneName));
    }
    public void PlayButton()
    {
        StartCoroutine(PlayButtonLogic());
    }

    // Coroutine to activate the script for 1 second and then switch scenes
    private IEnumerator ActivateScriptAndSwitchNextScene()
    {
        cameraScript.enabled = true; // Activate the script on the Main Camera
        yield return new WaitForSeconds(0.3f); // Wait for 1 second
        cameraScript.enabled = false; // Deactivate the script
                                      // Get the next scene's build index
        int lastLevelIndex = PlayerPrefs.GetInt("LastLevelIndex", -1);

        // Check if the last level index was stored correctly
        if (lastLevelIndex != -1)
        {
            // Calculate the next level index
            int nextLevelIndex = lastLevelIndex + 1;

            // Check if the next level index is within the valid range
            if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextLevelIndex); // Load the next level
            }
            else
            {
                Debug.LogWarning("No more levels to load.");
                // Optionally, loop back to the first level or end the game
                // SceneManager.LoadScene(0); // Load the first level
            }
        }
        else
        {
            Debug.LogError("LastLevelIndex not found. Make sure it was set correctly.");
        }
    }

    private IEnumerator ActivateScriptAndSwitchPreviousScene()
    {
        cameraScript.enabled = true; // Activate the script on the Main Camera
        yield return new WaitForSeconds(0.3f); // Wait for 1 second
        cameraScript.enabled = false; // Deactivate the script
                                      // Get the next scene's build index
        int lastLevelIndex = PlayerPrefs.GetInt("LastLevelIndex", -1);

        // Check if the last level index was stored correctly
        if (lastLevelIndex != -1)
        {
            // Calculate the next level index
            int nextLevelIndex = lastLevelIndex;

            // Check if the next level index is within the valid range
            if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextLevelIndex); // Load the next level
            }
            else
            {
                Debug.LogWarning("No more levels to load.");
                // Optionally, loop back to the first level or end the game
                // SceneManager.LoadScene(0); // Load the first level
            }
        }
        else
        {
            Debug.LogError("LastLevelIndex not found. Make sure it was set correctly.");
        }
    }
    private IEnumerator ActivateScriptAndSwitchScene(string sceneName)
    {
        cameraScript.enabled = true; // Activate the script on the Main Camera
        yield return new WaitForSeconds(0.3f); // Wait for 1 second
        cameraScript.enabled = false; // Deactivate the script
        SceneManager.LoadScene(sceneName); // Switch to the desired scene
    }

    private IEnumerator PlayButtonLogic()
    {
        cameraScript.enabled = true; // Activate the script on the Main Camera
        yield return new WaitForSeconds(0.3f); // Wait for 1 second
        cameraScript.enabled = false; // Deactivate the script
        CheckTutorialStatus();
    }
    public void Back()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void CheckTutorialStatus()
    {
        // Check if the tutorial has been completed
        if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 1)
        {
            // If the tutorial was completed, go directly to the level selector
            LoadLevelSelector();
        }
        else
        {
            // If the tutorial was not completed, load the tutorial
            LoadTutorial();
        }
    }

    private void LoadTutorial()
    {
        // Load the tutorial scene
        SceneManager.LoadScene(tutorialSceneName);
    }

    private void LoadLevelSelector()
    {
        // Load the level selector scene
        SceneManager.LoadScene(levelSelectorSceneName);
    }
}
