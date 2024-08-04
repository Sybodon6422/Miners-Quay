using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    #region singleton
    private static InventoryManager _instance;
    public static InventoryManager I { get { return _instance; } }

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
    [SerializeField] GameObject inventoryFab;
    [SerializeField] Transform holder;
    void Start(){gameObject.SetActive(false);}
    bool thisIsActive = false;

    public void ToggleInventory()
    {
        gameObject.SetActive(!thisIsActive);
        thisIsActive = !thisIsActive;
        if(thisIsActive){
            CharacterLocomotion.I.inventory.OnInventoryUpdated += UpdateInventory;
            UpdateInventory();
        }else
        {
            CharacterLocomotion.I.inventory.OnInventoryUpdated -= UpdateInventory;
        }
    }
    public void UpdateInventory()
    {
        List<InventoryItem> inventoryItems = CharacterLocomotion.I.inventory.GetInventory();
        for (int i = 0; i < holder.childCount; i++)
        {
            Destroy(holder.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < inventoryItems.Count; i++)
        {
            var go = Instantiate(inventoryFab,holder);
            go.GetComponent<HUDInventoryPiece>().Setup(inventoryItems[i]);
        }
    }
}
