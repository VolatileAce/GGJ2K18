using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UserSpaceState
{
    Gameplay,
    EndRound
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
    public float DurationSeconds = 120;
    public float CurrentDuration = 0;

    [HideInInspector]
    public UserSpaceState CurrentGameState = UserSpaceState.Gameplay;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
