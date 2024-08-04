using UnityEngine;

[CreateAssetMenu(fileName = "ShipStuff/New Payload", menuName = "Payload", order = 0)]
public class PayloadSO : ScriptableObject
{
    public string payloadName;
    public Sprite payloadIcon;

    public int cost;
    public int crew;
    public int maxCargo;

    public float grossTonnage;
    public float maxDistance;
}
