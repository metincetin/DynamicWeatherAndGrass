using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WindDirectionController : MonoBehaviour
{
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private WindCalculator _windCalculator;
    private void OnEnable()
    {
        _slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        _windCalculator.WindDirection = new Vector3(Mathf.Cos(value), 0, Mathf.Sin(value));
    }

    private void Awake()
    {
        var dir = _windCalculator.WindDirection.normalized;
        _slider.SetValueWithoutNotify(Mathf.Atan2(dir.y, dir.x));
    }

}
