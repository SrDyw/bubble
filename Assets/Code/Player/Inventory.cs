using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DywFunctions;

public class Inventory : MonoBehaviour
{
    private readonly List<Item> _items = new();

    public static Inventory Current;
    public ItemEvent OnItemAdd;

    int currentIDCount = 0;

    private void Awake()
    {
        Current = this;
    }

    private void Update() {
        _items.Show();
    }

    public void AddItem(Item item)
    {
        var itemInStock = _items.FirstOrDefault(itemInStock => itemInStock.Scheme.Name == item.Scheme.Name);

        item.IdInInventory = currentIDCount++;
        _items.Add(item);
        OnItemAdd?.Invoke(item);
    }

    public void UseItem(string itemName)
    {
        var item = _items.FirstOrDefault(itemInStock => itemInStock.Scheme.Name == itemName);
        _items.Remove(item);
    }

    public int GetQuantity(ItemScheme itemScheme)
    {
        return _items.Where(item => item.Scheme.Name == itemScheme.Name).Count();
    }

}
