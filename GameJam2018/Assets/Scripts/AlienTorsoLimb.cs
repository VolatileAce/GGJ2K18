using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum TorsoLimbType
{
    Null,

    Lizard_Torso,
    Xenomorph_Torso,

    MAX_TYPES
}

public class AlienTorsoLimb : AlienLimb
{
    public TorsoLimbType Limb;
    public override LimbLocation Location
    {
        get
        {
            return LimbLocation.Torso;
        }
    }

    public AlienTorsoLimb(TorsoLimbType in_Limb)
    {
        Limb = in_Limb;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
