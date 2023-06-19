using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    #region singleton
    private static HUDManager _instance;
    public static HUDManager I { get { return _instance; } }

    [SerializeField] private GameObject furnaceMenu;

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

    void Start()
    {
        CloseAllMenus();
    }

    #region furnace/container
    private Transform furnacePos, playerPos;
    private bool furnaceMenuOpen = false;
    private bool containerMenuOpen = false;
    public void OpenFurnaceMenu(Furnace furnaceRef)
    {
        furnacePos =  furnaceRef.transform;
        playerPos = CharacterController.I.transform;

        if(Vector2.Distance(playerPos.position,furnacePos.position) > 4)
        {
            furnacePos = null;
            playerPos = null;
            furnaceMenuOpen = false;
            return;
        }
        if(containerMenuOpen){ContainerInventoryHUD.I.ToggleInventory(null);}
        furnaceMenuOpen = true;
        furnaceMenu.SetActive(true);
        FurnaceInputProcessor.I.linkedFurnace = furnaceRef;
    }

    public void OpenContainerMenu(StorageContainer containerRef)
    {
        if(furnaceMenuOpen){CloseFurnaceMenu();}
        containerMenuOpen = ContainerInventoryHUD.I.ToggleInventory(containerRef);

    }

    public void CloseFurnaceMenu()
    {
        furnaceMenu.SetActive(false);
        furnaceMenuOpen = false;
    }

    #endregion

    public void CloseAllMenus()
    {
        furnaceMenu.SetActive(false);
        
        furnaceMenuOpen = false;
        containerMenuOpen = false;
    }

    public void InventoryItemClicked(InventoryItem _item)
    {
        if(furnaceMenuOpen) 
        {
            FurnaceInputProcessor.I.AddItemToFurance(_item);
        }  else if(containerMenuOpen)
        {
            ContainerInventoryHUD.I.ContainerRef.inventory.AddToInventory(_item);
            CharacterController.I.inventory.RemoveItem(_item);
        }
    }

    [SerializeField] private RectTransform oxygenBarRect;
    public void UpdateOxygenBar(float _oxygen,float maxOxygen)
    {
        //scale oxygen float to 0,1
        float oxygenScaled = _oxygen / maxOxygen;
        oxygenBarRect.sizeDelta = new Vector2(oxygenScaled*100,20);
    }

    private void FixedUpdate()
    {
        if(!furnaceMenuOpen){return;}
        if(Vector2.Distance(playerPos.position,furnacePos.position) > 4)
        {
            CloseFurnaceMenu();
        }
    }
}
