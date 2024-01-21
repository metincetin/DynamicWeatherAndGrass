using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody))]
public class BallRoll : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Camera _camera;
    [SerializeField]
    private float _rollPower = 6;

    [SerializeField]
    private float _jumpForce = 5;

    [SerializeField]
    private float _maxVelocity = 10;

    [SerializeField]
    private float _velocityDamping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _camera = Camera.main;
    }

    // Update is called once per frame
    private void Update()
    {
        var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input = Quaternion.Euler(0, _camera.transform.eulerAngles.y, 0) * input;
        _rigidbody.AddForce(input * _rollPower);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        var clampedVelocity = _rigidbody.velocity;
        clampedVelocity.y = 0;
        clampedVelocity = Vector3.ClampMagnitude(clampedVelocity, _maxVelocity);
        clampedVelocity = Vector3.MoveTowards(clampedVelocity, Vector3.zero, _velocityDamping * Time.deltaTime);

        var angularVelocity = _rigidbody.angularVelocity;
        angularVelocity= Vector3.MoveTowards(angularVelocity, Vector3.zero, _velocityDamping * Time.deltaTime);

        clampedVelocity.y = _rigidbody.velocity.y;

        _rigidbody.velocity = clampedVelocity;
        _rigidbody.angularVelocity = angularVelocity;
    }
}
