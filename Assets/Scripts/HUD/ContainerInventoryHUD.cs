using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerInventoryHUD : HUDMenu
{
    #region singleton
    private static ContainerInventoryHUD _instance;
    public static ContainerInventoryHUD I { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }



    #endregion

    [SerializeField] private Transform holder;
    [SerializeField] private GameObject inventoryFab;
    private StorageContainer containerRef;
    public StorageContainer ContainerRef { get { return containerRef; } }
    void Start(){gameObject.SetActive(false);}

    private bool isInventoryOpen = false;
    public bool IsInventoryOpen { get { return isInventoryOpen; } }

    public bool ToggleInventory(StorageContainer container)
    {
        containerRef = container;

        gameObject.SetActive(!gameObject.activeInHierarchy);
        if(gameObject.activeInHierarchy){
            containerRef.inventory.OnInventoryUpdated += UpdateLinkedInventory;
            UpdateLinkedInventory();
            return true;
        }
        else
        {
            if(containerRef == null){return false;}
            containerRef.inventory.OnInventoryUpdated -= UpdateLinkedInventory;
            return false;
        }
    }

    public void ItemClicked(InventoryItem itemRef)
    {
        CharacterController.I.inventory.AddToInventory(itemRef);
        containerRef.inventory.RemoveItem(itemRef);
    }

    public void UpdateLinkedInventory()
    {
        List<InventoryItem> inventoryItems = containerRef.inventory.GetInventory();
        for (int i = 0; i < holder.childCount; i++)
        {
            Destroy(holder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            var go = Instantiate(inventoryFab,holder);
            go.GetComponent<HUDContainerScript>().Setup(inventoryItems[i]);
        }
    }
}
