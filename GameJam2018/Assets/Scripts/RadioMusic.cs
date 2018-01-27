using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum RadioType
{
    NULL,

    Cantina,
    Space_Opera,
    Retro_Millenium
}

[System.Serializable]
public class Radio
{
    public RadioType RadioType;
    public float Volume;
    public float TolerancePercentage;

    public Radio(RadioType in_Type, float in_Volume, float in_TolerancePercentage)
    {
        this.RadioType = in_Type;
        Volume = 0;
        TolerancePercentage = in_TolerancePercentage;
    }
}
