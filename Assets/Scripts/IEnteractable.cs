using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnteractable
{
    EnteractionData OnEnteract(Astronaut astro);

    public class EnteractionData
    {
        public bool astronautEnteractionOnly;

        public EnteractionData(bool astroOnly)
        {
            astronautEnteractionOnly = astroOnly;
        }
    }
}
