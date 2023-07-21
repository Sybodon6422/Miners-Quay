using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astronaut : MonoBehaviour, IDamagable
{
    public bool canEnterBuilding = false;
    public Door buildingToEnter;
    internal bool insideBuilding = false;
    public bool InsideBuilding { get { return insideBuilding; } }

    public float maxOxygen = 100; public float MaxOxygen { get { return maxOxygen; } }
    internal float oxygen = 100;
    private float damageCoolDown = .2f;

    public void BuildingSwitch(bool inside)
    {
        insideBuilding = inside;
        if(insideBuilding)
        {
            gameObject.layer = 15;
        }
        else
        {
            gameObject.layer = 3;
        }
    }

    void FixedUpdate()
    {
        damageCoolDown = Mathf.Clamp(damageCoolDown-Time.deltaTime,0,.2f);
        if(insideBuilding){oxygen = Mathf.Clamp(oxygen+Time.deltaTime,0,maxOxygen);}
        else{oxygen = Mathf.Clamp(oxygen-Time.deltaTime,-1,maxOxygen);}
        if(oxygen <= 0){TakeDamage(1);}

        BaseUpdateCall();
    }

    internal virtual void BaseUpdateCall(){}

    int health = 20;

    public void TakeDamage(int damage)
    {
        if(damageCoolDown > 0){return;}
        health -= damage;
        damageCoolDown = .2f;
        
        if(health <=0)
        {
            //dead
            Destroy(this.gameObject);
        }
    }
}
