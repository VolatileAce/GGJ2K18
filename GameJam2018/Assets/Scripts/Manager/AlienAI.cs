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
    public int Max_Antennae = 2;
    public int Max_Ears = 2;

    public void Randomize()
    {
        var torso = gameObject.GetComponentInChildren<AlienTorsoLimb>();
        var armLimbs = gameObject.GetComponentsInChildren<AlienArmLimb>();
        var headLimbs = gameObject.GetComponentsInChildren<AlienHeadLimb>();

        //Work out the max
        foreach (var ele in headLimbs)
        {
            switch (ele.Limb)
            {
                case HeadLimbType.Antennae:
                    Max_Antennae++;
                    break;
                case HeadLimbType.Ears:
                    Max_Ears++;
                    break;
                case HeadLimbType.EyeBalls:
                    Max_EyeBalls++;
                    break;
                case HeadLimbType.Eye_Stalk:
                    Max_EyeStalks++;
                    break;
                case HeadLimbType.Mouth:
                    Max_Mouths++;
                    break;
                case HeadLimbType.Warts:
                    Max_Warts++;
                    break;
            }
        }

        foreach (var ele in armLimbs)
        {
            switch (ele.Limb)
            {
                case ArmLimbType.Robot:
                    Max_Arms++;
                    break;

                case ArmLimbType.Tentacle:
                    Max_Tentacles++;
                    break;
            }
        }


        int seed = System.DateTime.Now.Millisecond + System.DateTime.Now.Minute + System.DateTime.Now.Hour;
        Random.InitState( seed);

        RegenerateRacialName();

        //Randomize the home, effecting language and audio
        GalacticHome = (AlienGalacticHome)Random.Range((int)0, (int)TorsoLimbType.MAX_TYPES);

        //Randomize the limbs
        TorsoLimbType Torso = (TorsoLimbType)Random.Range((int)1, (int)TorsoLimbType.MAX_TYPES );

        int eyeStalks = Random.Range(0, Max_EyeStalks);
        int eyeballs = Random.Range(0, Max_EyeBalls);
        int arms = Random.Range(0, Max_Arms);
        int ears = Random.Range(0, Max_Ears);
        int tentacles = Random.Range(0, Max_Tentacles);
        int warts = Random.Range(0, Max_Warts);
        int mouths = Random.Range(0, Max_Mouths);
        int antennae = Random.Range(0, Max_Antennae);


        AllLimbs.Add(torso);

        //Now we set up the rendering of the alien to match!
        foreach (var ele in armLimbs)
        {
            switch(ele.Limb)
            {
                case ArmLimbType.Robot:
                    if (arms <= 0)
                        ele.gameObject.SetActive(false);
                    arms--;
                    break;

                case ArmLimbType.Tentacle:
                    if (tentacles <= 0)
                        ele.gameObject.SetActive(false);
                    tentacles--;
                    break;
            }
        }

        foreach (var ele in headLimbs)
        {
            switch (ele.Limb)
            {
                case HeadLimbType.Antennae:
                    if (antennae <= 0)
                        ele.gameObject.SetActive(false);
                    antennae--;
                    break;
                case HeadLimbType.Ears:
                    if (ears <= 0)
                        ele.gameObject.SetActive(false);
                    ears--;
                    break;
                case HeadLimbType.EyeBalls:
                    if (eyeballs <= 0)
                        ele.gameObject.SetActive(false);
                    eyeballs--;
                    break;
                case HeadLimbType.Eye_Stalk:
                    if (eyeStalks <= 0)
                        ele.gameObject.SetActive(false);
                    eyeStalks--;
                    break;
                case HeadLimbType.Mouth:
                    if (mouths <= 0)
                        ele.gameObject.SetActive(false);
                    mouths--;
                    break;
                case HeadLimbType.Warts:
                    if (warts <= 0)
                        ele.gameObject.SetActive(false);
                    warts--;
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        Requirements.Evaluate();
    }
}
