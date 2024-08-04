using System;

[Serializable]
public class RocketShip
{
    private LaunchVehicleSO launchVehicle;
    private PayloadSO payload;

    private float maxDistance;

    public RocketShip(LaunchVehicleSO _launchVehicle, PayloadSO _payload)
    {
        launchVehicle = _launchVehicle;
        payload = _payload;
        maxDistance = payload.maxDistance;
    }
}
