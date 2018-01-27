using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Request that are passed grant large bonuses to rating
/// </summary>
public class AlienRequest
{
    public System.TimeSpan TimeUntilActivation;
    public float SecondsAllowed;

    public AlienRequest(float in_SecondsUntilActivation, float in_SecondsAllowed)
    {
        TimeUntilActivation = System.TimeSpan.FromSeconds(in_SecondsUntilActivation);
        SecondsAllowed = in_SecondsAllowed;
    }
}


public class RadioRequest : AlienRequest
{
    public Radio MyRadio;

    public RadioRequest(Radio in_Radio, float in_SecondsUntilActivation, float in_SecondsAllowed)
        : base(in_SecondsUntilActivation, in_SecondsAllowed)
    {
        MyRadio = in_Radio;
    }
}


public class AlienRequirements
{
    /// <summary>
    /// The gas required to be happy and survive
    /// </summary>
    Dictionary<GasType, Gas> BreathableAtmosphere = new Dictionary<GasType, Gas>();

    /// <summary>
    /// What radio makes the passenger happy
    /// </summary>
    public Radio LikedRadio;

    /// <summary>
    /// What radio makes the passenger unhappy
    /// </summary>
    public Radio HatedRadio;

    /// <summary>
    /// What temperature is liked\hated. Contains a tolerance.
    /// </summary>
    public Heat LikedHeat;

    public AlienAI MyAlien;

    /// <summary>
    /// Initialize the preferences based on the appearance of the alien
    /// </summary>
    /// <param name="in_Alien"></param>
    public AlienRequirements(AlienAI in_Alien)
    {
        MyAlien = in_Alien;

        BreathableAtmosphere.Add(GasType.Oxygen, new Gas(GasType.Oxygen)
        {
            Percentage = in_Alien.HeadLimbCount[HeadLimbType.Eye_Stalk] * 0.05f
        });
        BreathableAtmosphere.Add(GasType.Carbon_Dioxide, new Gas(GasType.Carbon_Dioxide)
        {
            Percentage = (in_Alien.HeadLimbCount[HeadLimbType.Eye_Stalk] * -0.05f) + in_Alien.ArmLimbCount[ArmLimbType.Tentacle] * 0.01f
        });
        BreathableAtmosphere.Add(GasType.Nitrogen, new Gas(GasType.Nitrogen)
        {
            Percentage = in_Alien.HeadLimbCount[HeadLimbType.EyeBalls] * 0.05f
        });
        BreathableAtmosphere.Add(GasType.Methane, new Gas(GasType.Methane)
        {
            Percentage = in_Alien.HeadLimbCount[HeadLimbType.EyeBalls] * -0.10f + in_Alien.ArmLimbCount[ArmLimbType.Tentacle] * 0.5f
        });
        BreathableAtmosphere.Add(GasType.Sulfur, new Gas(GasType.Sulfur)
        {
            Percentage = in_Alien.ArmLimbCount[ArmLimbType.Robot] * -0.05f + (in_Alien.HeadLimbCount[HeadLimbType.Antennae] > 0 ? 0.65f : 0f)
        });
        BreathableAtmosphere.Add(GasType.Hydrogen, new Gas(GasType.Hydrogen)
        {
            Percentage = in_Alien.HeadLimbCount[HeadLimbType.Mouth] > 0 ? 0.65f : 0f + in_Alien.HeadLimbCount[HeadLimbType.Antennae] > 0 ? 0.21f : 0f
        });
        BreathableAtmosphere.Add(GasType.Chlorine, new Gas(GasType.Chlorine)
        {
            Percentage = in_Alien.HeadLimbCount[HeadLimbType.Mouth] * -0.42f 
        });

        switch (in_Alien.ArmLimbCount[ArmLimbType.Tentacle])
        {
            case 1:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.01f;
                break;
            case 2:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.10f;
                break;
            case 3:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.14f;
                break;
            case 4:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.10f;
                break;
            case 5:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.02f;
                break;
            case 6:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.03f;
                break;
            case 7:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.04f;
                break;
            case 8:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.02f;
                break;
            case 9:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.03f;
                break;
            case 10:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.10f;
                break;

            case 11:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.21f;
                break;
            case 12:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.04f;
                break;
            case 13:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.10f;
                break;
            case 14:
                BreathableAtmosphere[GasType.Nitrogen].Percentage += 0.05f;
                break;
        }
        switch (in_Alien.HeadLimbCount[HeadLimbType.Warts])
        {
            case 1:
                BreathableAtmosphere[GasType.Chlorine].Percentage += 0.09f;
                break;
            case 2:
                BreathableAtmosphere[GasType.Chlorine].Percentage += 0.13f;
                break;
            case 3:
                BreathableAtmosphere[GasType.Chlorine].Percentage += 0.5f;
                break;
            case 4:
                BreathableAtmosphere[GasType.Chlorine].Percentage += 0.15f;
                break;
            case 5:
                BreathableAtmosphere[GasType.Chlorine].Percentage += 0.15f;
                break;
        }

        LikedRadio = new Radio(RadioType.NULL, 0.5f, 0.1f);
        HatedRadio = new Radio(RadioType.Cantina, 0.5f, 0.1f);

        LikedHeat = new Heat(20, 5);

        switch (MyAlien.TorsoColor)
        {
            case TorsoLimbType.Red:
                LikedHeat.TemperatureDegrees = HeatingManager.Instance.MinTemperature;
                break;

            case TorsoLimbType.Blue:
                LikedHeat.TemperatureDegrees = HeatingManager.Instance.MaxTemperature;
                break;

            case TorsoLimbType.Green:
                LikedHeat.TemperatureDegrees = HeatingManager.Instance.MaxTemperature / 2;
                break;
        }

    }

