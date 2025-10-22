using UnityEngine;
using System;
using System.Collections.Generic;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public List<AudioClip> audioClips;
    public AudioSource sfxSource;
    
    public AudioClip fishShootSoundEffect;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource.clip = audioClips[0];
        audioSource.loop = true; // Enable looping
        audioSource.Play(); // Start playing the music
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeBackgroundMusic(int index)
    {
        audioSource.clip = audioClips[index];
        audioSource.loop = true; // Enable looping
        audioSource.Play(); // Start playing the music
    }
    public void playFSE()
    {
        sfxSource.PlayOneShot(fishShootSoundEffect);
    }
}
