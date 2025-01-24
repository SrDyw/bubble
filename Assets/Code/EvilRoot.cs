using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EvilRoot : MonoBehaviour
{
    [SerializeField] private float _timeToGrow = 2f;
    [SerializeField] private Ease _growAnimationEase;
    [SerializeField] private float _animationTime;
    [SerializeField] private Transform[] _roots;

    private Planet _attachedPlanet;
    private bool _isAlive = true;


    private Timer _growTimer;
    private Transform _targetRoot;
    private List<Vector2> _defaultScales = new();
    private int _currentRoot = 0;

    private Tween _currentTween;

    private void Awake()
    {

        for (int i = 0; i < _roots.Length; i++)
        {
            _defaultScales.Add(_roots[i].localScale);
            _roots[i].localScale = Vector3.zero;
        }

        _attachedPlanet = GetComponentInParent<Planet>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _growTimer = Timer.CreateNewTimer(_timeToGrow, gameObject, Grow);
        GameManager.Current.OnGameOver += StopRoots;

    }

    private void OnDestroy()
    {
        GameManager.Current.OnGameOver -= StopRoots;
        _currentTween?.Kill();
    }

    public void StopRoots()
    {
        if (_isAlive)
        {
            _isAlive = false;
            _growTimer.Stop();
        }
    }

    public void Grow()
    {
        if (_isAlive)
        {
            _targetRoot = _roots[_currentRoot];
            _currentTween = _targetRoot.DOScale(_defaultScales[_currentRoot], _animationTime)
                .SetEase(_growAnimationEase);


            _currentRoot++;
            if (_currentRoot < _roots.Length)
            {
                _growTimer.RestartTimer();
            }
        }
    }
}
