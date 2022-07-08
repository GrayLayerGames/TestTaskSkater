using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlatformsCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text _counterText;
    private PlayerMovement _playerMovement;

    [Zenject.Inject]
    public void Construct(PlayerMovement playerMovement)
    {
        _playerMovement = playerMovement;
        _playerMovement.inventory.OnPlatformsCountChanged += PlatformsCountChanged;
    }

    private void PlatformsCountChanged(int newCount)
    {
        _counterText.text = newCount.ToString("000");
    }
}
