﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public enum UserSpaceState
{
    NULL, 

    Pickup,
    Transport,
    Destination,

    MAX_STATES    
}

public enum GameWinState
{
    UNSET,

    PendingStart,
    Playing,
    Dead,
    Won,
    PostWin
}
    
/// <summary>
/// One round is one trip
/// </summary>
public class RoundManager : MonoBehaviour
{
    [HideInInspector]
    public Dictionary<UserSpaceState, int> StateDurations = new Dictionary<UserSpaceState, int>();
    
    public UserSpaceState CurrentGameState = UserSpaceState.NULL;
    public int PickupTimeMin = 30;
    public int PickupTimeMax = 60;

    public int TransportTimeMin = 30;
    public int TransportTimeMax = 60;

    [HideInInspector]
    public Stopwatch RoundTimer;
    [HideInInspector]
    public Stopwatch DriveTimer;

    public Text ClockText;
    public Text StateText;

    public void ChangeState(UserSpaceState in_NewState)
    {
        CurrentGameState = in_NewState;
        StateText.text = CurrentGameState.ToString().Replace('_', ' ');
    }

    void Awake()
    {
        int seed = System.DateTime.Now.Millisecond + System.DateTime.Now.Minute + System.DateTime.Now.Hour;
        Random.InitState(seed);

        StateDurations.Add(UserSpaceState.Pickup, Random.Range(PickupTimeMin, PickupTimeMax));
        StateDurations.Add(UserSpaceState.Transport, Random.Range(TransportTimeMin, TransportTimeMax));        
        
        RoundTimer = Stopwatch.StartNew();
        ChangeState(UserSpaceState.Pickup);
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        int remainingTimeInState = StateDurations[CurrentGameState] - (int)RoundTimer.Elapsed.TotalSeconds;
        ClockText.text =  UberSpaceHelpers.SecondsToString(remainingTimeInState);

        switch(CurrentGameState)
        {
            case UserSpaceState.Pickup:
                if (remainingTimeInState <= 0)
                    ChangeState(UserSpaceState.Transport);
                break;

            case UserSpaceState.Transport:
                if (remainingTimeInState <= 0)
                {
                    ChangeState(UserSpaceState.Destination);
                    ScoreManager.EvaluateWinCondition();
                }
                break;
        }
    }
}
