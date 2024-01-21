using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDistortionContainer : MonoBehaviour
{
    private static List<GrassDistortion> _distorters = new List<GrassDistortion>();

    [SerializeField]
    private RenderTexture _distortionMap;


    private Material _distortionMaterial;


    private RenderTexture _tempDistortion;
    private RenderTexture _tempEmission;


    internal static void AddDistorter(GrassDistortion grassDistortion)
    {
        if (!_distorters.Contains(grassDistortion))
            _distorters.Add(grassDistortion);
    }

    internal static void RemoveDistorter(GrassDistortion grassDistortion)
    {
        _distorters.Remove(grassDistortion);
    }

    private void Awake()
    {
        _distortionMaterial = new Material(Shader.Find("Shader Graphs/Distortion"));

        _tempDistortion = Instantiate(_distortionMap);
        Shader.SetGlobalTexture("_DistortionMap", _distortionMap);
    }

    private void OnDisable()
    {
        ClearTextures();
    }

    private void ClearTextures()
    {
        var t = RenderTexture.active;
        RenderTexture.active = _tempDistortion;
        GL.Clear(false, true, new Color(0.5f, 0.5f,0,0));
        RenderTexture.active = _distortionMap;
        GL.Clear(false, true, new Color(0.5f, 0.5f,0,0));
        RenderTexture.active = t;
    }

    // Update is called once per frame
    private void Update()
    {
        var t = RenderTexture.active;
        RenderTexture.active = _tempDistortion;
        GL.Clear(false, true, new Color(0.5f, 0.5f,0,0));
        RenderTexture.active = t;

        foreach (var d in _distorters)
        {
            _distortionMaterial.SetFloat("_Radius", d.Radius);
            _distortionMaterial.SetVector("_WorldPosition", d.transform.position + Vector3.up * d.HeightOffset);
            Graphics.Blit(null, _tempDistortion, _distortionMaterial, 0);
        }
        Graphics.Blit(_tempDistortion, _distortionMap);
        //Graphics.Blit(_distortionMap, _fadeMaterial);
    }

}
