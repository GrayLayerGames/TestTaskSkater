using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private TriggerEventsSender _triggerEventsSender;
    private LevelManager _levelManager;

    [Zenject.Inject]
    public void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }
    void Start()
    {
        _triggerEventsSender.TriggerCallback += OnTriggered;
    }

    private void OnTriggered(TriggerEventsSender.EventType eventType, Collider collider)
    {
        if (eventType == TriggerEventsSender.EventType.ENTER && collider.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            _levelManager.LevelComplete();
        }
    }

    private void OnDestroy()
    {
        _triggerEventsSender.TriggerCallback -= OnTriggered;
    }
}
