using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrecipitationController : MonoBehaviour
{
    [SerializeField]
    private Slider _rainSlider;

    [SerializeField]
    private Slider _snowSlider;

    [SerializeField]
    private WeatherController _weatherController;

    private void OnEnable()
    {
        _rainSlider.onValueChanged.AddListener(OnRainSliderValueChanged);
        _snowSlider.onValueChanged.AddListener(OnSnowSliderValueChanged);
    }

    private void OnDisable()
    {
        _rainSlider.onValueChanged.RemoveListener(OnRainSliderValueChanged);
        _snowSlider.onValueChanged.RemoveListener(OnSnowSliderValueChanged);
    }

    private void OnRainSliderValueChanged(float value)
    {
        _weatherController.Rain = value;
    }

    private void OnSnowSliderValueChanged(float value)
    {
        _weatherController.Snow = value;
    }
}
