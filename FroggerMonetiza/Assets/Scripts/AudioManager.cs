using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public GameObject musicPlayer, sfxPlayer;
    private AudioSource musicSource, sfxSource;
    public AudioClip hop, goal, death, time;

    void Awake()
    {
        if (instance == null)
            instance = this;

        musicSource = musicPlayer.GetComponent<AudioSource>();
        sfxSource = sfxPlayer.GetComponent<AudioSource>();

        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
            musicSource.Play();
    }

    public void StopMusic()
    {
        if (musicSource.isPlaying)
            musicSource.Stop();
    }

    public void PlaySFX(string sound)
    {
        switch (sound)
        {
            case "Hop":
                sfxSource.PlayOneShot(hop);
                break;
            case "Goal":
                sfxSource.PlayOneShot(goal);
                break;
            case "Death":
                sfxSource.PlayOneShot(death);
                break;
            case "Time":
                sfxSource.PlayOneShot(time);
                break;
        }
    }
}
