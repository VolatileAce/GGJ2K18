using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationDial : InteractableItem {

    public Transform knob;

    public override void OnClickStay(Vector3 inWorldMousePos)
    {
        knob.LookAt(inWorldMousePos);
    }



}
