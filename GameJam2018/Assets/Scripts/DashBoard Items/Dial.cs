﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : Slider {

    public Transform KnobMid;

    private float clickedValue;


    public override void OnClickEnter(Vector3 inWorldMousePos)
    {
        base.OnClickEnter(inWorldMousePos);

        clickedValue = CurrentValue;
    }

    public override void OnClickStay(Vector3 in_WorldMousePos)
    {
        base.OnClickStay(in_WorldMousePos);

        Vector3 mouseDirection = in_WorldMousePos - Knob.transform.position;
        float angle = Vector3.Angle(mouseDirection, -Knob.transform.up);

        Debug.DrawRay(Knob.transform.position, mouseDirection,Color.green,0.5f);

        Debug.DrawRay(Knob.transform.position, Knob.transform.right * mouseDirection.magnitude, Color.red,0.5f);

        if (Mathf.Sign(Vector3.Dot(mouseDirection, Knob.transform.right)) < 0)
        {
            angle = 180 + (180 - angle);
        }



        OnSliderChange((angle / 320) + CurrentValue);

    }


    public override void OnSliderChange(float newValue)
    {
        Quaternion newRot;
        if (newValue < 0.5f)
           newRot = Quaternion.Lerp(KnobMin.rotation, KnobMid.rotation, newValue * 2);
        else
           newRot = Quaternion.Lerp(KnobMid.rotation, KnobMax.rotation, (newValue - 0.5f) * 2);
        Knob.transform.rotation = newRot;
        LightMatInstance.color = ClickedColor;
        lastChangeTime = Time.time;
        hasChangedColor = false;
        OnValueChanged.Invoke(newValue);
    }


}
