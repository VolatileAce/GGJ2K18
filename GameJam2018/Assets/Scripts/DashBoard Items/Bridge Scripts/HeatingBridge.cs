using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingBridge : Bridge {

    public override void doSliderChange(float input)
    {
        base.doSliderChange(input);
        HeatingManager.SetHeatDegrees(input);
    }



}
