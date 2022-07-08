using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CountDown _countDown;
    [SerializeField] private Scrollbar _distanceScroll;
    [SerializeField] private WinWindow _winWindow;
    [SerializeField] private LoseWindow _loseWindow;
    public AdsController adsController;
    private LevelManager _levelManager;

    [Zenject.Inject]
    public void Construct(LevelManager levelManager)
    {
        _levelManager = levelManager;
    }

    public async Task StartCountdownAsync(float seconds) => await _countDown.WaitForSecondsAsync(seconds);
    public void ShowWinWindow() => _winWindow.Show();
    public void ShowLoseWindow(string message) => _loseWindow.Show(message);
    private void Update()
    {
        _distanceScroll.size = 1f - _levelManager.DistanceToFinishNormalized;
    }

}
