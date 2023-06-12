using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUDInventoryPiece : MonoBehaviour
{
    private ItemSO linkedItem;
    private int itemAmmount;

    public TextMeshProUGUI nameText, ammountText;

    public void Setup(InventoryItem _item)
    {
        linkedItem = _item.itemRef;
        itemAmmount = _item.itemAmmount;

        nameText.text = linkedItem.itemName;
        ammountText.text = itemAmmount.ToString();
    }
    public void Clicked()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
