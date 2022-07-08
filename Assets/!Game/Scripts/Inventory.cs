using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public event Action<int> OnPlatformsCountChanged;
    public int CurrentPlatformsCount => transform.childCount;
    public Platform GetPlatform() => transform.GetChild(CurrentPlatformsCount - 1).GetComponent<Platform>();
    private void OnTransformChildrenChanged() => OnPlatformsCountChanged?.Invoke(transform.childCount);
}
