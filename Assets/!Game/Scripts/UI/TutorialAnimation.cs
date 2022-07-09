using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TutorialAnimation : MonoBehaviour
{
    [SerializeField] private Transform _hand;

    void Start()
    {
        _hand.DOLocalMove(_hand.transform.localPosition + _hand.transform.up * 100f, 1f)
             .SetEase(Ease.InOutCubic)
             .SetLoops(-1, LoopType.Yoyo)
             .SetId(this);
    }
    private void OnDisable()
    {
        DOTween.Kill(this);
    }

}
