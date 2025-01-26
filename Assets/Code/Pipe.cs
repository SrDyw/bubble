using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pipe : Vulkan
{
    public Tween _shotTween;
    [SerializeField] private float _shotForce = 1f;
    [SerializeField] private float shotTime = 0.3f;
    [SerializeField] private Ease _shotEase;
    private Vector3 _defaultPosition;
    public override void Start()
    {
        base.Start();
        _defaultPosition = transform.position;
    }
    public override void Pawn()
    {
        base.Pawn();

        _shotTween = transform.DOMove(_defaultPosition - transform.up * _shotForce, shotTime)
            .SetEase(_shotEase)
            .OnComplete(() =>
            {
                transform.DOMove(_defaultPosition, 0.5f);
            });
    }

    private void OnDestroy()
    {
        _shotTween?.Kill();
    }
}
