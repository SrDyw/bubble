using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Planet : MonoBehaviour
{
    [SerializeField] private PlanetScheme _scheme;
    [SerializeField] private GameObject _visual;
    [SerializeField] private GameObject[] _arrows;

    [Space]
    [Header("Effects")]
    [SerializeField] private Ease _easeInStep, _easeOutStep;

    private Tween _surfaceTween;
    private Vector2 _defaultPosition;


    private void Awake()
    {
        foreach (var exit in _arrows)
        {
            exit.gameObject.SetActive(false);
        }
    }
    private void Start()
    {
        _defaultPosition = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SurfaceStep(Vector3.up, 0.5f);
        }
    }

    public void SurfaceStep(Vector3 stepDirection, float force = 1)
    {
        if (_surfaceTween?.active == true) return;
        var effectTime = 0.15f;
        Kill(_surfaceTween);
        var inverseDirection = -(stepDirection.normalized / Scheme.Weight) * force;

        _surfaceTween = _visual.transform.DOMove(_visual.transform.position + inverseDirection, effectTime)
            .OnComplete(() =>
            {
                _surfaceTween = _visual.transform.DOMove(_defaultPosition, effectTime)
                    .OnComplete(() =>
                    {
                        Kill(_surfaceTween);
                    });
            });
    }

    private void OnDestroy()
    {
        Kill(_surfaceTween);
    }

    public void FreeExit(string name)
    {
        foreach (var exit in _arrows)
        {
            if (exit.name == name)
            {
                exit.gameObject.SetActive(true);
                return;
            }
        }
    }

    private void Kill(Tween tween)
    {
        tween?.Kill();
        tween = null;
    }
    public PlanetScheme Scheme { get => _scheme; set => _scheme = value; }
}
