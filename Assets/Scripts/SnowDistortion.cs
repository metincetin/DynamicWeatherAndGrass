using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowDistortion : MonoBehaviour
{
    [SerializeField]
    private float _radius;
    public float Radius => _radius;

    [SerializeField]
    private float _heightOffset;
    public float HeightOffset => _heightOffset;


    private void OnEnable()
    {
        SnowDistortionContainer.AddDistorter(this);
    }
    private void OnDisable()
    {
        SnowDistortionContainer.RemoveDistorter(this);
    }
}
