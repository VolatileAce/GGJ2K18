using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;
using UnityEngine.Events;

[System.Serializable]
public class SliderEvent : UnityEvent<float> { }
public class Slider : InteractableItem
{

    public float CurrentValue = 0;

    public int KnobNumber;

    public SliderEvent OnValueChanged;

    public Transform Knob;
    public Transform KnobMin;
    public Transform KnobMax;

    protected float lastChangeTime;
    protected bool hasChangedColor = true;

    protected override void Start()
    {
        base.Start();
        MidiManager.RegisterDial(this);
    }

    public override void OnClickStay(Vector3 in_WorldMousePos)
    {
        base.OnClickStay(in_WorldMousePos);
        Vector3 minMaxDirection = KnobMax.position - KnobMin.position;
        Vector3 mouseDirection = in_WorldMousePos - KnobMin.position;

        mouseDirection = Vector3.Project(mouseDirection, minMaxDirection.normalized);

        float value = mouseDirection.magnitude / minMaxDirection.magnitude;
        //value * Vector3.Dot(mouseDirection, minMaxDirection);
        value = value * Mathf.Sign(Vector3.Dot(mouseDirection, minMaxDirection));
        
        OnSliderChange(Mathf.Clamp01(value));
    }

    public virtual void OnSliderChange(float newValue)
    {
        Vector3 newPos = Vector3.Lerp(KnobMin.position, KnobMax.position, newValue);
        Knob.transform.position = newPos;
        LightMatInstance.color = ClickedColor;
        lastChangeTime = Time.time;
        hasChangedColor = false;
        OnValueChanged.Invoke(newValue);
        CurrentValue = newValue;
    }

    void Update()
    {
        if (Time.time - lastChangeTime > 0.2f && LightMatInstance.color != BaseColor && !hasChangedColor)
        {
            LightMatInstance.color = BaseColor;
            hasChangedColor = true;
        }


    }





}
