using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IDamagable
{
    int health = 10;
    public ItemSO itemToDrop;
    public int dropAmount = 1;
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if(health <=0)
        {
            //dead
            Break();
        }
    }

    public void Break()
    {
        //spawn item
        if(itemToDrop != null)
        {
            GameManager.I.SpawnItem(itemToDrop, dropAmount, transform.position);
        }
        Destroy(this.gameObject);
    }
}
