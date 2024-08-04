using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EarthManagement : MonoBehaviour
{
    #region singleton
    private static EarthManagement _instance;
    public static EarthManagement I { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        builtRocketsInRetainer = new List<RocketShip>();
    }
    #endregion

    private List<RocketShip> builtRocketsInRetainer;

    private float tickCurrentTime = 0;
    private bool waitingForHUDTick = false;
    private void FixedUpdate()
    {
        tickCurrentTime += Time.fixedDeltaTime;
        if (tickCurrentTime >= 1)
        {
            tickCurrentTime = 0;
            Tick();
        }
    }

    private void Tick()
    {
        if (waitingForHUDTick)
        {
            GetAllButtons();
            waitingForHUDTick = false;
        }
    }

    #region shipBuilding

    private LaunchVehicleSO selectedLaunchVehicle; 
    public void SelectLaunchVehicle(LaunchVehicleHUD lv) 
    { 
        selectedLaunchVehicle = lv.LaunchVehicleSO; 
        UpdateShipStats();
        ResetLVButtons(lv);
    }

    private PayloadSO selectedPayload; 
    public void SelectPayload(PayloadOptionHUD payload) 
    { 
        selectedPayload = payload.PayloadSO; 
        UpdateShipStats(); 
        ResetPLButtons(payload);
    }


    [SerializeField] private TextMeshProUGUI buildButtonText;

    [SerializeField] private Transform LVMenuTransform, payloadMenuTransform, crewMenuTransform;

    [SerializeField] private GameObject LVOptionPrefab, payloadOptionPrefab, crewOptionPrefab;

    //TODO: add a research system to handle unlocked ships and payloads and a crew mangement system for crew. for now a list of ships for debug testing
    [SerializeField] private List<LaunchVehicleSO> availableLaunchVehicles;
    [SerializeField] private List<PayloadSO> availablePayloads;

    private LaunchVehicleHUD[] lvButtons;
    private PayloadOptionHUD[] plButtons;

    [SerializeField] Color selectColor, defaultColor;
    private void GetAllButtons()
    {
        lvButtons = null;
        plButtons = null;

        lvButtons = LVMenuTransform.GetComponentsInChildren<LaunchVehicleHUD>();
        plButtons = payloadMenuTransform.GetComponentsInChildren<PayloadOptionHUD>();
    }

    private void ResetLVButtons(LaunchVehicleHUD skipButton)
    {
       foreach (LaunchVehicleHUD lvHUD in lvButtons)
        {
            if(lvHUD != skipButton)
            {
                lvHUD.SetColor(defaultColor);
            }
            else
            {
                lvHUD.SetColor(selectColor);
            }
        }
    }

    private void ResetPLButtons(PayloadOptionHUD skipButton)
    {
        foreach (PayloadOptionHUD plHUD in plButtons)
        {
            if (plHUD != skipButton)
            {
                plHUD.SetColor(defaultColor);
            }
            else
            {
                plHUD.SetColor(selectColor);
            }
        }
    }

    public void OpenShipCreationMenu()
    {
        HUDManager.I.CloseAllMenus();
        HUDManager.I.OpenShipBuildingMenu();

        //clear out old options
        foreach (Transform child in LVMenuTransform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in payloadMenuTransform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in crewMenuTransform)
        {
            Destroy(child.gameObject);
        }

        foreach (LaunchVehicleSO lv in availableLaunchVehicles)
        {
            GameObject go = Instantiate(LVOptionPrefab, LVMenuTransform);
            go.GetComponent<LaunchVehicleHUD>().SetupVehicle(lv);
        }

        foreach (PayloadSO payload in availablePayloads)
        {
            GameObject go = Instantiate(payloadOptionPrefab, payloadMenuTransform);
            go.GetComponent<PayloadOptionHUD>().SetupVehicle(payload);
        }

        selectedLaunchVehicle = availableLaunchVehicles[0];
        selectedPayload = availablePayloads[0];
        UpdateShipStats();
        waitingForHUDTick = true;
    }

    public void BuildShip()
    {
        HUDManager.I.CloseAllMenus();
        if (selectedLaunchVehicle == null || selectedPayload == null)
        {
            Debug.Log("Missing vehicle or payload");
            return;
        }
        else
        {
            RocketShip newRocket = new RocketShip(selectedLaunchVehicle, selectedPayload);
            builtRocketsInRetainer.Add(newRocket);

            selectedLaunchVehicle = null;
            selectedPayload = null;
        }
    }

    private void UpdateShipStats()
    {
        RocketShip newRocket = new RocketShip(selectedLaunchVehicle, selectedPayload);
        int shipCost = selectedLaunchVehicle.shipCost + selectedPayload.cost;
        buildButtonText.text = "Build Ship ($" + shipCost + ")";
    }

    #endregion
}
