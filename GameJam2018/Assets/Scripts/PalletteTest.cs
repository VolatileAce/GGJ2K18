using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class PalletteTest : MonoBehaviour {

    public float axisTest;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        

       axisTest =  MidiMaster.GetKnob(MidiChannel.Ch1, 1);

	}
}
