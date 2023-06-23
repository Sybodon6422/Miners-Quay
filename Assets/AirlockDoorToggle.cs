using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirlockDoorToggle : MonoBehaviour , IEnteractable
{
    [SerializeField] private Animator anim1, anim2;
    private bool doorOpen = false;

    void start()
    {

    }

    public void OnEnteract()
    {
        if(doorOpen)
        {
            anim1.SetTrigger("Close");
            anim2.SetTrigger("Open");
        }
        else
        {
            anim1.SetTrigger("Open");
            anim2.SetTrigger("Close");
        }
        doorOpen = !doorOpen;
    }
}
