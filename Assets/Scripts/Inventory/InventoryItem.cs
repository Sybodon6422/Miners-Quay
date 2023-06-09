using System;

[Serializable]
public class InventoryItem
{
    public ItemSO itemRef;
    public int itemAmmount;

    public InventoryItem(ItemSO _itemRef, int _itemAmmount)
    {
        itemRef = _itemRef;
        itemAmmount = _itemAmmount;
    }
}
