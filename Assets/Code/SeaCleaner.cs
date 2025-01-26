using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaCleaner : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _seaRenderer;
    [SerializeField] private int _bubblesAmount = 3;
    [SerializeField] private int _cleanTimes;

    private Planet _planet;


    private void Start()
    {
        Inventory.Current.OnItemUse += OnItemUse;
        _planet = GetComponentInParent<Planet>();
    }

    private void OnDestroy()
    {
        Inventory.Current.OnItemUse -= OnItemUse;
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
        _seaRenderer.color += Color.white * 1 / _bubblesAmount;

        if (_cleanTimes >= _bubblesAmount) _seaRenderer.color = Color.white;
    }
}
