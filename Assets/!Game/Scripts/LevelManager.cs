using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public float DistanceToFinish => Vector3.Distance(_playerMovement.transform.position, _finish.transform.position);
    public float DistanceToFinishNormalized => DistanceToFinish / DistanceToFinishFromStart;
    public float DistanceToFinishFromStart { get; private set; }
    private bool isLost;
    [SerializeField] private Finish _finish;
    private PlayerMovement _playerMovement;
    private PathSpawner _pathSpawner;
    private Joystick _joystick;
    private UIManager _uiManager;

    [Zenject.Inject]
    public void Construct(PlayerMovement playerMovement, PathSpawner pathSpawner, Joystick joystick, UIManager uiManager)
    {
        _playerMovement = playerMovement;
        _pathSpawner = pathSpawner;
        _joystick = joystick;
        _uiManager = uiManager;
    }

    private void Start()
    {
        DistanceToFinishFromStart = DistanceToFinish;
        ActivateGameplay(false);
    }

    public void PlayerDies(string comment = "")
    {
        if (isLost) return;
        isLost = true;
        ActivateGameplay(false);
        _uiManager.ShowLoseWindow(comment);
    }

    public void LevelComplete()
    {
        ActivateGameplay(false);
        _uiManager.ShowWinWindow();
    }


    public void ActivateGameplay(bool on = true)
    {
        _playerMovement.IsMoving = on;
        _pathSpawner.gameObject.SetActive(on);
        _joystick.gameObject.SetActive(on);
    }
}
