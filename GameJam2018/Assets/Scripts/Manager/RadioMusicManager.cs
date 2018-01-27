using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioMusicManager : MonoBehaviour
{
    //Static Acess to this class set on awake
    public static RadioMusicManager Instance;

    public Radio CurrentRadio;

    void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public static void SetRadioType(RadioType in_Radio)
    {
        Instance.CurrentRadio.RadioType = in_Radio;

        //Switch the playback to something else
    }

    public static void SetRadioVolume(float in_Volume)
    {
        Debug.Assert(in_Volume > 0, "Volume too quiet");
        Debug.Assert(in_Volume > 1, "Volume too high");

        Instance.CurrentRadio.Volume = in_Volume;
    }

}
