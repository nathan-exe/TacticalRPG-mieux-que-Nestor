using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CoolSlider : MonoBehaviour
{

    [SerializeField] float _value, _minValue, _maxValue;
    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            _mainSlider.DOValue(value, _tweenDuration);
            _backGroundSlider.DOValue(value, _tweenDuration*2f);
        }
    }

    public float MinValue 
    {   get => _minValue;  
        set {
            _minValue = value;
            _mainSlider.minValue = MinValue;
            _backGroundSlider.minValue = MinValue;
        } 
    }
    public float  MaxValue
    {
        get => _maxValue;
        set {
            _maxValue = value;
            _mainSlider.maxValue = MaxValue;
            _backGroundSlider.maxValue = MaxValue;
        }
    }

    [Header("Tweening")]
    [SerializeField] float _tweenDuration = .8f;

    [Header("References")]
    [SerializeField] Slider _mainSlider;
    [SerializeField] Slider _backGroundSlider;

    public void PreviewValue(float value)
    {
        _mainSlider.DOValue(value, _tweenDuration);
    }

    public void CancelPreview()
    {
        _mainSlider.DOValue(Value, _tweenDuration);
    }

    private void OnValidate()
    {
        Value = _value;
        MinValue = _minValue;
        MaxValue = _maxValue;
    }
}
