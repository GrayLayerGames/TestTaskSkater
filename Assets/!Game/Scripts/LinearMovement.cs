using System.Globalization;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class LinearMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 1f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform[] _movementPoints;
    private int _currentPointIndex;

    private void Start()
    {
        StartMovementAsync(this.GetCancellationTokenOnDestroy());
    }

    public async void StartMovementAsync(CancellationToken cancellationToken = default(CancellationToken))
    {
        if (_movementPoints.Length < 2) throw new System.Exception("Minimum 2 movement points needed");
        _rigidbody.MovePosition(_movementPoints[0].position);
        while (!cancellationToken.IsCancellationRequested)
        {
            int _prevPointIndex = _currentPointIndex;
            _currentPointIndex = (++_currentPointIndex) % _movementPoints.Length;
            float movementDuration = Vector3.Distance(_movementPoints[_prevPointIndex].position, _movementPoints[_currentPointIndex].position) / _movementSpeed;
            await _rigidbody.DOMove(_movementPoints[_currentPointIndex].position, movementDuration)
                            .SetId(this)
                            .SetEase(Ease.InOutCubic)
                            .AsyncWaitForCompletion()
                            .AsUniTask()
                            .AttachExternalCancellation(cancellationToken)
                            .SuppressCancellationThrow();
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(this);
    }
}
