using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    [SerializeField] private float _maxSpawnDistance;
    [SerializeField] private float _distanceBetweenPlatforms = 0.25f;
    private Joystick _joystick;
    private PlayerMovement _playerMovement;
    private bool IsJoystickPressed => _joystick.transform.GetChild(0).gameObject.activeInHierarchy;
    private bool isSpawning;

    [Zenject.Inject]
    public void Construct(PlayerMovement playerMovement, Joystick joystick)
    {
        _playerMovement = playerMovement;
        _joystick = joystick;
    }

    void Update()
    {
        if (IsJoystickPressed && !isSpawning && _playerMovement.inventory.CurrentPlatformsCount > 0)
        {
            isSpawning = true;
            StartCoroutine(SpawnPlatforms());
        }
        if (!IsJoystickPressed)
        {
            isSpawning = false;
            StopAllCoroutines();
        }
    }

    private IEnumerator SpawnPlatforms()
    {
        _playerMovement.Jump();
        Vector3 spawnPos = _playerMovement._platformPlacementSpot.position;
        Vector3? prevSpawnPos = null;
        while (true)
        {
            if (Vector3.Distance(_playerMovement.transform.position, spawnPos) < _maxSpawnDistance)
            {
                Transform spawnedPlatform = GetPlatformFromInventory(_playerMovement);
                if (spawnedPlatform == null)
                {
                    isSpawning = false;
                    yield break;
                }
                spawnedPlatform.gameObject.GetComponent<Platform>().InitDestruction();
                spawnedPlatform.position = spawnPos;
                if (prevSpawnPos != null)
                {
                    Quaternion rot = new Quaternion();
                    rot.SetLookRotation((spawnedPlatform.position - (Vector3)prevSpawnPos));
                    spawnedPlatform.rotation = rot;
                }
                prevSpawnPos = spawnPos;
                spawnPos.z -= _distanceBetweenPlatforms;
                spawnPos.y += _joystick.Direction.y / 10f;
            }
            yield return new WaitForEndOfFrame();
        }

    }

    public Transform GetPlatformFromInventory(PlayerMovement playerMovement)
    {
        if (playerMovement.inventory.CurrentPlatformsCount == 0) return null;
        Platform platform = playerMovement.inventory.GetPlatform();
        Transform plTransf = platform.transform;
        plTransf.SetParent(null, false);
        //plTransf.position = _platformPlacementSpot.position;
        plTransf.GetComponent<Collider>().isTrigger = false;
        platform.platformState = PlatformState.PLACED;
        return plTransf;
    }
}
