using UnityEngine;
using static UnityEngine.Analytics.IAnalytic;

public class VehicleControlPanel : MonoBehaviour, IEnteractable
{
    public IEnteractable.EnteractionData OnEnteract(Astronaut astro)
    {
        HUDManager.I.OpenWorldsTravelMenu(true);
        IEnteractable.EnteractionData iData = new IEnteractable.EnteractionData(true);
        return iData;
    }
}
