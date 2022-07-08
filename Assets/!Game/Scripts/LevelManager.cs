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

    private async void Start()
    {
        DistanceToFinishFromStart = DistanceToFinish;
        ActivateGameplay(false);
        await _uiManager.StartCountdownAsync(3);
        ActivateGameplay(true);
    }

    public void PlayerDies(string comment = "")
    {
        ActivateGameplay(false);
        Debug.Log(comment);
    }

    public void LevelComplete()
    {
        ActivateGameplay(false);
        // TODO: Show win dialogue
    }


    public void ActivateGameplay(bool on = true)
    {
        _playerMovement.IsMoving = on;
        _pathSpawner.gameObject.SetActive(on);
        _joystick.gameObject.SetActive(on);
    }
}
