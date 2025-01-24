using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIItemSlot : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _amountTMP;
    private Item _currentItem;

    public Item CurrentItem { get => _currentItem; set => _currentItem = value; }

    private void Start()
    {
        UpdateSlot();
    }

    public bool Assign(Item item)
    {
        if (CurrentItem == null)
        {
            CurrentItem = item;
            UpdateSlot();
            return true;
        }
        return false;
    }

    public void UpdateSlot()
    {
        if (CurrentItem == null || Inventory.Current.GetQuantity(CurrentItem.Scheme) <= 0)
        {
            _itemImage.gameObject.SetActive(false);
            _amountTMP.text = "";
            CurrentItem = null;
            return;
        }

        if (!_itemImage.gameObject.activeSelf) _itemImage.gameObject.SetActive(true);

        _itemImage.sprite = CurrentItem.Scheme.Thumb;
        _amountTMP.text = $"x{Inventory.Current.GetQuantity(CurrentItem.Scheme)}";
    }

    public override string ToString()
    {
        return CurrentItem ? $"@Item {CurrentItem.Scheme.Name} x{Inventory.Current.GetQuantity(CurrentItem.Scheme)}" : "Empty";
    }
}
