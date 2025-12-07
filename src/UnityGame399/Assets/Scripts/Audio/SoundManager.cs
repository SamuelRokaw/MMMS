using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    public AudioSource backgroundTrack;
    public AudioSource playerSFXSource;
    public AudioSource enemySFXSource;
    public AudioSource otherSFXSource;
    public static SoundManager Instance {get; private set;}

    [SerializeField] private float fadeDuration = 3f;
    
    private float currentBackgroundVolume = 1f;
    private float currentPlayerVolume = 1f;
    private float currentEnemyVolume = 1f;
    private float currentOtherVolume = 1f;
    private float masterVolume = 1f;
    
    
    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadAudioSettings();
    }

    public void playBackGroundTrack(AudioClip clip)
    {
        backgroundTrack.clip = clip;
        backgroundTrack.loop = true;
        backgroundTrack.Play();
    }

    public void switchBackGroundTrack(AudioClip clip)
    {
        backgroundTrack.Stop();
        playBackGroundTrack(clip);
    }
    public void playPlayerSFX(AudioClip clip)
    {
        playerSFXSource.PlayOneShot(clip);
    }
    public void playEnemySFX(AudioClip clip)
    {
        enemySFXSource.PlayOneShot(clip);
    }
    public void playOtherSFX(AudioClip clip)
    {
        otherSFXSource.PlayOneShot(clip);
    }

    public void switchBackGroundTrackWithFade(AudioClip clip) //for fun
    {
        StartCoroutine(FadeTrack(clip));
    }

    private IEnumerator FadeTrack(AudioClip clip)
    {
        float startingVolume = backgroundTrack.volume;

        while (backgroundTrack.volume > 0) // fade out loop
        {
            backgroundTrack.volume -= startingVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        
        switchBackGroundTrack(clip);

        while (backgroundTrack.volume < startingVolume) //fade in loop
        {
            backgroundTrack.volume += startingVolume * Time.deltaTime / fadeDuration;
            yield return null;
        }
        backgroundTrack.volume = startingVolume; //just in case it doesn't reach original volume
    }
    
    // Lower volume gradually (fade down)
    public IEnumerator LowerVolume(AudioSource source, float targetVolume, float duration)
    {
        float startVolume = source.volume;
        float time = 0f;

        while (time < duration)
        {
            source.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        source.volume = targetVolume;
    }

    // Reset volumes to currents
    public void ResetVolumes()
    {
        backgroundTrack.volume = 1;
        playerSFXSource.volume = 1;
        otherSFXSource.volume = 1f;
    }
    
    // -------------------------------
    // INDIVIDUAL VOLUME CONTROL
    // -------------------------------
    public void SetBackgroundVolume(float volume)
    {
        backgroundTrack.volume = Mathf.Clamp01(volume) * masterVolume;
        PlayerPrefs.SetFloat("BackgroundVolume", volume);
    }

    public void SetPlayerVolume(float volume)
    {
        playerSFXSource.volume = Mathf.Clamp01(volume) * masterVolume;
        PlayerPrefs.SetFloat("PlayerVolume", volume);
    }

    public void SetOtherVolume(float volume)
    {
        otherSFXSource.volume = Mathf.Clamp01(volume) * masterVolume;
        PlayerPrefs.SetFloat("OtherVolume", volume);
    }

    public void SetEnemyVolume(float volume)
    {
        enemySFXSource.volume = Mathf.Clamp01(volume) * masterVolume;
        PlayerPrefs.SetFloat("EnemyVolume", volume);
    }
    
    public void SetMasterVolume(float volume)
    {
        masterVolume = Mathf.Clamp01(volume);

        backgroundTrack.volume = PlayerPrefs.GetFloat("BackgroundVolume", currentBackgroundVolume) * masterVolume;
        playerSFXSource.volume = PlayerPrefs.GetFloat("PlayerVolume", currentPlayerVolume) * masterVolume;
        otherSFXSource.volume = PlayerPrefs.GetFloat("OtherVolume", currentOtherVolume) * masterVolume;
        enemySFXSource.volume = PlayerPrefs.GetFloat("EnemyVolume", currentEnemyVolume) * masterVolume;

        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
    }

    public float GetMasterVolume()
    {
        return masterVolume;
    }
    
    public void SaveAudioSettings()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolume);
        PlayerPrefs.SetFloat("BackgroundVolume", backgroundTrack.volume / masterVolume);
        PlayerPrefs.SetFloat("PlayerVolume", playerSFXSource.volume / masterVolume);
        PlayerPrefs.SetFloat("OtherVolume", otherSFXSource.volume / masterVolume);
        PlayerPrefs.SetFloat("EnemyVolume", currentEnemyVolume / masterVolume);
        PlayerPrefs.Save();
    }

    public void LoadAudioSettings()
    {
        masterVolume = PlayerPrefs.GetFloat("MasterVolume", 1f);

        float backgroundVol = PlayerPrefs.GetFloat("BackgroundVolume", currentBackgroundVolume);
        float playerVol = PlayerPrefs.GetFloat("PlayerVolume", currentPlayerVolume);
        float otherVol = PlayerPrefs.GetFloat("OtherVolume", currentOtherVolume);
        float enemyVol = PlayerPrefs.GetFloat("EnemyVolume", currentEnemyVolume);

        backgroundTrack.volume = backgroundVol * masterVolume;
        playerSFXSource.volume = playerVol * masterVolume;
        otherSFXSource.volume = otherVol * masterVolume;
        enemySFXSource.volume = enemyVol * masterVolume;
    }
}