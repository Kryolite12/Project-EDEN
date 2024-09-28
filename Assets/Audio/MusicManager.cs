using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MusicManager : MonoBehaviour
{
    private AudioSource AudioSource;
    public AudioClip[] Tracks;
    public float volume = 1.0f;

    private string tutorial;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        if (!AudioSource.isPlaying)
            ChangeTrack(Random.Range(0, Tracks.Length));

    }

    // Update is called once per frame
    void Update()
    {
        AudioSource.volume = volume;

        if(!AudioSource.isPlaying)
            ChangeTrack(Random.Range(0, Tracks.Length));

        MuteMusic();
    }
    public void ChangeTrack(int songPicked)
    {
        AudioSource.clip = Tracks[songPicked];
        AudioSource.Play();
    }
    private void Awake()
    {
        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("BGM");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

     public void MuteMusic()
    {
        // Get the current scene name
        string sceneName = SceneManager.GetActiveScene().name;

        // If the scene is "Tutorial Level", mute the music by setting volume to 0
        if (sceneName == "Tutorial Level")
        {
            AudioSource.volume = 0f;
        }
        else
        {
            // Restore volume if it's not the Tutorial scene
            AudioSource.volume = volume;
        }
    }
}

