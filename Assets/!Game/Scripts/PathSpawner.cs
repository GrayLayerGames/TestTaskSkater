using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    [SerializeField] private float _maxSpawnDistance;
    [SerializeField] private float _distanceBetweenPlatforms = 0.25f;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private Joystick _joystick;
    private bool IsJoystickPressed => _joystick.transform.GetChild(0).gameObject.activeInHierarchy;
    private bool isSpawning;

    void Update()
    {
        if (IsJoystickPressed && !isSpawning && _playerMovement.inventoryTransform.childCount > 0)
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
        if (playerMovement.inventoryTransform.childCount == 0) return null;
        Transform plTransf = playerMovement.inventoryTransform.GetChild(playerMovement.inventoryTransform.childCount - 1);
        plTransf.SetParent(null, false);
        //plTransf.position = _platformPlacementSpot.position;
        plTransf.GetComponent<Collider>().isTrigger = false;
        plTransf.GetComponent<Platform>().platformState = PlatformState.PLACED;
        return plTransf;
    }
}
