using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IEnteractable
{
    [SerializeField] Transform insidePortSpot, outsidePortSpot;
    public Building master;
    public void OnEnteract(Astronaut astronaut)
    {
        if(astronaut.InsideBuilding)
        {
            astronaut.transform.position = outsidePortSpot.position;
            astronaut.BuildingSwitch(false);
            master.PlayerExit(astronaut);
        }
        else
        {
            astronaut.transform.position = insidePortSpot.position;
            astronaut.BuildingSwitch(true);
            master.PlayerEnter(astronaut);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Astronaut astro = other.gameObject.GetComponent<Astronaut>();
            astro.canEnterBuilding = true;
            astro.buildingToEnter = this;
        }
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Astronaut astro = other.gameObject.GetComponent<Astronaut>();
            astro.canEnterBuilding = false;
            astro.buildingToEnter = null;
        }
    }
}
