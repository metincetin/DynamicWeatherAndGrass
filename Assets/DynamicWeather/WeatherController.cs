using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    [SerializeField]
    private float _rain;
    public float Rain
    {
        get => _rain;
        set => _rain = value;
    }


    [SerializeField]
    private float _snow;
    public float Snow
    {
        get => _snow;
        set => _snow = value;
    }

    private float _snowMeshFill;
    public float SnowMeshFill => _snowMeshFill;

    private float _rainPuddle;
    public float RainPuddle => _rainPuddle;

    [SerializeField]
    private float _snowMeshFillDuration;
    public float SnowMeshFillDuration => _snowMeshFillDuration;

    [SerializeField]
    private float _snowMeshDecay;


    private float _smoothnessOffset;
    public float SmoothnessOffset => _smoothnessOffset;

    [SerializeField]
    private float _durationToFullWetness;
    [SerializeField]
    private float _durationForPuddle;

    [SerializeField]
    private float _puddleDecay;

    [SerializeField]
    private float _smoothnessDecay;

    private void Update()
    {
        if (_rain > 0)
        {
            _smoothnessOffset += _rain * Time.deltaTime / _durationToFullWetness;
            _rainPuddle += Time.deltaTime / _durationForPuddle;
        }
        else
        {
            _smoothnessOffset -= _smoothnessDecay * Time.deltaTime;
            _rainPuddle -= _puddleDecay + Time.deltaTime;
        }
        if (_snow > 0)
        {
            _snowMeshFill += _snow * Time.deltaTime / _snowMeshFillDuration;
        }
        else
        {
            _snowMeshFill -= _snowMeshDecay * Time.deltaTime;
        }

        _smoothnessOffset = Mathf.Clamp(_smoothnessOffset, 0, 0.9f);
        _rainPuddle = Mathf.Clamp01(_rainPuddle);
        _snowMeshFill = Mathf.Clamp01(_snowMeshFill);

        Shader.SetGlobalFloat("_SmoothnessOffset", _smoothnessOffset);
        Shader.SetGlobalFloat("_SnowMeshFill", _snowMeshFill);
    }

    private void OnDisable()
    {
        Shader.SetGlobalFloat("_SmoothnessOffset", 0);
        Shader.SetGlobalFloat("_SnowMeshFill", 0);
    }
}
