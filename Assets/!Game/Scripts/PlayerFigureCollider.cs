using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFigureCollider : MonoBehaviour
{
    private LevelManager _levelManager;

    [Zenject.Inject]
    public void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }
    private void OnTriggerEnter(Collider other)
    {
        _levelManager.PlayerDies("You hit something)");
    }
}
