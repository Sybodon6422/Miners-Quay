using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] private List<Collider2D> colliders;
    public BuildingHUB hub;
    void Start()
    {
        hub = GetComponentInParent<BuildingHUB>();
        hub.AddBuilding(this);
    }

    public virtual void TickUpdate()
    {

    }
}