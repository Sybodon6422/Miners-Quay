using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagableObject : MonoBehaviour, IDamagable
{
    int health = 10;
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if(health <=0)
        {
            //dead
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
}
