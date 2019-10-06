using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderManipulator : MonoBehaviour
{
    public Slider slider;

    public string identifier;
    public float defaultValue = 4;

    private void Awake()
    {
        if (slider == null)
        {
            slider = GetComponent<Slider>();
        }

        slider.value = PlayerPrefs.GetFloat(identifier + "Slider", defaultValue);
    }

    public void UpdateSavedValue(float value)
    {
        PlayerPrefs.SetFloat(identifier + "Slider", value);
    }

    public void ChangeSliderValue(float delta)
    {
        slider.value = slider.value + delta;
    }
}
