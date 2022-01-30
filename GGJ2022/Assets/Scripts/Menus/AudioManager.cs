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
        audioToggle.isOn = true;
        float currentSFX = PlayerPrefs.GetFloat("sfxVol");
        float currentMusic = PlayerPrefs.GetFloat("mVol");

        musicSlider.value = currentMusic;
        sfxSlider.value = currentSFX;


        if (PlayerPrefs.GetInt("SoundSettings") == 0)
        {
            audioToggle.isOn = true;
        }

        else if (PlayerPrefs.GetInt("SoundSettings") == 1)
        {
            audioToggle.isOn = false;
        }
    }

    private void Update()
    {
        if (audioToggle.isOn == true)
        {
            PlayerPrefs.SetInt("SoundSettings", 0);
            AudioListener.volume = 1;
        }

        else if (audioToggle.isOn == false)
        {
            PlayerPrefs.SetInt("SoundSettings", 1);
            AudioListener.volume = 0;
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
