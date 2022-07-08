using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class AdsController : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private VideoPlayer _player;
    public event Action OnVideoEnds;
    public void ShowAds()
    {
        _view.SetActive(true);
        _player.loopPointReached += VideoEnds;
    }

    private void VideoEnds(VideoPlayer source)
    {
        _player.loopPointReached -= VideoEnds;
        _view.SetActive(false);
        OnVideoEnds?.Invoke();
    }


}
