using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LaunchVehicleHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI vehicleNameText, payloadText, costText;
    [SerializeField] private Image vehicleIcon;
    [SerializeField] private LaunchVehicleSO thisVehicle;

    private Image buttonImage;

    private void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    public void SetupVehicle(LaunchVehicleSO vehicle)
    {
        thisVehicle = vehicle;
        vehicleNameText.text = vehicle.name;
        payloadText.text = "Payload: " + vehicle.maximumOrbitWeight;
        vehicleIcon.sprite = vehicle.vehicleIcon;
        costText.text = "Cost: " + vehicle.shipCost;
    }

    public void SelectVehicle()
    {
        EarthManagement.I.SelectLaunchVehicle(this);
    }

    public LaunchVehicleSO LaunchVehicleSO { get { return thisVehicle; } }
    public void SetColor(Color color)
    {
        buttonImage.color = color;
    }
}
