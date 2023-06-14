using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour
{
    #region singleton
    private static BuildingSystem _instance;
    public static BuildingSystem I { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }



    #endregion
    
    private List<Building> buildings = new List<Building>();

    void Start()
    {
        buildings.AddRange(FindObjectsOfType<Building>());
    }

    public void AddBuilding(Building building)
    {
        if(!buildings.Contains(building)){return;}
        buildings.Add(building);
    }
}
