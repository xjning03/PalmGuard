using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    private static BackgroundMusicController instance;

    public AudioSource backgroundMusic;
    public AudioClip background;

    private void Awake()
    {
        // Ensure only one instance of BackgroundMusicController exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicates
        }
    }

    private void Start()
    {
        backgroundMusic.clip = background;
        backgroundMusic.loop = true;
        backgroundMusic.Play();
    }

    
}
