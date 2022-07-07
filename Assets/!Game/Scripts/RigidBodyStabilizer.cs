using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyStabilizer : MonoBehaviour
{
    [SerializeField] private float stabilityWobble = 0.3f;
    [SerializeField] private float stabilizationSpeed = 1.0f;
    [SerializeField] private Rigidbody _rigidbody;

    public void On() => _isOn = true;
    public void Off() => _isOn = false;
    private bool _isOn;

    private void FixedUpdate()
    {
        if (_isOn)
        {
            Vector3 predictedUp = Quaternion.AngleAxis(
                     _rigidbody.angularVelocity.magnitude * Mathf.Rad2Deg * stabilityWobble / stabilizationSpeed,
                     _rigidbody.angularVelocity
                 ) * transform.up;
            Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
            _rigidbody.AddTorque(torqueVector * stabilizationSpeed * stabilizationSpeed);
        }
    }
}
