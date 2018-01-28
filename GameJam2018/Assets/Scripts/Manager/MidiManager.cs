using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MidiJack;

public class MidiManager : MonoBehaviour {

    public static MidiManager Instance;

    public List<Button> AllButtons;
    public List<Slider> AllKnobs;


    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        MidiMaster.noteOffDelegate += OnButtonUp;
        MidiMaster.noteOnDelegate += OnButtonDown;
        MidiMaster.knobDelegate += OnKnobChange;
    }

	public void OnButtonDown(MidiChannel channel, int noteNumber, float velocity)
    {
        foreach (Button button in AllButtons)
        {
            if ((button.Channel == channel) && (button.Note == noteNumber))
                button.OnButtonDown();
        }
    }

    public void OnButtonUp(MidiChannel channel, int noteNumber)
    {
        foreach (Button button in AllButtons)
        {
            if ((button.Channel == channel) && (button.Note == noteNumber))
                button.OnButtonUp();
        }
   } 

    public void OnKnobChange(MidiChannel channel, int DialNumber, float newValue)
    {
        foreach (Slider Knob in AllKnobs)
        {
            if (Knob.Channel == channel && Knob.KnobNumber == DialNumber)
            {
                Knob.OnSliderChange(newValue);
            }
        }
    }


    public static void RegisterButton(Button in_Button)
    {
      //  if ()

        Instance.AllButtons.Add(in_Button);
    }

    public static void RegisterDial(Slider in_Dial)
    {
        Instance.AllKnobs.Add(in_Dial);
    }

    void OnDestroy()
    {
        MidiMaster.noteOffDelegate -= OnButtonUp;
        MidiMaster.noteOnDelegate -= OnButtonDown;
        MidiMaster.knobDelegate -= OnKnobChange;
    }

}
