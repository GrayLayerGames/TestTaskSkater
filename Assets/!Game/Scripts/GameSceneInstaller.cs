using UnityEngine;
using Zenject;

public class GameSceneInstaller : MonoInstaller
{
    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PathSpawner _pathSpawner;
    [SerializeField] private Joystick _joystick;
    [SerializeField] private UIManager _uiManager;

    public override void InstallBindings()
    {
        Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle();
        Container.Bind<PlayerMovement>().FromInstance(_playerMovement).AsSingle();
        Container.Bind<PathSpawner>().FromInstance(_pathSpawner).AsSingle();
        Container.Bind<Joystick>().FromInstance(_joystick).AsSingle();
        Container.Bind<UIManager>().FromInstance(_uiManager).AsSingle();
    }
}