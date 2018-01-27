using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ArmLimbType
{
    Null,

    Tentacle,
    Robot,

    MAX_TYPES
}

public class AlienArmLimb : AlienLimb
{
    public ArmLimbType Limb;
    public override LimbLocation Location
    {
        get
        {
            return LimbLocation.Arms;
        }
    }

    public AlienArmLimb(ArmLimbType in_Limb)
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
