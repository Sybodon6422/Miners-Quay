using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : MonoBehaviour ,IEnteractable
{
    public void OnEnteract()
    {
        HUDManager.I.OpenFurnaceMenu(this);
    }
    
    private InventoryItem itemToSmelt;
    private InventoryItem finishedItem;

    void OnEnable()
    {
        itemToSmelt = new InventoryItem(null,0);
        finishedItem = new InventoryItem(null,0);
    }

    void Start()
    {
        FurnaceInputProcessor.I.SetIcon(0, false);
        FurnaceInputProcessor.I.SetIcon(1, false);
    }

    public bool TryAddSmeltItem(InventoryItem itemToAdd)
    {
        if(itemToSmelt.itemRef == null)
        {
            itemToSmelt = itemToAdd;
            FurnaceInputProcessor.I.SetIcon(0, true);

            return true;
        }
        else if(itemToSmelt.itemRef == itemToAdd.itemRef)
        {
            itemToSmelt.itemAmmount += itemToAdd.itemAmmount;

            return true;
        }
        return false;
    }

    public void TryRemoveSmeltItem()
    {
        if(itemToSmelt.itemRef != null)
        {
            CharacterController.I.inventory.AddToInventory(itemToSmelt.itemRef,1);
            itemToSmelt.itemAmmount -= 1;
            if(itemToSmelt.itemAmmount <= 0)
            {
                itemToSmelt = new InventoryItem(null,0);
                FurnaceInputProcessor.I.SetIcon(0, false);
                CancelCraft();
            }
        }
    }

    public void TryRemoveFinishedItem()
    {
        if(finishedItem.itemRef != null)
        {
            CharacterController.I.inventory.AddToInventory(finishedItem.itemRef,finishedItem.itemAmmount);
            finishedItem = new InventoryItem(null,0);
            FurnaceInputProcessor.I.SetIcon(1, false);
        }
    }

    public void CancelCraft()
    {
        smeltTime = 10f;
    }

    private float smeltTime = 10f;
    void FixedUpdate()
    {
        if(itemToSmelt.itemRef != null)
        {
            smeltTime -= Mathf.Clamp(Time.deltaTime, 0, 99f);
            if(smeltTime <= 0)
            {
                smeltTime = 10f;

                if(finishedItem.itemRef == null)
                {
                    finishedItem = FurnaceInputProcessor.I.recipeBook.GetOutputItem(itemToSmelt.itemRef);
                    FurnaceInputProcessor.I.SetIcon(1, true);
                    itemToSmelt.itemAmmount -= 1;
                    if(itemToSmelt.itemAmmount <= 0)
                    {
                        itemToSmelt = new InventoryItem(null,0);
                        FurnaceInputProcessor.I.SetIcon(0, false);
                    }
                }else
                {
                    finishedItem.itemAmmount += 1;
                    itemToSmelt.itemAmmount -= 1;
                    if(itemToSmelt.itemAmmount <= 0)
                    {
                        itemToSmelt = new InventoryItem(null,0);
                        FurnaceInputProcessor.I.SetIcon(0, false);
                    }
                }
            }
        }
    }
}
