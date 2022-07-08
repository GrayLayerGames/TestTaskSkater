using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public Vector3 _axis;
    public float _angle;
    private void Update()
    {
        //_angle += Time.deltaTime;
        transform.Rotate(_axis, _angle);
    }
}
