using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public float minVolumeLevel = 0.0001f;
    public float maxVolumeLevel = 1.0f;

    public float minSliderValue = 0;
    public float maxSliderValue = 4;

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
}
