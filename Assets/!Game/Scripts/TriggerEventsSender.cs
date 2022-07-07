using System;

using UnityEngine;
/// <summary>
/// Component for sending collision callbacks to other objects
/// </summary>
public class TriggerEventsSender : MonoBehaviour
{
    public LayerMask layerMask;
    public event Action<EventType, Collider> Callback;
    private void OnTriggerEnter(Collider collider)
    {
        if (!IsInLayerMask(collider.gameObject.layer, layerMask)) return;
        Callback?.Invoke(EventType.ENTER, collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!IsInLayerMask(collider.gameObject.layer, layerMask)) return;
        Callback?.Invoke(EventType.STAY, collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!IsInLayerMask(collider.gameObject.layer, layerMask)) return;
        Callback?.Invoke(EventType.EXIT, collider);
    }

    public enum EventType { ENTER, STAY, EXIT }

    private bool IsInLayerMask(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
}
