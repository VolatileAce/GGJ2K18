using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtmosphereManager : MonoBehaviour
{
    //Static Acess to this class set on awake
    public static AtmosphereManager Instance;

    public List<Gas> AllGases = new List<Gas>();

    void Awake()
    {
        Instance = this;
    }

    public static Gas GetGas(GasType in_Type)
    {
        foreach (Gas gas in Instance.AllGases)
        {
            if (gas.GasType.Equals(in_Type))
                return gas;
        }
        return new Gas(GasType.Null);
    }

    public static void SetGas(GasType in_Type, float in_Percentage)
    {
        foreach (Gas gas in Instance.AllGases)
        {
            if (gas.GasType.Equals(in_Type))
                gas.Percentage = in_Percentage;           
        }
    }

    public void SetGas(Gas in_gas)
    {
        foreach (Gas gas in AllGases)
        {
            if (gas.GasType.Equals(in_gas.GasType))
                gas.Percentage = in_gas.Percentage;
        }
    }

}
