using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformState platformState;
    public void InitDestruction()
    {
        StartCoroutine(DestructionCoroutine());
    }

    private IEnumerator DestructionCoroutine()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }
}

public enum PlatformState { COLLECTIBLE, PICKED, PLACED }
