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
    public static AlienAI Instance;

    public string RacialName = "Alien";
    public AlienRequirements Requirements;
    public AlienGalacticHome GalacticHome;

    public Text DebugHeatText;
    public Text DebugMusicText;
    public Text DebugAtmosphereText;
    public Text DebugUberText;
    public Material AlienSkinMaterial;

    public Color SkinRed;
    public Color SkinBlue;
    public Color SkinGreen;

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

    void Awake()
    {
        Instance = this;
    }
        
    // Use this for initialization
    void Start()
    {
        Randomize();
        Requirements = new AlienRequirements(this);
    }

    private Dictionary<HeadLimbType, int> HeadMaxes = new Dictionary<HeadLimbType, int>();
    private Dictionary<ArmLimbType, int> ArmMaxes = new Dictionary<ArmLimbType, int>();

    public Dictionary<HeadLimbType, int> HeadLimbCount = new Dictionary<HeadLimbType, int>();
    public Dictionary<ArmLimbType, int> ArmLimbCount = new Dictionary<ArmLimbType, int>();


    public TorsoLimbType TorsoColor;

    private Dictionary<int, bool> VisibilityTable = new Dictionary<int, bool>();

    public void ScheduleRequets(int in_Requests = 3)
    {

    }

    public void Randomize()
    {
        var armLimbs = gameObject.GetComponentsInChildren<AlienArmLimb>();
        var headLimbs = gameObject.GetComponentsInChildren<AlienHeadLimb>();

        //Work out the max
        foreach (var ele in headLimbs)
        {
            if (ele.LocationID != 0)
                if (VisibilityTable.ContainsKey(ele.LocationID))
                    VisibilityTable.Add(ele.LocationID, false);

            if (!HeadMaxes.ContainsKey(ele.Limb))
                HeadMaxes.Add(ele.Limb, 0);

            HeadMaxes[ele.Limb]++;
        }

        foreach (var ele in armLimbs)
        {
            if (ele.LocationID != 0)
                if (!VisibilityTable.ContainsKey(ele.LocationID))
                    VisibilityTable.Add(ele.LocationID, false);

            if (!ArmMaxes.ContainsKey(ele.Limb))
                ArmMaxes.Add(ele.Limb, 0);

            ArmMaxes[ele.Limb]++;
        }

        int seed = System.DateTime.Now.Millisecond + System.DateTime.Now.Minute + System.DateTime.Now.Hour;
        Random.InitState( seed);

        RegenerateRacialName();

        //Randomize the home, effecting language and audio
        GalacticHome = (AlienGalacticHome)Random.Range((int)0, (int)TorsoLimbType.MAX_TYPES);

        //Randomize the skin color
        TorsoColor = (TorsoLimbType)Random.Range((int)1, (int)TorsoLimbType.MAX_TYPES );

        switch(TorsoColor)
        {
            case TorsoLimbType.Blue:
                AlienSkinMaterial.color = SkinBlue;
                break;

            case TorsoLimbType.Green:
                AlienSkinMaterial.color = SkinGreen;
                break;

            case TorsoLimbType.Red:
                AlienSkinMaterial.color = SkinRed;
                break;
        }

        HeadLimbCount.Add(HeadLimbType.Eye_Stalk, Random.Range(0, HeadMaxes[HeadLimbType.Eye_Stalk]));
        HeadLimbCount.Add(HeadLimbType.EyeBalls, Random.Range(0, HeadMaxes[HeadLimbType.EyeBalls]));
        HeadLimbCount.Add(HeadLimbType.Ears, Random.Range(0, HeadMaxes[HeadLimbType.Ears]));
        HeadLimbCount.Add(HeadLimbType.Warts, Random.Range(0, HeadMaxes[HeadLimbType.Warts]));
        HeadLimbCount.Add(HeadLimbType.Mouth, Random.Range(0, HeadMaxes[HeadLimbType.Mouth]));
        HeadLimbCount.Add(HeadLimbType.Antennae, Random.Range(0, HeadMaxes[HeadLimbType.Antennae]));

        ArmLimbCount.Add(ArmLimbType.Robot, Random.Range(0, ArmMaxes[ArmLimbType.Robot]));
        ArmLimbCount.Add(ArmLimbType.Tentacle, Random.Range(0, ArmMaxes[ArmLimbType.Tentacle]));

        //Now we set up the rendering of the alien to match!
        foreach (var ele in armLimbs)
        {
            if (!ele.gameObject.active)
                continue;

            if (ele.LocationID != 0)
                if (VisibilityTable[(ele.LocationID)])
                {
                    ele.gameObject.SetActive(false);
                    continue;
                }

            if (ArmLimbCount[ele.Limb] <= 0)
                ele.gameObject.SetActive(false);
            else
            {
                //Disable any arms occupying the same slot
                if (ele.LocationID != 0)
                    VisibilityTable[ele.LocationID] = true;

                ArmLimbCount[ele.Limb]--;
            }
        }

        foreach (var ele in headLimbs)
        {
            if (!ele.gameObject.active)
                continue;

            if (ele.LocationID != 0)
                if (VisibilityTable.ContainsKey(ele.LocationID))
                {
                    ele.gameObject.SetActive(false);
                    continue;
                }

            switch (ele.Limb)
            {
                default:
                    if (HeadLimbCount[ele.Limb] <= 0)
                        ele.gameObject.SetActive(false);
                    else
                        HeadLimbCount[ele.Limb]--;
                    break;

                case HeadLimbType.EyeBalls:
                    if (HeadLimbCount[ele.Limb] <= 0)
                        ele.gameObject.SetActive(false);
                    else
                    {
                        //Disable any eye stalks occupying the same slot
                        if (ele.LocationID != 0)
                            VisibilityTable.Add(ele.LocationID, true);
                    }
                    HeadLimbCount[ele.Limb]--;
                    break;

                case HeadLimbType.Eye_Stalk:
                    if (HeadLimbCount[ele.Limb] <= 0)
                        ele.gameObject.SetActive(false);
                    else
                    {
                        //Disable any eye balls occupying the same slot
                        if (ele.LocationID != 0)
                            VisibilityTable.Add(ele.LocationID, true);
                        HeadLimbCount[ele.Limb]--;
                    }
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
