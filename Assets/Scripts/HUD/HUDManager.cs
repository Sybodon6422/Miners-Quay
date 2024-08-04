using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField] private Menu plantTravelMenu, shipBuildingMenu;

    #region World management

    [SerializeField] private GameObject planetTravelMenuFab;
    [SerializeField] private Transform planetTravelMenuHolder;

    public void LoadPlanet(CelestialBody planet)
    {
        //load planet
        //load bg
        //set player planet
        //set player gravity
        //set player oxygen
    }

    //TODO: add a way to check what planets are available to travel to based on research and available fuel
    public void OpenWorldsTravelMenu(bool moon)
    {
        //destroy all children
        foreach (Transform child in planetTravelMenuHolder)
        {
            Destroy(child.gameObject);
        }

        if (moon)
        {
            GameObject go = Instantiate(planetTravelMenuFab, planetTravelMenuHolder);
            go.GetComponent<PlanetTravelOption>().SetupTravelOption(GameManager.I.GetBody(0));
        }
        else
        {
            GameObject go = Instantiate(planetTravelMenuFab, planetTravelMenuHolder);
            go.GetComponent<PlanetTravelOption>().SetupTravelOption(GameManager.I.GetBody(1));
        }

        plantTravelMenu.OpenMenu();
    }

    #endregion

    #region furnace/container
    private Transform furnacePos, playerPos;
    private bool furnaceMenuOpen = false;
    private bool containerMenuOpen = false;
    public void OpenFurnaceMenu(Furnace furnaceRef)
    {
        furnacePos =  furnaceRef.transform;
        playerPos = CharacterLocomotion.I.transform;

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

    #region Earth Menus

    public void OpenShipBuildingMenu()
    {
        shipBuildingMenu.OpenMenu();
    }

    public void CloseShipBuildingMenu()
    {
        shipBuildingMenu.CloseMenu();
    }

    #endregion

    public void CloseAllMenus()
    {
        furnaceMenu.SetActive(false);
        plantTravelMenu.CloseMenu();
        shipBuildingMenu.CloseMenu();

        furnaceMenuOpen = false;
        containerMenuOpen = false;
    }

    #region astronaut specific

    public void InventoryItemClicked(InventoryItem _item)
    {
        if(furnaceMenuOpen) 
        {
            FurnaceInputProcessor.I.AddItemToFurance(_item);
        }  else if(containerMenuOpen)
        {
            ContainerInventoryHUD.I.ContainerRef.inventory.AddToInventory(_item);
            CharacterLocomotion.I.inventory.RemoveItem(_item);
        }
    }

    [SerializeField] private RectTransform oxygenBarRect;
    [SerializeField] private TextMeshProUGUI oxygenText;
    public void UpdateOxygenBar(float _oxygen,float maxOxygen)
    {
        //scale oxygen float to 0,1
        oxygenText.text = _oxygen.ToString("F0");
        float oxygenScaled = _oxygen / maxOxygen;
        oxygenBarRect.sizeDelta = new Vector2(oxygenScaled*125,20);
    }

    #endregion

    #region stats

    [SerializeField] private TextMeshProUGUI moneyText;

    public void UpdateMoneyText(int money)
    {
        moneyText.text = "$" + money.ToString();
    }

    #endregion

    private void FixedUpdate()
    {
        if(!furnaceMenuOpen){return;}
        if(Vector2.Distance(playerPos.position,furnacePos.position) > 4)
        {
            CloseFurnaceMenu();
        }
    }
}
