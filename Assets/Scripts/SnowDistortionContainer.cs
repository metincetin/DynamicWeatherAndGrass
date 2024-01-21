using System.Reflection.Emit;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowDistortionContainer : MonoBehaviour
{
    private static List<SnowDistortion> _distorters = new List<SnowDistortion>();

    [SerializeField]
    private RenderTexture _distortionMap;

    private struct DeformationOverTime
    {
        public float Time;
        public Vector3 Position;
        public float Radius;
        public float Power;
        public float Angle;
    }

    private Material _distortionMaterial;
    private Material _fadeMaterial;
    private RenderTexture _tempDistortion;
    private RenderTexture _tempDistortionB;

    [SerializeField]
    private WeatherController _weatherController;
    private bool _distortionMapCleared;

    [SerializeField]
    private float _snowFillDuration;

    private List<DeformationOverTime> _deformationsOverTime = new List<DeformationOverTime>();

    internal static void AddDistorter(SnowDistortion distortion)
    {
        if (!_distorters.Contains(distortion))
            _distorters.Add(distortion);
    }

    internal static void RemoveDistorter(SnowDistortion distortion)
    {
        _distorters.Remove(distortion);
    }

    private void Awake()
    {
        _distortionMaterial = new Material(Shader.Find("Shader Graphs/SnowDistortion"));
        _fadeMaterial = new Material(Shader.Find("Shader Graphs/SnowFade"));

        _tempDistortion = new RenderTexture(_distortionMap);
        _tempDistortionB = new RenderTexture(_tempDistortion);
        Shader.SetGlobalTexture("_SnowDistortionMap", _distortionMap);
    }

    private void OnDisable()
    {
        ClearDistortionMap();
    }

    public void ClearDistortionMap()
    {
        var t = RenderTexture.active;
        RenderTexture.active = _distortionMap;
        GL.Clear(false, true, new Color(0, 0, 0, 0));
        RenderTexture.active = _tempDistortion;
        GL.Clear(false, true, new Color(0, 0, 0, 0));
        RenderTexture.active = _tempDistortionB;
        GL.Clear(false, true, new Color(0, 0, 0, 0));
        RenderTexture.active = t;
    }

    public void AddDeformation(Vector3 position, float radius, float power, float angle)
    {
        _distortionMaterial.SetFloat("_Radius", radius);
        _distortionMaterial.SetVector("_WorldPosition", position);
        _distortionMaterial.SetFloat("_DeformationPower", power);
        _distortionMaterial.SetFloat("_Angle", angle);
        Graphics.Blit(_tempDistortion, _tempDistortionB);
        Graphics.Blit(_tempDistortionB, _tempDistortion, _distortionMaterial, 0);
        Graphics.Blit(_tempDistortion, _distortionMap);
    }

    // Update is called once per frame
    private void Update()
    {
        if (_weatherController.SnowMeshFill > 0)
        {
            _distortionMapCleared = false;
        }
        else if (!_distortionMapCleared)
        {
            _distortionMapCleared = true;
            ClearDistortionMap();
            _deformationsOverTime.Clear();
        }
        if (_weatherController.SnowMeshFill > 0.3f)
        {

            ApplyDeformationsOverTime();
            Graphics.Blit(_distortionMap, _tempDistortion);
            foreach (var d in _distorters)
            {
                float verticalDistance = Mathf.Abs(d.transform.position.y - d.HeightOffset);
                float heightMultiplier = 1 - Mathf.Clamp(verticalDistance, 0, 1);
                _distortionMaterial.SetFloat("_Radius", d.Radius * heightMultiplier);
                _distortionMaterial.SetVector("_WorldPosition", d.transform.position + Vector3.up * d.HeightOffset);
                _distortionMaterial.SetFloat("_DeformationPower", 1);
                _distortionMaterial.SetFloat("_Angle", -1);
                Graphics.Blit(_tempDistortion, _tempDistortionB);
                Graphics.Blit(_tempDistortionB, _tempDistortion, _distortionMaterial, 0);
            }
            Graphics.Blit(_tempDistortion, _distortionMap);

            _fadeMaterial.SetFloat("_SnowFillSpeed", _snowFillDuration * _weatherController.Snow);

            Graphics.Blit(null, _distortionMap, _fadeMaterial, 0);
        }
    }

    private void ApplyDeformationsOverTime()
    {
        for (int i = _deformationsOverTime.Count - 1; i >= 0; i--)
        {
            DeformationOverTime d = _deformationsOverTime[i];
            AddDeformation(d.Position, d.Radius, d.Power, d.Angle);
            d.Time -= Time.deltaTime;
            if (d.Time <= 0)
            {
                _deformationsOverTime.RemoveAt(i);
            }
            else
            {
                _deformationsOverTime[i] = d;
            }
        }
    }

    public void AddDeformationOverTime(Vector3 position, float radius, float power, float angle, float time)
    {
        _deformationsOverTime.Add(new DeformationOverTime
        {
            Position = position,
            Radius = radius,
            Power = power,
            Angle = angle,
            Time = time
        });
    }
}
