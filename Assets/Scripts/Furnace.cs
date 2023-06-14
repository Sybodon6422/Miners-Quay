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

    public bool TryAddSmeltItem(InventoryItem itemToAdd)
    {
        if(itemToSmelt.itemRef == null)
        {
            itemToSmelt = itemToAdd;

            return true;
        }
        else if(itemToSmelt.itemRef == itemToAdd.itemRef)
        {
            itemToSmelt.itemAmmount += itemToAdd.itemAmmount;

            return true;
        }
        return false;
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
                    itemToSmelt.itemAmmount -= 1;
                    if(itemToSmelt.itemAmmount <= 0)
                    {
                        itemToSmelt = new InventoryItem(null,0);
                    }
                }else
                {
                    finishedItem.itemAmmount += 1;
                    itemToSmelt.itemAmmount -= 1;
                    if(itemToSmelt.itemAmmount <= 0)
                    {
                        itemToSmelt = new InventoryItem(null,0);
                    }
                }
            }
        }
    }
}
