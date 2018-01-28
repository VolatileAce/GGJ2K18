using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;


public class MidiLearner : MonoBehaviour
{

    public List<midiInput> AllDials;
    public List<midiInput> AllButtons;

    public static MidiLearner Instance;
    public bool CustomInput = false;

    public void Awake()
    {
        Instance = this;
    }

    public void StartListening()
    {
        MidiMaster.noteOnDelegate += ButtonPressed;
        MidiMaster.knobDelegate += KnobInput;
        DontDestroyOnLoad(gameObject);
        CustomInput = true;
    }

    public void StopListening()
    {
        MidiMaster.noteOnDelegate -= ButtonPressed;
        MidiMaster.knobDelegate -= KnobInput;
    }

    public void ButtonPressed(MidiChannel channel, int noteNumber, float velocity)
    {
        foreach (midiInput button in AllButtons)
        {
            if (button.channel == channel && button.number == noteNumber)
            {
                return;
            }
        }
        midiInput newInput = new midiInput(channel, noteNumber);
        AllButtons.Add(newInput);
    }

    public void KnobInput(MidiChannel channel, int dialNumber, float valueChanged)
    {
        foreach (midiInput dial in AllDials)
        {
            if (dial.channel == channel && dial.number == dialNumber)
            {
                return;
            }
        }
        midiInput newInput = new midiInput(channel, dialNumber);
        AllDials.Add(newInput);
    }
}

public struct midiInput
{
    public MidiChannel channel;
    public int number;

    public midiInput(MidiChannel channel, int number)
    {
        this.channel = channel;
        this.number = number;
    }
}
