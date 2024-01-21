using UnityEngine;

public class WindManipulationTester : MonoBehaviour
{
    [SerializeField]
    private WindCalculator _windCalculator;

    [SerializeField]
    private SnowDistortionContainer _snowDistortion;

    [SerializeField]
    private float _radius;
    public float Radius
    {
        get => _radius;
        set => _radius = value;
    }

    [SerializeField]
    private float _power;
    public float Power
    {
        get => _power;
        set => _power = value;
    }

    [SerializeField]
    private float _angle;
    public float Angle
    {
        get => _angle;
        set => _angle = value;
    }

    private Collider[] _pushedRigidbodies = new Collider[20];

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _windCalculator.AddWindExplosion(transform.position, _power, _radius, Mathf.Cos(Mathf.Deg2Rad * _angle * 0.5f));
            _snowDistortion.AddDeformationOverTime(transform.position, _radius, _power * 0.01f, Mathf.Cos(Mathf.Deg2Rad * _angle * 0.5f), .3f);

            // lets also push rigidbodies just for fun
            var size = Physics.OverlapSphereNonAlloc(transform.position, _radius, _pushedRigidbodies);

            for (int i = 0; i < size; i++)
            {
                var rb = _pushedRigidbodies[i];
                if (rb.gameObject == gameObject) continue;
                if (!rb.attachedRigidbody) continue;
                rb.attachedRigidbody.AddExplosionForce(_power * _power, transform.position, _radius, 1);
            }
        }
    }
}
