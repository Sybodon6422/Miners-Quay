using TMPro;
using UnityEngine;

public class HUDContainerScript : MonoBehaviour
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
        ContainerInventoryHUD.I.ItemClicked(itemRef);
    }
}
