using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class AudioManager : MonoBehaviour
{

    public static AudioManager Instance;

    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource sfxSource;

    [Header("Audio Clips")]
    [SerializeField] AudioClip[] musicClips;
    [SerializeField] AudioClip[] sfxClips;

    bool isMusicPaused = false;
    float musicPauseTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMusic(int clipIndex, bool loop)
    {
        if (clipIndex >= 0 && clipIndex < musicClips.Length)
        {
            musicSource.clip = musicClips[clipIndex];
            musicSource.Play();
            musicSource.loop = loop;
            isMusicPaused = false;
        }
        else
        {
            Debug.LogWarning("G PAS TA MUSIQUE");
        }
    }

    public void PlaySFX(int clipIndex)
    {
        if (clipIndex >= 0 && clipIndex < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[clipIndex]);
        }
        else
        {
            Debug.LogWarning("G PAS TON SFX");
        }
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PauseMusic()
    {
        if (!isMusicPaused)
        {
            musicPauseTime = musicSource.time;
            musicSource.Pause();
            isMusicPaused = true;
        }
    }

    public void ResumeMusic()
    {
        if (isMusicPaused)
        {
            musicSource.time = musicPauseTime;
            musicSource.Play();
            isMusicPaused = false;
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
        isMusicPaused = false;
    }

    public bool IsMusicPaused()
    {
        return isMusicPaused;
    }
}
