using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainPlane : MonoBehaviour
{
    [SerializeField]
    private WeatherController _weatherController;

    [SerializeField]
    private Vector2 _heightRange;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(0, Mathf.Lerp(_heightRange.x, _heightRange.y, _weatherController.RainPuddle), 0);
    }
}
