using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassDistortion : MonoBehaviour
{
    [SerializeField]
    private float _radius;
    public float Radius => _radius;

    [SerializeField]
    private float _heightOffset;
    public float HeightOffset => _heightOffset;


    private void OnEnable()
    {
        GrassDistortionContainer.AddDistorter(this);
    }
    private void OnDisable()
    {
        GrassDistortionContainer.RemoveDistorter(this);
    }
}
