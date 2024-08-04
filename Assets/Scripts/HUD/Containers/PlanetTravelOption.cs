using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlanetTravelOption : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI planetNameText, planetGravityText, planetOxygenText, fuelCostText;
    [SerializeField] private Image planetImage;
    [SerializeField] private CelestialBody thisPlanet;

    public void SetupTravelOption(CelestialBody planet)
    {
        thisPlanet = planet;
        planetNameText.text = planet.name;
        planetGravityText.text = "Gravity: " + planet.gravity;
        planetOxygenText.text = planet.oxygen ? "Oxygen: Yes" : "Oxygen: No";
        planetImage.sprite = planet.sprite;
        fuelCostText.text = "Fuel Cost: " + planet.gravity;
    }


    public void TravelButtonPressed()
    {
        HUDManager.I.CloseAllMenus();
        GameManager.I.LoadWorld(thisPlanet);
    }
}
