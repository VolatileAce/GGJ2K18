using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum HatLimbType
{
    Null,

    Eye_Stalk,
    Horns,

    MAX_TYPES
}


public class AlienHatLimb : AlienLimb
{
    public HatLimbType Limb;
    public override LimbLocation Location
    {
        get
        {
            return LimbLocation.Hat;
        }
    }

    public AlienHatLimb(HatLimbType in_Limb)
    {
        Limb = in_Limb;
    }


    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
