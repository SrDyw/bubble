using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SeaCleaner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _seaRenderer;
    [SerializeField] private int _bubblesAmount = 3;
    [SerializeField] private float _colorTransitionTime = 0.5f;
    [SerializeField] private int _cleanTimes;

    private Planet _planet;
    private Tween _mainTween;


    private void Start()
    {
        Inventory.Current.OnItemUse += OnItemUse;
        _planet = GetComponentInParent<Planet>();
    }

    private void OnDestroy()
    {
        Inventory.Current.OnItemUse -= OnItemUse;
        _mainTween?.Kill();
    }

    public void OnItemUse(Item item)
    {
        if (Player.Current.CurrentPlanet == _planet)
        {
            (item as Bubble)?.Dissapear();
        }
    }


    public void Clean()
    {
        _cleanTimes++;
        _mainTween?.Kill();
        _mainTween = _seaRenderer.DOColor(_seaRenderer.color + Color.white * 1 / _bubblesAmount, _colorTransitionTime);

        if (_cleanTimes >= _bubblesAmount) _seaRenderer.color = Color.white;
    }

}
