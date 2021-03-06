﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource[] music;
    public AudioSource[] sfx;

    public int musicVolumeOnLevel;
    private int currentTrack;

    public AudioMixerGroup musicMixer, sfxMixer;

    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Boss1")
        {
            PlayMusic(8);
        }
        else
        {
            PlayMusic(0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            currentTrack++;
            if (currentTrack >= music.Length)
            {
                currentTrack = 0;
            }
            PlayMusic(currentTrack);

        }
    }

    public void PlayMusic(int musicToPlay)
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].Stop();
        }
        music[musicToPlay].Play();
    }

    public void PlaySFX(int sfxToPlay)
    {
        sfx[sfxToPlay].Play();
    }

    public void SetMusicLevel()
    {
        musicMixer.audioMixer.SetFloat("MusicVol", UIManager.instance.musicVolSlider.value);
    }

    public void SetSFXLevel()
    {
        sfxMixer.audioMixer.SetFloat("SfxVol", UIManager.instance.sfxVolSlider.value);
    }
}