    public float RatingPenaltyPerSecond = 0.1f;
    public float RatingBonusPerSecond = 0.1f;
    public float breathableTolerance = 0.1f;

    public void Evaluate()
    {
        string gasRating = "Neutral";
        string musicRating = "Nuetral";
        string heatRating = "Neutral";

        //Atmosphere   
        float gasPenalty = 0f;
        foreach (var myGas in this.BreathableAtmosphere)
        {
            var cabinGas = AtmosphereManager.GetGas(myGas.Key);

            float diff = Mathf.Abs(cabinGas.Percentage - myGas.Value.Percentage);

            if (diff > breathableTolerance)
                gasPenalty -= RatingPenaltyPerSecond * Time.deltaTime;
            else
                gasPenalty += RatingBonusPerSecond* Time.deltaTime;
        }


        if (gasPenalty > 0)
        {
            gasRating = "Good Atmosphere: " + gasPenalty;
            ScoreManager.ImproveUberRating(gasPenalty);
        }
        else
        {
            gasRating = "Bad Atmosphere: " + gasPenalty;
            ScoreManager.DamageUberRating(-gasPenalty);
        }


        //Music
        if (RadioMusicManager.Instance.CurrentRadio.RadioType == HatedRadio.RadioType)
        {
            //I don't care what the volume is, turn it off mate.
            ScoreManager.DamageUberRating(RatingPenaltyPerSecond * Time.deltaTime);
            musicRating = "Hate Song";
        }    

        if (RadioMusicManager.Instance.CurrentRadio.RadioType == LikedRadio.RadioType)
        {
            bool withinTolerance = Mathf.Abs(RadioMusicManager.Instance.CurrentRadio.Volume - LikedRadio.Volume) < LikedRadio.Volume;
            float improvement = RatingBonusPerSecond * Time.deltaTime;
            musicRating = "Like Song";

            //Too loud, or I can't here it? I don't enjoy it so much
            if (withinTolerance)
            {
                musicRating += " ,Bad Volume";
                improvement *= 0.5f;
            }

            ScoreManager.ImproveUberRating(improvement);
        }

        //Heat
        float diffDegrees = Mathf.Abs(HeatingManager.Instance.Heat.TemperatureDegrees - LikedHeat.TemperatureDegrees);

        //That's comfy
        if (diffDegrees < LikedHeat.ToleranceDegrees)
        {
            heatRating = "Good Heat";
            ScoreManager.ImproveUberRating(RatingBonusPerSecond * Time.deltaTime);
        }

        //Dude, too hot or cold.
        else
        {
            heatRating = "Bad Heat";
            ScoreManager.DamageUberRating(RatingPenaltyPerSecond * Time.deltaTime);
        }

        MyAlien.DebugUberText.text = "Uber: " + ScoreManager.UberRating + " / 5";
        MyAlien.DebugAtmosphereText.text = "Gas: " + gasRating;
        MyAlien.DebugHeatText.text = "Heat: " + heatRating;
        MyAlien.DebugMusicText.text = "Music: " + musicRating;
    }
}
