using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image _targetImage;
    public float initialDeleay;
    public bool _in = false;
    public float _time = 0;
    public string _targetScene;

    private void Start()
    {
        _targetImage.enabled = true;
        _targetImage.DOColor(_in ? Color.black : Color.clear, _time)
            .OnComplete(() =>
            {
                if (_targetScene != "") UnityEngine.SceneManagement.SceneManager.LoadScene(_targetScene);
            })
            .SetDelay(initialDeleay);
    }
}
