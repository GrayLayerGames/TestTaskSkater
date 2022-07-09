using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TapToStart : MonoBehaviour
{
    [SerializeField] private Button _tapArea;
    private LevelManager _levelManager;

    [Zenject.Inject]
    public void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }
    private void Start()
    {
        _tapArea.onClick.AddListener(() =>
        {
            _levelManager.ActivateGameplay(true);
            Destroy(gameObject);
        });
    }

    private void OnDestroy()
    {
        _tapArea.onClick.RemoveAllListeners();
    }
}
