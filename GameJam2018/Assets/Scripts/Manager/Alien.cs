using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Randomized passenger with both initial, and ongoing requirements
/// </summary>
public class Alien : MonoBehaviour
{
    public string RacialName = "Alien";

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

    public void RegenerateRacialName()
    {
        RacialName = NamePrefix[(int)Random.Range(0, NamePrefix.Count)] + NamePostfix[(int)Random.Range(0, NamePostfix.Count)];        
    }
    

    /// <summary>
    /// The gas required to be happy and survive
    /// </summary>
    public Gas BreathableAtmosphere;
        

	// Use this for initialization
	void Start ()
    {
        RegenerateRacialName();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
