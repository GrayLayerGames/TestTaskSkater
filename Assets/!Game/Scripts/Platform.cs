using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public PlatformState platformState;
}

public enum PlatformState { COLLECTIBLE, PICKED, PLACED }
