using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    private TMP_Text _text;
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        Application.targetFrameRate = 60;
        StartCoroutine(FPSUpdater());
    }

    private IEnumerator FPSUpdater()
    {
        while (true)
        {
            _text.text = (1f / Time.deltaTime).ToString("0");
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
