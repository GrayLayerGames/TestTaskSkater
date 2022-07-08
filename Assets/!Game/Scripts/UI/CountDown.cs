using System.Runtime.CompilerServices;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private GameObject _view;
    [SerializeField] private TMP_Text _counterText;
    private bool _isCounting;
    public async Task WaitForSecondsAsync(float seconds, CancellationToken cancellationToken = default(CancellationToken))
    {
        if (_isCounting) throw new Exception("Countdown already started");
        float secondsLeft = seconds;
        _isCounting = true;
        _view.SetActive(true);
        while (secondsLeft >= 0f && !cancellationToken.IsCancellationRequested)
        {
            secondsLeft -= Time.deltaTime;
            await Task.Delay(Mathf.FloorToInt(Time.deltaTime * 1000), cancellationToken);
            _counterText.text = Mathf.CeilToInt(secondsLeft).ToString();
        }
        _counterText.text = "GO!";
        await Task.Delay(500);
        _isCounting = false;
        _view.SetActive(false);
    }
}
