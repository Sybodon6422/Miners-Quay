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
    public void UpdateInventory(List<InventoryItem> inventoryItems)
    {
        gameObject.SetActive(!thisIsActive);
        thisIsActive = !thisIsActive;

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
