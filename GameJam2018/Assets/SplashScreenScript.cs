using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenScript : MonoBehaviour
{
    public float TransitionDelay = 0.5f;

    // Use this for initialization
    void Start ()
    {
        //StartCoroutine("TimedTransitionToMainMenu");

    }
	
	// Update is called once per frame
	void Update ()
    {

    }

    private IEnumerator TimedTransitionToMainMenu()
    {
        yield return new WaitForSeconds(TransitionDelay);
        Application.LoadLevel(1);
    }

    public void OnMainMenuClick()
    {
        Application.LoadLevel(0);
    }

    public void OnPlayClick()
    {
        Application.LoadLevel(1);
    }

    public void OnManualClick(string url)
    {
        Application.OpenURL(url);
    }

    public void OnCreditsClick()
    {
        Application.LoadLevel(2);
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
