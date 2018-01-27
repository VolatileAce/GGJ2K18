using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AlienRequirements
{
    /// <summary>
    /// The gas required to be happy and survive
    /// </summary>
    public Gas BreathableAtmosphere;

    /// <summary>
    /// The gas that creates unhappiness and death
    /// </summary>
    public Gas ToxicAtmosphere;

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

    public Alien MyAlien;

    /// <summary>
    /// Initialize the preferences based on the appearance of the alien
    /// </summary>
    /// <param name="in_Alien"></param>
    public AlienRequirements(Alien in_Alien)
    {
        MyAlien = in_Alien;

        BreathableAtmosphere = new Gas(GasType.Oxygen);
        ToxicAtmosphere = new Gas(GasType.Carbon_Dioxide);

        LikedRadio = new Radio(RadioType.NULL, 0.5f, 0.1f);
        HatedRadio = new Radio(RadioType.Cantina, 0.5f, 0.1f);

        LikedHeat = new Heat(20, 5);


        switch (((AlienTorsoLimb)MyAlien.GetLimb(LimbLocation.Torso)).Limb)
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

    public void Evaluate()
    {
        string gasRating = "Neutral";
        string musicRating = "Nuetral";
        string heatRating = "Neutral";

        //Atmosphere   
        var goodGas = AtmosphereManager.GetGas(this.BreathableAtmosphere.GasType);
        var badGas = AtmosphereManager.GetGas(this.ToxicAtmosphere.GasType);

        float breathableTolerance = 0.1f;

        if (goodGas.GasType != GasType.Null)
            if (Mathf.Abs(goodGas.Percentage - BreathableAtmosphere.Percentage) < breathableTolerance)
                ScoreManager.ImproveUberRating(breathableTolerance * Time.deltaTime);

        if (badGas.GasType != GasType.Null)
            ScoreManager.DamageUberRating(badGas.Percentage * Time.deltaTime);

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
