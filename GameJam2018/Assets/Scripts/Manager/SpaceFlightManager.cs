using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceFlightManager : MonoBehaviour
{
    public static SpaceFlightManager Instance;

    public ParticleSystem HyperspaceParticles;

    void Awake()
    {
        Instance = this;
    }

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
