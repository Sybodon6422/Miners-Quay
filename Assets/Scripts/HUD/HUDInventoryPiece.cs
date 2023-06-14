using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class HUDInventoryPiece : MonoBehaviour
{
    InventoryItem itemRef;
    public TextMeshProUGUI nameText, ammountText;

    public void Setup(InventoryItem _item)
    {
        itemRef = _item;

        nameText.text = itemRef.itemRef.itemName;
        ammountText.text = itemRef.itemAmmount.ToString();
    }
    public void Clicked()
    {
        HUDManager.I.InventoryItemClicked(itemRef);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
