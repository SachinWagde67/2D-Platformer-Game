using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    public AudioSource soundEffect;
    public bool isMute = false;
    public float Volume = 1f;
    public AudioSource soundMusic;
    public SoundType[] allSounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetVolume(0.5f);
        PlayMusic(Sounds.Music);
    }

    public void Mute(bool status)
    {
        isMute = status;
    }

    public void SetVolume(float volume)
    {
        Volume = volume;
        soundEffect.volume = volume;
        soundMusic.volume = volume;
    }

    public void PlayMusic(Sounds sounds)
    {
        if(isMute)
        {
            return;
        }
        AudioClip clip = getSoundClip(sounds);
        if (clip != null)
        {
            soundMusic.clip = clip;
            soundMusic.Play();
        }
        else
        {
            Debug.LogError("Clip not found for sound type: " + sounds);
        }
    }

    public void Play(Sounds sounds)
    {
        if (isMute)
        {
            return;
        }

        AudioClip clip = getSoundClip(sounds);
        if(clip != null)
        {
            soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip not found for sound type: " + sounds);
        }
    }

    private AudioClip getSoundClip(Sounds sounds)
    {
        SoundType item = Array.Find(allSounds, i => i.soundType == sounds);
        if(item != null)
        {
            return item.soundClip;
        }
        return null;
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
};

public enum Sounds
{
    ButtonClick,
    Music,
    PlayerDeath,
    KeyPick,
    Teleporter,
}
