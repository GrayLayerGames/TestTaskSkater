using System;

using UnityEngine;
/// <summary>
/// Component for sending collision callbacks to other objects
/// </summary>
public class TriggerEventsSender : MonoBehaviour
{
    public LayerMask layerMask;
    public event Action<EventType, Collider> TriggerCallback;
    public event Action<EventType, Collision> CollisionCallback;
    private void OnTriggerEnter(Collider collider)
    {
        if (!IsInLayerMask(collider.gameObject.layer, layerMask)) return;
        TriggerCallback?.Invoke(EventType.ENTER, collider);
    }

    private void OnTriggerStay(Collider collider)
    {
        if (!IsInLayerMask(collider.gameObject.layer, layerMask)) return;
        TriggerCallback?.Invoke(EventType.STAY, collider);
    }

    private void OnTriggerExit(Collider collider)
    {
        if (!IsInLayerMask(collider.gameObject.layer, layerMask)) return;
        TriggerCallback?.Invoke(EventType.EXIT, collider);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!IsInLayerMask(collision.gameObject.layer, layerMask)) return;
        CollisionCallback?.Invoke(EventType.ENTER, collision);
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!IsInLayerMask(collision.gameObject.layer, layerMask)) return;
        CollisionCallback?.Invoke(EventType.STAY, collision);
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!IsInLayerMask(collision.gameObject.layer, layerMask)) return;
        CollisionCallback?.Invoke(EventType.EXIT, collision);
    }

    public enum EventType { ENTER, STAY, EXIT }

    private bool IsInLayerMask(int layer, LayerMask layermask)
    {
        return layermask == (layermask | (1 << layer));
    }
}
