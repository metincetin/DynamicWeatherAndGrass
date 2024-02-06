using System.Security.AccessControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public struct WindManipulationData
{
    public Vector3 Position;
    public float Angle;
    public float Arc;
    public Vector2 Power;
    public Vector2 Radius;
    public float Time;
}
public class WindCalculator : MonoBehaviour
{
    [SerializeField]
    private ComputeShader _shader;

    [SerializeField]
    private float _noiseScale;

    [SerializeField]
    private RenderTexture _texture;

    [SerializeField]
    private float _windFrequency;

    [SerializeField]
    private float _windChangePower;

    [SerializeField]
    private float _globalWindPower = 1;

    [SerializeField]
    private AnimationCurve _powerByTime;

    [SerializeField]
    private AnimationCurve _radiusByTime;

    private List<WindManipulationData> _data;
    private ComputeBuffer _windManipulationComputeBuffer;

    [SerializeField]
    private Vector3 _windDirection;
    public Vector3 WindDirection
    {
        get => _windDirection;
        set => _windDirection = value;
    }

    int _dataIndex;

    // Start is called before the first frame update
    void Awake()
    {
        _windManipulationComputeBuffer = new ComputeBuffer(5, 3 * 4 + 4 + 4 + 4 * 2 + 4 * 2 + 4, ComputeBufferType.Structured, ComputeBufferMode.Dynamic);
        _data = new List<WindManipulationData>(new WindManipulationData[5]);
    }

    private void Update()
    {
        UpdateWindManipulationData();
        Dispatch();
        Shader.SetGlobalVector("_WindDirection", _windDirection.normalized);
    }

    private void UpdateWindManipulationData()
    {
        for (int i = 0; i < _data.Count; i++)
        {
            WindManipulationData d = _data[i];
            d.Radius.y = _radiusByTime.Evaluate(d.Time);
            d.Power.y = _powerByTime.Evaluate(d.Time);
            d.Time += Time.deltaTime;
            _data[i] = d;
        }
        _windManipulationComputeBuffer.SetData(_data);
    }

    public void Dispatch()
    {
        var o = RenderTexture.active;

        RenderTexture.active = _texture;

        GL.Clear(true, true, new Color(0, 0, 0, 0));

        RenderTexture.active = o;

        _shader.SetTexture(0, "Result", _texture);
        _shader.SetFloat("_NoiseScale", _noiseScale);
        _shader.SetFloat("_Time", Time.time);
        _shader.SetFloat("_WindFrequency", _windFrequency);
        _shader.SetFloat("_WindChangePower", _windChangePower);
        _shader.SetVector("_WindDirection", _windDirection);

        _shader.SetBuffer(0, "_WindManipulationBuffer", _windManipulationComputeBuffer);

        _shader.Dispatch(0, 8, 8, 1);

        Shader.SetGlobalTexture("_FlowMap", _texture);
        Shader.SetGlobalFloat("_GlobalWindPower", _globalWindPower);
    }

    private void OnDisable()
    {
        _windManipulationComputeBuffer.Release();
        var o = RenderTexture.active;
        RenderTexture.active = _texture;
        GL.Clear(false, true, Color.black);
        RenderTexture.active = o;
    }

    public void AddWindExplosion(Vector3 position, float power, float radius, float arc)
    {
        _data[_dataIndex] = (new WindManipulationData
        {
            Position = position,
            Arc = arc,
            Angle = 0,
            Radius = new Vector2(radius, 0),
            Time = 0,
            Power = new Vector2(power, 0),
        });

        _dataIndex++;
        _dataIndex = _dataIndex % 5;
    }

    public void SetPreset(WindPreset preset)
    {
        _noiseScale = preset.NoiseScale;
        _windFrequency = preset.WindFrequency;
        _globalWindPower = preset.GlobalWindPower;
        _windChangePower = preset.WindChangePower;
    }
}
