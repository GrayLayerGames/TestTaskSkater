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
    [SerializeField] private ParticleSystem _smoke;
    [SerializeField] private TriggerEventsSender _groundTriggerSender;
    public Inventory inventory;
    public Transform _platformPlacementSpot;
    [SerializeField] private TriggerEventsSender _triggerEventsSender;

    [SerializeField] private RigidBodyStabilizer _rigidBodyStabilizer;
    [SerializeField] private bool isMoving;
    public bool IsMoving
    {
        get => isMoving;
        set
        {
            isMoving = value;
            if (isMoving)
            {
                _rigidbody.isKinematic = false;
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.isKinematic = true;
            }
        }
    }


    private float _inventoryPlatformSideShift = 0.45f;
    private float _inventoryPlatformUpShift = 0.15f;

    [Zenject.Inject]
    public void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    private void Awake()
    {
        _groundTriggerSender.TriggerCallback += GroundEventUpdater;
        _triggerEventsSender.TriggerCallback += TriggerCallback;
        SmokeEffect(false);
    }

    private void GroundEventUpdater(TriggerEventsSender.EventType eventType, Collider collider)
    {
        if (eventType == TriggerEventsSender.EventType.EXIT)
        {
            _isGrounded = false;
            return;
        }
        _isGrounded = true;
    }

    private void TriggerCallback(TriggerEventsSender.EventType eventType, Collider collider)
    {

        if (eventType == TriggerEventsSender.EventType.ENTER && collider.TryGetComponent<Platform>(out Platform platform))
        {
            if (platform.platformState != PlatformState.COLLECTIBLE) return;
            platform.platformState = PlatformState.PICKED;
            platform.transform.SetParent(inventory.transform, false);
            int sibInd = platform.transform.GetSiblingIndex();
            platform.transform.rotation = inventory.transform.rotation;
            platform.transform.localPosition = Vector3.zero + (Vector3.left * (IsEven(sibInd) ? _inventoryPlatformSideShift : -_inventoryPlatformSideShift)) + (Vector3.up * Mathf.Floor(sibInd / 2) * _inventoryPlatformUpShift);
        }
    }

    private void SmokeEffect(bool on)
    {
        if (on)
            _smoke.Play();
        else
            _smoke.Stop();
    }

    private bool IsEven(int x) => Mathf.Abs(x) % 2 == 0;

    [SerializeField] private float _stuckTime = 2f;
    private float _stuckCounter;
    private LevelManager _levelManager;

    private void FixedUpdate()
    {
        //stuck check
        if (_rigidbody.velocity.magnitude < 0.5f && isMoving)
        {
            _stuckCounter += Time.fixedDeltaTime;
            if (_stuckCounter >= _stuckTime) _levelManager.PlayerDies("Looks like you're stuck");
        }
        else
            _stuckCounter = 0f;

        if (!isMoving) return;
        SmokeEffect(_isGrounded);
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


    private void OnDestroy()
    {
        _triggerEventsSender.TriggerCallback -= TriggerCallback;
    }
}
