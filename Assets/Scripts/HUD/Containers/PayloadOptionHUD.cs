using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PayloadOptionHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI vehicleNameText, costText, maxDistanceText, crewText, cargoText;
    [SerializeField] private Image vehicleIcon;
    [SerializeField] private PayloadSO thisVehicle;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void SetupVehicle(PayloadSO vehicle)
    {
        thisVehicle = vehicle;
        vehicleNameText.text = vehicle.payloadName;
        costText.text = "Cost: " + vehicle.cost;
        maxDistanceText.text = "Max Distance: " + vehicle.maxDistance;
        vehicleIcon.sprite = vehicle.payloadIcon;
        cargoText.text = "Max Cargo: " + vehicle.maxCargo;
        if (vehicle.crew > 0)
        {
            crewText.text = "Max Crew: " + vehicle.crew;
        }
        else
        {
           crewText.text = "Unmanned";
        }
    }

    public void SelectVehicle()
    {
        EarthManagement.I.SelectPayload(this);
    }

    public PayloadSO PayloadSO { get { return thisVehicle; } }
    public void SetColor(Color color)
    {
        buttonImage.color = color;
    }
}
