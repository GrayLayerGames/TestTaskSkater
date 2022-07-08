using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkipAds : MonoBehaviour
{
    [SerializeField] private Button _button;

    private void Start()
    {
        _button.onClick.AddListener(() => transform.DOScale(0f, 0.5f).From(1f).OnComplete(() =>
        {
            _button.onClick.RemoveAllListeners();
            Destroy(gameObject);
        }));
    }

}
