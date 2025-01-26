using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DywFunctions;

public class Inventory : MonoBehaviour
{
    private readonly List<Item> _items = new();

    public GameObject bubbleWhite, bubbleBlue, bubbleRed;

    public static Inventory Current;
    public ItemEvent OnItemAdd;
    public ItemEvent OnItemUse;

    int currentIDCount = 0;

    public List<Item> Items => _items;

    private void Awake()
    {
        Current = this;
    }

    private void Update()
    {
        // _items.Show();
    }


    public void AddItem(Item item)
    {
        var itemInStock = Items.FirstOrDefault(itemInStock => itemInStock.Scheme.Name == item.Scheme.Name);

        item.IdInInventory = currentIDCount++;
        Items.Add(item);
        OnItemAdd?.Invoke(item);
    }

    public void UseItem(string itemName)
    {
        var item = Items.FirstOrDefault(itemInStock => itemInStock.Scheme.Name == itemName);
        Items.Remove(item);
        OnItemUse?.Invoke(item);
        UIInventoryModule.Current.UpdateModule();
    }

    public void UseItem(Item item)
    {
        UseItem(item.Scheme.Name);
    }

    public int GetQuantity(ItemScheme itemScheme)
    {
        return Items.Where(item => item.Scheme.Name == itemScheme.Name).Count();
    }

}
