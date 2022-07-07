using System.Reflection.Emit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 1f;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Transform _inventoryTransform;
    [SerializeField] private Transform _platformPlacementSpot;
    [SerializeField] private TriggerEventsSender _triggerEventsSender;
    [SerializeField] private Joystick _joystick;
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
            platform.transform.SetParent(_inventoryTransform, false);
            int sibInd = platform.transform.GetSiblingIndex();
            platform.transform.localPosition = Vector3.zero + (Vector3.left * (IsEven(sibInd) ? _inventoryPlatformSideShift : -_inventoryPlatformSideShift)) + (Vector3.up * Mathf.Floor(sibInd / 2) * _inventoryPlatformUpShift);
        }
    }

    private bool IsEven(int x) => Mathf.Abs(x) % 2 == 0;

    private void Update()
    {
        if (_joystick.transform.GetChild(0).gameObject.activeInHierarchy)
            Debug.Log(_joystick.Direction);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Transform plTransf = _inventoryTransform.GetChild(_inventoryTransform.childCount - 1);
            plTransf.SetParent(null, false);
            plTransf.position = _platformPlacementSpot.position;
            plTransf.GetComponent<Collider>().isTrigger = false;
        }
    }

    private void FixedUpdate()
    {
        if (_isGrounded && _rigidbody.velocity.magnitude < 10f)
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
