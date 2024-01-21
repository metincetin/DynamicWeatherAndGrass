using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Wind/Preset")]
public class WindPreset : ScriptableObject
{
    [SerializeField]
    private float _noiseScale;
    public float NoiseScale => _noiseScale;

    [SerializeField]
    private float _windFrequency;
    public float WindFrequency => _windFrequency;

    [SerializeField]
    private float _windChangePower;
    public float WindChangePower => _windChangePower;

    [SerializeField]
    private float _globalWindPower = 1;
    public float GlobalWindPower => _globalWindPower;
}
