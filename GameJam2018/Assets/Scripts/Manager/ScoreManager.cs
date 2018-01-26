using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    /// <summary>
    /// The current score rating being left by the Alien Pax
    /// </summary>
    public float UberRating { get; protected set; }      

    public void DamageUberRating (float in_Damage)
    {
        Debug.Assert(in_Damage > 0, "Damage must be great than 0");
        Debug.Assert(in_Damage < 5, "Damage cannot exceed 5");

        UberRating -= in_Damage;
    }

    public void ImproveUberRating(float in_Improvement)
    {
        Debug.Assert(in_Improvement > 0, "Improvement must be great than 0");
        Debug.Assert(in_Improvement < 5, "Improvement cannot exceed 5");

        UberRating += in_Improvement;
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
