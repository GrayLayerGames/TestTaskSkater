using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _followTarget;
    [SerializeField] private Vector3 _targetOffset;
    [SerializeField] private float _interpolation;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _followTarget.transform.position + _targetOffset, _interpolation * Time.deltaTime);
    }
}
