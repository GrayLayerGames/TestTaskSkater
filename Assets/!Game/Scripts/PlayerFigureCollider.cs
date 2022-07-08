using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigureCollider : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    [Zenject.Inject]
    public void Construct(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }
}
