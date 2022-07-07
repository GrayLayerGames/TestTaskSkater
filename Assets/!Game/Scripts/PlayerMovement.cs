using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 1f;
    [SerializeField] private float _speedLimit = 5f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private bool _isGrounded;
    public Transform inventoryTransform;
    public Transform _platformPlacementSpot;
    [SerializeField] private TriggerEventsSender _triggerEventsSender;

    [SerializeField] private RigidBodyStabilizer _rigidBodyStabilizer;
    private float _inventoryPlatformSideShift = 0.45f;
    private float _inventoryPlatformUpShift = 0.15f;

    private void Awake()
    {
        _triggerEventsSender.Callback += TriggerCallback;
    }

    private void TriggerCallback(TriggerEventsSender.EventType eventType, Collider collider)
    {

        if (eventType == TriggerEventsSender.EventType.ENTER && collider.TryGetComponent<Platform>(out Platform platform))
        {
            if (platform.platformState != PlatformState.COLLECTIBLE) return;
            platform.platformState = PlatformState.PICKED;
            platform.transform.SetParent(inventoryTransform, false);
            int sibInd = platform.transform.GetSiblingIndex();
            platform.transform.rotation = Quaternion.identity;
            platform.transform.localPosition = Vector3.zero + (Vector3.left * (IsEven(sibInd) ? _inventoryPlatformSideShift : -_inventoryPlatformSideShift)) + (Vector3.up * Mathf.Floor(sibInd / 2) * _inventoryPlatformUpShift);
        }
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector3.up * 2f, ForceMode.Impulse);
    }

    private bool IsEven(int x) => Mathf.Abs(x) % 2 == 0;

    private void FixedUpdate()
    {
        if (_isGrounded && _rigidbody.velocity.magnitude < _speedLimit)
        {
            _rigidbody.AddForce(-transform.forward * _playerSpeed * Time.deltaTime);
            _rigidBodyStabilizer.Off();
        }
        else
        {
            _rigidBodyStabilizer.On();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        _isGrounded = false;
    }

    private void OnCollisionStay(Collision other)
    {
        _isGrounded = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        _isGrounded = true;
    }

    private void OnDestroy()
    {
        _triggerEventsSender.Callback -= TriggerCallback;
    }
}
