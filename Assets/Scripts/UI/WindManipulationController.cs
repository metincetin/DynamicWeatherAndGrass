using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindManipulationController : MonoBehaviour
{
    [SerializeField]
    private Slider _powerSlider;
    [SerializeField]
    private Slider _radiusSlider;
    [SerializeField]
    private Slider _angleSlider;

    [SerializeField]
    private WindManipulationTester _tester;

    private void Awake()
    {
        _powerSlider.value = _tester.Power;
        _radiusSlider.value = _tester.Radius;
        _angleSlider.value = _tester.Angle;
    }

    private void OnEnable()
    {
        _powerSlider.onValueChanged.AddListener(OnPowerSliderValueChanged);
        _radiusSlider.onValueChanged.AddListener(OnRadiusSliderValueChanged);
        _angleSlider.onValueChanged.AddListener(OnAngleSliderValueChanged);
    }

    private void OnDisable()
    {
        _powerSlider.onValueChanged.RemoveListener(OnPowerSliderValueChanged);
        _radiusSlider.onValueChanged.RemoveListener(OnRadiusSliderValueChanged);
        _angleSlider.onValueChanged.RemoveListener(OnAngleSliderValueChanged);
    }

    private void OnAngleSliderValueChanged(float value)
    {
        _tester.Angle = value;
    }

    private void OnRadiusSliderValueChanged(float value)
    {
        _tester.Radius = value;
    }

    private void OnPowerSliderValueChanged(float value)
    {
        _tester.Power = value;
    }
}
