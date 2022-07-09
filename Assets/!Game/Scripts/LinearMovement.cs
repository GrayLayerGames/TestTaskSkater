using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class LinearMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform[] _movementPoints;
    private int _currentPointIndex;

}
