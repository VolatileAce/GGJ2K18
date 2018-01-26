using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomized passenger with both initial, and ongoing requirements
/// </summary>
public class Alien : MonoBehaviour
{
    public string RacialName = "Alien";

    private List<string> NamePrefix = new List<string>()
    {
        "Mee",
        "Cronen",
        "Gromfla",
        "Hu",
        "Gazor",
        "Butter",
    };

    private List<string> NamePostfix = new List<string>()
    {
        "seek",
        "berg",
        "mite",
        "man",
        "pian",
        "ions"
    };

    public void RegenerateRacialName()
    {
        RacialName = NamePrefix[(int)Random.Range(0, NamePrefix.Count)] + NamePostfix[(int)Random.Range(0, NamePostfix.Count)];        
    }
    

    /// <summary>
    /// The gas required to be happy and survive
    /// </summary>
    public Gas BreathableAtmosphere;

    /// <summary>
    /// The gas that creates unhappiness and death
    /// </summary>
    public Gas ToxicAtmosphere;

    // Use this for initialization
    void Start()
    {
        RegenerateRacialName();

    }

    // Update is called once per frame
    void Update()
    {
        var goodGas = AtmosphereManager.GetGas(this.BreathableAtmosphere.GasType);
        var badGas = AtmosphereManager.GetGas(this.ToxicAtmosphere.GasType);

        float breathableTolerance = 0.1f;

        if (goodGas.GasType != GasType.Null)        
            if (Mathf.Abs(goodGas.Percentage - BreathableAtmosphere.Percentage) < breathableTolerance)
                ScoreManager.ImproveUberRating(breathableTolerance * Time.deltaTime);        

        if (badGas.GasType != GasType.Null)        
            ScoreManager.DamageUberRating(badGas.Percentage * Time.deltaTime);    
    }
}
