using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private Transform _window;
    [SerializeField] private Button _showAdsButton;
    [SerializeField] private Button _skipAdsButton;
    private int _scenesCount = 3;
    private UIManager _uiManager;

    [Zenject.Inject]
    public void Construct(UIManager uiManager)
    {
        _uiManager = uiManager;
    }
    public void Show()
    {
        _view.SetActive(true);
        _window.DOScale(1f, 0.5f).From(0f).SetId(this);
        string nextSceneName = "GameScene" + (((SceneManager.GetActiveScene().buildIndex + 1) % _scenesCount) + 1);
        Debug.Log(nextSceneName);
        _showAdsButton.onClick.AddListener(() =>
        {
            _uiManager.adsController.ShowAds();
            _uiManager.adsController.OnVideoEnds += () => { SceneManager.LoadScene(nextSceneName); };
        });
        _skipAdsButton.onClick.AddListener(() => SceneManager.LoadScene(nextSceneName));
    }

    private void OnDestroy()
    {
        _showAdsButton.onClick.RemoveAllListeners();
        _skipAdsButton.onClick.RemoveAllListeners();
    }
}
