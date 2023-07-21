using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingHUB hub;

    public SpriteRenderer[] outdoorView;

    void Start()
    {
        hub = GetComponentInParent<BuildingHUB>();
        hub.AddBuilding(this);
    }

    public void PlayerEnter(Astronaut astronaut)
    {
        foreach (SpriteRenderer view in outdoorView)
        {
            view.enabled = false;
        }
    }

    public void PlayerExit(Astronaut astronaut)
    {
        foreach (SpriteRenderer view in outdoorView)
        {
            view.enabled = true;
        }
    }

    public virtual void TickUpdate()
    {

    }
}