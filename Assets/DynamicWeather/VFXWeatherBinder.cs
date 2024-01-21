using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXWeatherBinder : MonoBehaviour
{
    private UnityEngine.VFX.VisualEffect _effect;

    [SerializeField]
    private WeatherController _weatherController;

    private void Awake()
    {
        _effect = GetComponent<UnityEngine.VFX.VisualEffect>();
    }

    // Update is called once per frame
    private void Update()
    {
        _effect.SetFloat("_GlobalWindPower", Shader.GetGlobalFloat("_GlobalWindPower"));
        if (_effect.HasFloat("_Rain"))
            _effect.SetFloat("_Rain", _weatherController.Rain);
        if (_effect.HasFloat("_Snow"))
            _effect.SetFloat("_Snow", _weatherController.Snow);
    }
}
