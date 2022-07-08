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
        _showAdsButton.onClick.AddListener(() =>
        {
            _showAdsButton.onClick.RemoveAllListeners();
            _uiManager.adsController.ShowAds();
            _uiManager.adsController.OnVideoEnds += () => { SceneManager.LoadScene("GameScene2"); };
        });
    }
}
