using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CelestialBody", menuName = "Celestial Body")]
public class CelestialBody : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    BodyType type;
    public float gravity;

    public bool oxygen;

    public AnimationCurve sunLightCurve;
}

public enum BodyType
{
    homeWorld,
    planet,
    moon,
    asteroid,
    gasGiant
}
