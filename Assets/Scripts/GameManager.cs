using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    #region singleton
    private static GameManager _instance;
    public static GameManager I { get { return _instance; } }

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

    private int money = 10000;
    public int Money { get { return money; } }

    public CharacterLocomotion controlledAstro;

    public void ChangeMoney(int amount)
    {
        money += amount;
        HUDManager.I.UpdateMoneyText(money);
    }
    #region Crew Management

    public void TakeOverAstronaut(Astronaut astroToControl)
    {
        CameraController.I.SetTrackedObject(astroToControl.transform);
        var loco = astroToControl.AddComponent<CharacterLocomotion>();
        loco.EnableAstro(astroToControl);
        controlledAstro = loco;
    }

    #endregion

    #region item management

    public GameObject itemWorldFAB;
    [SerializeField] private Transform itemHolder;
    public void SpawnItem(int itemID, Vector2 position)
    {
        var go = Instantiate(itemWorldFAB,position,Quaternion.identity);
        go.transform.parent = itemHolder;
    }

    public void SpawnItem(ItemSO item, int amount, Vector2 position)
    {
        for (int i = 0; i < amount; i++)
        {
            var go = Instantiate(itemWorldFAB,position,Quaternion.identity);
            go.transform.parent = itemHolder;
            go.GetComponent<WorldItem>().thisItem = item;
        }
    }

    #endregion

    #region world management
    [SerializeField] private List<CelestialBody> bodyList;
    [SerializeField] private CelestialBody currentBody;
    [SerializeField] private Light2D sun;

    public CelestialBody GetBody(int index)
    {
        return bodyList[index];
    }
    [SerializeField] private List<GameObject> worlds;
    [SerializeField] private List<GameObject> backgrounds;

    public void LoadWorld(CelestialBody planet)
    {
        SetActiveWorld(bodyList.IndexOf(planet));
        SetActiveBG(bodyList.IndexOf(planet));
        CharacterLocomotion.I.Astronaut.SetPlanet(planet);
        sun.intensity = planet.sunLightCurve.Evaluate(0.5f);
        currentBody = planet;

        // set camera to planet
        if(planet.name == "Earth")
        {
            CameraController.I.SetCameraToStaticMode(new Vector3(0,10.7f,-10));
            CameraController.I.SetCameraZoom(15);
        } else 
        {
            //TODO: need crew manager in game to get crew position
        }
    }

    public CelestialBody GetCurrentPlanet()
    {
        return currentBody;
    }

    private void SetActiveWorld(int worldIndex)
    {
        // set active world
        for (int i = 0; i < worlds.Count; i++)
        {
            if (i == worldIndex)
            {
                worlds[i].SetActive(true);
            } else {
                worlds[i].SetActive(false);
            }
        }
    }

    private void SetActiveBG(int worldIndex)
    {
        // set active background
        for (int i = 0; i < backgrounds.Count; i++)
        {
            if (i == worldIndex)
            {
                backgrounds[i].SetActive(true);
            } else {
                backgrounds[i].SetActive(false);
            }
        }
    }


    #endregion
}
