using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioMixer audioMixer;

    public float minVolumeLevel = 0.0001f;
    public float maxVolumeLevel = 1.0f;

    public float minSliderValue = 0;
    public float maxSliderValue = 4;

    public AudioSource musicSource;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        musicSource.time = PlayerPrefs.GetFloat("MusicTrackPosition", 0);
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("MusicTrackPosition");
    }

    public void SetMasterVolume(float sliderValue)
    {
        SetVolume(sliderValue, "Master");
    }

    public void SetMusicVolume(float sliderValue)
    {
        SetVolume(sliderValue, "Music");
    }

    public void SetSFXVolume(float sliderValue)
    {
        SetVolume(sliderValue, "SFX");
    }

    public void SetVolume(float sliderValue, string mixName)
    {
        float logValue = (sliderValue - minSliderValue) * ((maxVolumeLevel - minVolumeLevel) / (maxSliderValue - minSliderValue)) + minVolumeLevel;
        audioMixer.SetFloat(mixName + "Vol", Mathf.Log10(logValue) * 20);
    }

    public void SaveTrackPosition()
    {
        PlayerPrefs.SetFloat("MusicTrackPosition", musicSource.time);
    }

    public IEnumerator FadeMusic(bool fadeIn, float duration)
    {
        float startingVol, endingVol;
        if (fadeIn)
        {
            startingVol = 0;
            endingVol = 1;
        } else
        {
            startingVol = 1;
            endingVol = 0;
        }

        for (float timeLeft = duration; timeLeft > 0; timeLeft -= Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startingVol, endingVol, 1 - (timeLeft / duration));
            yield return 0;
        }
    }
}
