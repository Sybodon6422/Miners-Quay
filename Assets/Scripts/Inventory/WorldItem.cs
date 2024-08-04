using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : MonoBehaviour
{
    public ItemSO thisItem;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CharacterLocomotion>().inventory.AddToInventory(thisItem,1);
            Destroy(gameObject);
        }
    }
}
