using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingHUB : MonoBehaviour
{
    public int maxEnergy = 100;
    public int currentEnergy = 0;

    public int maxWater = 100;
    public int currentWater = 0;

    public int maxFood = 100;
    public int currentFood = 0;

    public int maxOxygen = 100;
    public int currentOxygen = 0;

    public int basePopulation;

    public List<Building> buildings = new List<Building>();

    void Start()
    {
        
    }

    public void AddBuilding(Building building)
    {
        buildings.Add(building);
    }

    private float tickTime = 0;
    // Update is called once per frame
    void FixedUpdate()
    {
        tickTime += Time.deltaTime;
        if (tickTime >= 3)
        {
            tickTime = 0;
            OnTick();
        }
    }

    private void OnTick()
    {

    }
}
