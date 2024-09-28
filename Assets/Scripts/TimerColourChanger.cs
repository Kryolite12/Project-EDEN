using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TimerColourChanger : MonoBehaviour
{
    public Image Background;
    public Image Slider;
    public TextMeshProUGUI Timer;
    public GameObject LevelControler; // The GameObject that has the TimerScript attached
    private LevelControllerScript timerScript;

    public Sprite Orange;
    public Sprite Red;

    public Color orange;
    public Color red;
    // Start is called before the first frame update
    private void Start()
    {
        timerScript = LevelControler.GetComponent<LevelControllerScript>();
    }
    // Update is called once per frame
    public void Update()
    {
        float currentTime = timerScript.sliderTimer;

        if (currentTime <= 40 && currentTime > 20)
        {
            Background.sprite = Orange; // First sprite
            Slider.color = orange;
            ChangeVertexColor(orange);
        }
        else if (currentTime <= 20 && currentTime >= 0)
        {
            Background.sprite = Red; // Second sprite
            Slider.color = red;
            ChangeVertexColor(red);
        }
      

    }
    public void ChangeVertexColor(Color color)
    {
        Timer.color = color; // This changes the vertex color of the entire text
    }
}
