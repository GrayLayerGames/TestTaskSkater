using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stack : MonoBehaviour
{
    [SerializeField] private float _rotationSpeedPerPlatform;
    private int _childCount;

    private void Start()
    {
        _childCount = transform.childCount;
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0f, 1f, 0f) * (_rotationSpeedPerPlatform / _childCount));
    }

    private void OnTransformChildrenChanged()
    {
        if (transform.childCount == 0) Destroy(gameObject);
    }
}
