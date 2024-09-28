using UnityEngine;
using UnityEngine.Video;

public class VideoEndHandler : MonoBehaviour
{
    public GameObject objectToDeactivate; // The GameObject with the VideoPlayer
    public GameObject objectToActivate2; // The GameObject with the Manager 
    public GameObject objectToActivate;   // The GameObject to activate after the video ends

    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = objectToDeactivate.GetComponent<VideoPlayer>();
        if (videoPlayer != null)
        {
            // Register the event handler to trigger when the video finishes playing
            videoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        objectToDeactivate.SetActive(false); // Deactivate the GameObject with the VideoPlayer
        objectToActivate.SetActive(true);
        objectToActivate2.SetActive(true);    // Activate the other GameObject
    }
   
}
