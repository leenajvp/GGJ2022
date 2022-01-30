using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [Header("Menu Interaction Sounds")]

    [SerializeField] private AudioSource buttonClick;

    private void Start()
    {
        float currentSFX = PlayerPrefs.GetInt("sfxVol");
        float currentMusic = PlayerPrefs.GetInt("mVol");

        SetSFXLevel(currentSFX);
        SetMusicLevel(currentMusic);
    }

    private void Update()
    {
        
    }

    public void SetSFXLevel(float sfxLvl)
    {
        audioMixer.SetFloat("sfxVol", sfxLvl);
        PlayerPrefs.SetFloat("sfxVol", sfxLvl);
    }

    public void SetMusicLevel(float musicLvl)
    {
        audioMixer.SetFloat("mVol", musicLvl);
        PlayerPrefs.SetFloat("mVol", musicLvl);
    }
}
