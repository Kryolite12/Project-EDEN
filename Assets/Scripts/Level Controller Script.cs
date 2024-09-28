using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelControllerScript : MonoBehaviour
{
   
    public GameObject[] layers; // Reference to Layer 3 GameObject
    public GameObject AccesGranted;// The GameObject to activate (background layer and image)
    public GameObject AccessDenied;
    public GameObject Greencontainer;
    public GameObject Whitecontainer;
    public GameObject[] DecryptionKeys;
    public Slider TimerSlider;
    public float sliderTimer;
    private bool stopTimer = false;
    private bool TimerOver = false;
    public TextMeshProUGUI TimeCounter;
    public Button Rewind; 
    private bool rewindUsed = false;


    private void Start()
    {
        TimerSlider.maxValue= sliderTimer;
        TimerSlider.value= sliderTimer;
        StartTimer();
    }


    public void StartTimer()
    {
        StartCoroutine(StartTheTimerTicker());
    }

    IEnumerator StartTheTimerTicker()
    {
        while (stopTimer == false)
        {
            UpdateTimerText();
            sliderTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            if (sliderTimer <= 0)
            {
                stopTimer = true;
                TimerOver = true;
                AccessDenied.SetActive(true);
            }
            if (stopTimer == false)
            {
               
                TimerSlider.value = sliderTimer;
            }
        }

        void UpdateTimerText()
        {
            // Format the float value to show only two decimal places (e.g., 59.99)
            TimeCounter.text = sliderTimer.ToString("0 s"); // Change format if necessary
        }

    }
    public void PauseGame()
    {
        Time.timeScale = 0f;  // Pauses time-based Coroutines and animations
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;  // Resumes time
    }

    public void RewindTimer()
    {
        if (rewindUsed)
        {
            Debug.Log("Rewind has already been used.");
            return;
        }

        if (sliderTimer != 0)
        {
            sliderTimer += 10f; // Add 10 seconds to the timer
            TimerSlider.maxValue += 10f; // Adjust the slider max value if needed
            rewindUsed = true;
            Rewind.interactable = false;
        }
    }

    private void Update()
    {
        // Check if all layers are empty
        if (AreAllLayersEmpty())
        {
            // Activate the background and image
            AccesGranted.SetActive(true);
            Greencontainer.SetActive(true);
            Whitecontainer.SetActive(false);
            stopTimer = true;

        }
        for (int i = 0; i < layers.Length; i++)
        {
            if (IsLayerEmpty(layers[i]) && i < DecryptionKeys.Length)
            {
                DecryptionKeys[i].SetActive(true);
            }
        }
    }

    private bool AreAllLayersEmpty()
    {
        foreach (GameObject layer in layers)
        {
            if (!IsLayerEmpty(layer))
            {
                return false; // If any layer is not empty, return false
            }
        }
        return true; // All layers are empty
    }

    private bool IsLayerEmpty(GameObject layer)
    {
        // A layer is empty if it has no active child objects
        foreach (Transform child in layer.transform)
        {
            if (child.gameObject.activeSelf)
            {
                return false; // If any child is active, the layer is not empty
            }
        }
        return true; // All children are inactive, so the layer is empty
    }
}


