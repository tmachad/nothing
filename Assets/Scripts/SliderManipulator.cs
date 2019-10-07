using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SliderManipulator : MonoBehaviour
{
    public Slider slider;

    public string identifier;
    public float defaultValue = 4;
    public AudioSource audioSource;

    private void Awake()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        // Mute the audio source to prevent ear-bleed when initial slider value is set
        if (audioSource != null)
        {
            audioSource.mute = true;
        }
        slider.value = PlayerPrefs.GetFloat(identifier + "Slider", defaultValue);
        if (audioSource != null)
        {
            StartCoroutine(Unmute());
        }
    }

    public void UpdateSavedValue(float value)
    {
        PlayerPrefs.SetFloat(identifier + "Slider", value);
    }

    public void ChangeSliderValue(float delta)
    {
        slider.value = slider.value + delta;
    }

    private IEnumerator Unmute()
    {
        yield return new WaitForSeconds(0.25f);
        audioSource.mute = false;
    }
}
