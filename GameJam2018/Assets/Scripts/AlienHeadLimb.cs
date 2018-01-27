using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum HeadLimbType
{
    Null,

    Eye_Stalk,
    Warts,
    EyeBalls,
    Mouth,
    Antennae,
    Ears,

    MAX_TYPES
}


public class AlienHeadLimb : AlienLimb
{
    public HeadLimbType Limb;
    public override LimbLocation Location
    {
        get
        {
            return LimbLocation.Hat;
        }
    }

    public AlienHeadLimb(HeadLimbType in_Limb)
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
