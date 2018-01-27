using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBridge : Bridge {

    public GasType GasType;

    public override void doSliderChange(float input)
    {
        base.doSliderChange(input);

        AtmosphereManager.SetGas(GasType, input);
    }


}
