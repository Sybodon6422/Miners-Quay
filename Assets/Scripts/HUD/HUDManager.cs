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

    public void OpenFurnaceMenu(Furnace furnaceRef)
    {
        furnaceMenu.SetActive(true);
        FurnaceInputProcessor.I.linkedFurnace = furnaceRef;
    }

    public void CloseAllMenus()
    {
        furnaceMenu.SetActive(false);
    }

    public void InventoryItemClicked(InventoryItem _item)
    {
        if(furnaceMenu.activeInHierarchy) 
            {
                FurnaceInputProcessor.I.AddItemToFurance(_item);
            }  
        }
}
