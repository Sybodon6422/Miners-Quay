using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    private List<InventoryItem> inventoryItems;

    public void InitializeInventory()
    {
        inventoryItems = new List<InventoryItem>();
    }
    public List<InventoryItem> GetInventory(){return inventoryItems;}
    public void AddToInventory(ItemSO itemSO, int quantity)
    {
        if (HasItem(itemSO))
        {
            ItemMatch(itemSO).itemAmmount += quantity;
            return;
        }

        InventoryItem newItem = new InventoryItem(itemSO, quantity);
        inventoryItems.Add(newItem);
    }

    public InventoryItem ItemMatch(ItemSO itemSO)
    {
        foreach (InventoryItem item in inventoryItems)
        {
            if (item.itemRef == itemSO)
            {
                return item;
            }
        }
        Debug.Log("Item was not found!");
        return null;
    }

    public bool HasItem(ItemSO itemSO)
    {
        foreach (InventoryItem item in inventoryItems)
        {
            if (item.itemRef == itemSO)
            {
                return true;
            }
        }

        return false;
    }
}
