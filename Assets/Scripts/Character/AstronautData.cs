using System;
using UnityEngine;

[Serializable]
public class AstronautData
{
    public string firstName;
    public string lastName;

    public SerializableColor suitColor;

    public bool alive = true;



    public AstronautData(string firstName, string lastName, Color suitColor)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.suitColor = new SerializableColor(suitColor);
    }
}
