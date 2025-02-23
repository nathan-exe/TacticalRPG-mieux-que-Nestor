using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainVolumeSlider : MonoBehaviour
{
    Slider _slider;
    private void Awake()
    {
        TryGetComponent(out _slider);
        _slider.onValueChanged.AddListener(onValueChanged);
        _slider.minValue = 0;
        _slider.maxValue = 2;
        _slider.value = PlayerPrefs.GetFloat("MainVolume",1f);
    }

    public void onValueChanged(float v)
    {
        AudioListener.volume = v;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("MainVolume",AudioListener.volume);
    }
}
