using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
public class UIItemSlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _amountTMP;
    private Item _currentItem;
    private int slotIndex = 0;
    private UIInventoryModule _uiInventoryModule;

    public Item CurrentItem { get => _currentItem; set => _currentItem = value; }
    public UIInventoryModule UiInventoryModule { get => _uiInventoryModule; set => _uiInventoryModule = value; }
    public int SlotIndex { get => slotIndex; set => slotIndex = value; }

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

    public void OnPointerClick(PointerEventData eventData)
    {
        print("Asdasd");
        if (CurrentItem)
        {
            UiInventoryModule.SelectSlotAndUpdateCursor(SlotIndex);
        }
        else
        {
            print("Clicked Empty Slot");
        }
    }
}
