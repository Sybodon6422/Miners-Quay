using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageContainer : MonoBehaviour, IEnteractable
{
    public Inventory inventory;
    void Start()
    {
        inventory.InitializeInventory();
    }

    public void OnEnteract()
    {
        HUDManager.I.OpenContainerMenu(this);
    }
}
