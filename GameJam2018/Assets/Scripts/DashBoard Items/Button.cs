using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using UnityEngine.Events;

[System.Serializable]
public class ButtonEvent : UnityEvent<bool> { }

public class Button : InteractableItem {

    public int Note;
    public Transform buttonKnob;
    public float buttonClickDistance = 0.0025f;

    public ButtonEvent OnButtonClicked;


    #region MouseControls

    public override void OnClickEnter(Vector3 inWorldMousePos)
    {
        base.OnClickEnter(inWorldMousePos);
        OnButtonDown();

    }
    public override void OnClickExit(Vector3 inWorldMousePos)
    {
        base.OnClickExit(inWorldMousePos);
        OnButtonUp();
    }

    #endregion MouseControls

    #region ButtonFunctionality
    public void OnButtonDown()
    {
        buttonKnob.Translate(-transform.right * transform.lossyScale.y * buttonClickDistance);
        OnButtonClicked.Invoke(true);

        LightMatInstance.color = ClickedColor;
    }

    public void OnButtonUp()
    {
        buttonKnob.Translate(transform.right * transform.lossyScale.y * buttonClickDistance);
        OnButtonClicked.Invoke(false);

        LightMatInstance.color = BaseColor;
    }

    #endregion Button Functionality

    #region Unity Fucntions

    protected override void Start()
    {
        base.Start();
        MidiManager.RegisterButton(this);
    }


    #endregion Unity Functions





}
