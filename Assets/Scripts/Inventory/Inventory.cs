using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    private List<InventoryItem> inventoryItems;
    public List<InventoryItem> GetInventory(){return inventoryItems;}

    public void InitializeInventory()
    {
        inventoryItems = new List<InventoryItem>();
    }
    public void AddToInventory(ItemSO itemSO, int quantity)
    {
        if (HasItem(itemSO))
        {
            ItemMatch(itemSO).itemAmmount += quantity;
            TryUpdateInventory();
            return;
        }

        InventoryItem newItem = new InventoryItem(itemSO, quantity);
        inventoryItems.Add(newItem);
        TryUpdateInventory();
    }
    
    public void AddToInventory(InventoryItem item)
    {
        if (HasItem(item.itemRef))
        {
            ItemMatch(item.itemRef).itemAmmount += item.itemAmmount;
            TryUpdateInventory();
            return;
        }else{
            inventoryItems.Add(item);
            TryUpdateInventory();
        }
    }

    public void RemoveItem(InventoryItem itemToRemove)
    {
        inventoryItems.Remove(itemToRemove);
        TryUpdateInventory();
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

    private void TryUpdateInventory(){if(OnInventoryUpdated != null) OnInventoryUpdated();}
    public event Action OnInventoryUpdated;
    public delegate void InventoryUpdatedAction();

}
