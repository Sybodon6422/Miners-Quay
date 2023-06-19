using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDMenu : MonoBehaviour
{
    private bool menuOpen = false;
    public bool MenuOpen { get { return menuOpen; } }

    public virtual void ToggleMenu()
    {
        if(menuOpen)
        {
            OpenMenu();
        } else {
            CloseMenu();
        }
    }

    public virtual void OpenMenu()
    {
        gameObject.SetActive(true);
        menuOpen = true;
    }

    public virtual void CloseMenu()
    {
        gameObject.SetActive(false);
        menuOpen = false;
    }
}
