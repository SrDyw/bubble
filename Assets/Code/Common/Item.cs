using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private ItemScheme _scheme;
    private int _idInInventory = -1;
    private int _stockInInventory = 0;

    public int IdInInventory { get => _idInInventory; set => _idInInventory = value; }
    public ItemScheme Scheme { get => _scheme; set => _scheme = value; }
    // public int StockInInventory { get => _stockInInventory; set => _stockInInventory = value; }

    public virtual void Pick()
    {
        Inventory.Current.AddItem(this);
        gameObject.SetActive(false);
    }
}
