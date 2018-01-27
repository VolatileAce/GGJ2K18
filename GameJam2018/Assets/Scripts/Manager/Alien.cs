using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Which placement point on the alien
/// </summary>
[System.Serializable]
public enum LimbLocation
{
    Null,

    Hat,
    Torso,
    Arms,
}

/// <summary>
/// Language, and audio cues about alien
/// </summary>
public enum AlienGalacticHome
{
    Central,
    Inner,
    Outer,

    MAX_TYPES
}

/// <summary>
/// an asset and location to place on the alien
/// </summary>
[System.Serializable]
public class AlienLimb : MonoBehaviour
{
    public virtual LimbLocation Location { get { return LimbLocation.Null; } }    
}

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
    
    public List<AlienLimb> AllLimbs = new List<AlienLimb>();

    public AlienLimb GetLimb(LimbLocation in_Type)
    {
        foreach (var limb in AllLimbs)
        {
            if (limb.Location.Equals(in_Type))
                return limb;
        }

        return null;
    }    

    public void RegenerateRacialName()
    {
        RacialName = NamePrefix[(int)Random.Range(0, NamePrefix.Count)] + NamePostfix[(int)Random.Range(0, NamePostfix.Count)];        
    }

    public AlienGalacticHome GalacticHome;


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
        Randomize();
    }

    public void Randomize()
    {
        int seed = System.DateTime.Now.Millisecond + System.DateTime.Now.Minute + System.DateTime.Now.Hour;
        Random.InitState( seed);

        RegenerateRacialName();

        //Randomize the home, effecting language and audio
        GalacticHome = (AlienGalacticHome)Random.Range((int)0, (int)TorsoLimbType.MAX_TYPES);

        //Randomize the limbs
        TorsoLimbType Torso = (TorsoLimbType)Random.Range((int)1, (int)TorsoLimbType.MAX_TYPES );        
        ArmLimbType Arms = ((ArmLimbType)Random.Range((int)1, (int)ArmLimbType.MAX_TYPES ));        
        HatLimbType Hat = ((HatLimbType)Random.Range((int)1, (int)HatLimbType.MAX_TYPES ));

        //Now we set up the rendering of the alien to match!
        var torsos = gameObject.GetComponentsInChildren<AlienTorsoLimb>();
        var arms = gameObject.GetComponentsInChildren<AlienArmLimb>();
        var hats = gameObject.GetComponentsInChildren<AlienHatLimb>();

        foreach (var ele in torsos)
            if (ele.Limb != Torso)
                ele.gameObject.SetActive(false);

        foreach (var ele in arms)
            if (ele.Limb != Arms)
                ele.gameObject.SetActive(false);

        foreach (var ele in hats)
            if (ele.Limb != Hat)
                ele.gameObject.SetActive(false);
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
