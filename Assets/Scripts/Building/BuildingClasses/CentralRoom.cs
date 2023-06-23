using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralRoom : Building
{
    // Centralroom is the first piece of any base and contains enough resources to sustain 3 astronauts permanently.
    public override void TickUpdate()
    {
        hub.currentEnergy += 1;
        hub.currentFood += 1;
        hub.currentOxygen += 1;
        hub.currentWater += 1;
    }
}
