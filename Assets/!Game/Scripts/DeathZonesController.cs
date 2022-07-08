using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZonesController : MonoBehaviour
{
    private TriggerEventsSender[] _triggerEventsSenders;
    private LevelManager _levelManager;

    [Zenject.Inject]
    public void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }
    void Start()
    {
        _triggerEventsSenders = GetComponentsInChildren<TriggerEventsSender>(true);
        foreach (var tes in _triggerEventsSenders)
        {
            tes.TriggerCallback += OnZoneCallback;
        }
    }

    private void OnZoneCallback(TriggerEventsSender.EventType eventType, Collider collider)
    {
        if (eventType == TriggerEventsSender.EventType.ENTER && collider.TryGetComponent<PlayerMovement>(out PlayerMovement playerMovement))
        {
            ClearCallbacks();
            _levelManager.PlayerDies("Try not to fall into the abyss");
        }
    }

    private void ClearCallbacks()
    {
        foreach (var tes in _triggerEventsSenders)
        {
            tes.TriggerCallback -= OnZoneCallback;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
