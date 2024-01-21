using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WindPresetDropdown : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown _dropdown;

    [SerializeField]
    private WindPreset[] _presets;

    [SerializeField]
    private WindCalculator _windCalculator;

    private void Awake()
    {
        _windCalculator.SetPreset(_presets[0]);
    }

    private void OnEnable()
    {
        _dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnDisable()
    {
        _dropdown.onValueChanged.RemoveListener(OnValueChanged);
    }

    private void OnValueChanged(int value)
    {
        _windCalculator.SetPreset(_presets[value]);
    }
}
