using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioBridge : Bridge {

	public void NextRadio()
    {
        


    }

    public override void doSliderChange(float input)
    {
        base.doSliderChange(input);
        RadioMusicManager.SetRadioVolume(input);
    }




}
