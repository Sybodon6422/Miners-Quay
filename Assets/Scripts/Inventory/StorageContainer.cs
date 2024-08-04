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

    public IEnteractable.EnteractionData OnEnteract(Astronaut astronaut)
    {
        HUDManager.I.OpenContainerMenu(this);
        return new IEnteractable.EnteractionData(false);
    }
}
