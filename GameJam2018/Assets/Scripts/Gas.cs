using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GasType
{
    Null,
    Oxygen,
    Carbon_Dioxide,
    Nitrogen,
    Methane,
    Sulfur,
    Hydrogen,
    Chlorine
}


    /// <summary>
    /// Struct for hoild ing gas types
    /// </summary>
    /// 
[System.Serializable]
public class Gas {

    public GasType GasType;
    public float Percentage;

    public Gas(GasType type)
    {
        this.GasType = type;
        Percentage = 0;
    }
}
