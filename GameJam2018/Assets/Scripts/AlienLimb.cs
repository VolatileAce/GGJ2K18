using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// an asset and location to place on the alien
/// </summary>
[System.Serializable]
public class AlienLimb : MonoBehaviour
{
    public virtual LimbLocation Location { get { return LimbLocation.Null; } }
}