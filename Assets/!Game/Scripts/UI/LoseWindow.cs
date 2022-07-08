using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoseWindow : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private Transform _window;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private Button _retryButton;

    public void Show(string message = "")
    {
        messageText.text = message;
        _view.SetActive(true);
        _window.DOScale(1f, 0.5f).From(0f).SetId(this);
        _retryButton.onClick.AddListener(() =>
        {
            _retryButton.onClick.RemoveAllListeners();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        });
    }
}
