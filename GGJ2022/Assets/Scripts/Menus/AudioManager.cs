using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Master AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Toggle Audio On/Off")]
    [SerializeField] private Toggle audioToggle;

    [Header("Sliders to Manager AudioMixer")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    [Header("Menu Interaction Sounds")]
    [SerializeField] private AudioSource buttonClick;

    private void Start()
    {
        float currentSFX = PlayerPrefs.GetFloat("sfxVol");
        float currentMusic = PlayerPrefs.GetFloat("mVol");

        musicSlider.value = currentMusic;
        sfxSlider.value = currentSFX;

        if (PlayerPrefs.GetInt("Audio") == 1)
            audioToggle.isOn = true;

        else
            audioToggle.isOn = false;
    }

    private void Update()
    {
        if (audioToggle.isOn)
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Audio", 1);
        }

        else
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Audio", 0);
        }
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
