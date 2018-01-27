using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat
{
    /// <summary>
    /// The exact perfect temperature for a 5 star review
    /// </summary>
    public float TemperatureDegrees;

    /// <summary>
    /// The maximum difference before temperature becomes disliked
    /// </summary>
    public float ToleranceDegrees;

    public Heat(float in_Temperature, float in_ToleranceDegrees)
    {
        TemperatureDegrees = in_Temperature;
        ToleranceDegrees = in_ToleranceDegrees;
    }
}

public class HeatingManager : MonoBehaviour
{
    //Static Acess to this class set on awake
    public static HeatingManager Instance;

    public Heat Heat;

    public float MinTemperature = 0;
    public float MaxTemperature = 100;


    void Awake()
    {
        Instance = this;
        Heat = new Heat(50, 0);
    }

    public static void SetHeatDegrees(float in_Degrees)
    {
        Instance.Heat.TemperatureDegrees = in_Degrees;
    }
}
