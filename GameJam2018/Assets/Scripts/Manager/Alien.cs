using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
/// Randomized passenger with both initial, and ongoing requirements
/// </summary>
public class AlienAI : MonoBehaviour
{
    public string RacialName = "Alien";
    public AlienRequirements Requirements;
    public AlienGalacticHome GalacticHome;

    public Text DebugHeatText;
    public Text DebugMusicText;
    public Text DebugAtmosphereText;
    public Text DebugUberText;

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
            if (limb.Location.Equals(in_Type))
                return limb;       

        return null;
    }    
    public void RegenerateRacialName()
    {
        RacialName = NamePrefix[(int)Random.Range(0, NamePrefix.Count)] + NamePostfix[(int)Random.Range(0, NamePostfix.Count)];        
    }
        
    // Use this for initialization
    void Start()
    {
        Randomize();
        Requirements = new AlienRequirements(this);
    }

    public int Max_EyeStalks = 2;
    public int Max_EyeBalls = 2;
    public int Max_Arms = 4;
    public int Max_Tentacles = 4;
    public int Max_Warts = 2;
    public int Max_Mouths = 2;
    public int Max_Atnennae = 2;

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
        HeadLimbType Hat = ((HeadLimbType)Random.Range((int)1, (int)HeadLimbType.MAX_TYPES ));

        //Now we set up the rendering of the alien to match!
        var torsos = gameObject.GetComponentsInChildren<AlienTorsoLimb>();
        var arms = gameObject.GetComponentsInChildren<AlienArmLimb>();
        var hats = gameObject.GetComponentsInChildren<AlienHeadLimb>();

        foreach (var ele in torsos)
        {
            if (ele.Limb != Torso)
                ele.gameObject.SetActive(false);
            else
                AllLimbs.Add(ele);
        }

        foreach (var ele in arms)
        {
            if (ele.Limb != Arms)
                ele.gameObject.SetActive(false);
            else
                AllLimbs.Add(ele);
        }

        foreach (var ele in hats)
        {
            if (ele.Limb != Hat)
                ele.gameObject.SetActive(false);
            else
                AllLimbs.Add(ele);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Requirements.Evaluate();
    }
}
