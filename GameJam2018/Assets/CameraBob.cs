using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    //public Vector3 restPosition; //local position where your camera would rest when it's not bobbing.
    public float transitionSpeed = 20f; //smooths out the transition from moving to not moving.
    public float bobSpeed = 4.8f; //how quickly the player's head bobs.
    public float bobAmount = 0.05f; //how dramatic the bob is. Increasing this in conjunction with bobSpeed gives a nice effect for sprinting.

    float timer = Mathf.PI; //initialized as this value because this is where sin = 1. So, this will make the camera always start at the crest of the sin wave, simulating someone picking up their foot and starting to walk--you experience a bob upwards when you start walking as your foot pushes off the ground, the left and right bobs come as you walk.


    private Camera MyCamera;

    void Awake()
    {
        MyCamera = gameObject.GetComponentInParent<Camera>();
    }

    void Update()
    {

        timer += bobSpeed * Time.deltaTime ;

        //use the timer value to set the position
        
        Vector3 newPosition = new Vector3(Mathf.Cos(timer) * bobAmount ,( Mathf.Sin(timer) * bobAmount) * 0.25f, 0); 
        Vector3 newRotation = new Vector3(Mathf.Cos(timer) * bobAmount, (Mathf.Sin(timer) * bobAmount) * 0.25f, 0);

        if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
            timer = 0;

        MyCamera.transform.localPosition += newPosition;
        //MyCamera.transform.localRotation += Quaternion.FromToRotation(newRotation, Vector3.forward);
    }
}