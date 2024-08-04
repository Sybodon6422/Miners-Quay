using UnityEngine;

[CreateAssetMenu(fileName = "ShipStuff/New Launch Vehicle", menuName = "Launch Vehicle", order = 0)]
public class LaunchVehicleSO : ScriptableObject
{
    public string vehicleName;
    public Sprite vehicleIcon;

    public int shipCost;
    public float maximumOrbitWeight;
}
